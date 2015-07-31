using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.Common;
using YoYoStudio.Common.Wcf;
using YoYoStudio.DataService.Client;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;

namespace YoYoStudio.ChatService.Library
{
	internal class UserNCallback
	{
		public User User { get; set; }
		public IChatServiceCallback Callback { get; set; }
        public string PublicIpAddress { get; set; }
        public string MacAddress { get; set; }
        public string DataServiceToken { get; set; }
        public ChatService ServiceInstance { get; set; }
		public UserApplicationInfo UserInfo { get; set; }
        public SafeList<int> CommandIds { get; set; }
	}

    internal class RoomNCallback
    {
        public IChatServiceCallback Callback { get; set; }
        public string PublicIpAddress { get; set; }
        public ChatService ServiceInstance { get; set; }
    }

	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession, ConfigurationName = Const.ChatServiceName)]
	public partial class ChatService : WcfService, IChatService, IDisposable
	{
		#region Private Members

        private UserNCallback unc = null;
        private RoomNCallback rnc = null;

        private bool CheckUser()
		{
			return !string.IsNullOrEmpty(serviceToken) && unc != null && unc.User != null;
		}

        private void Channel_Faulted(object sender, EventArgs e)
		{
			LogOff();
		}

        private void Channel_Closing(object sender, EventArgs e)
		{
			LogOff();
		}

        private void RoomChannel_Faulted(object sender, EventArgs e)
        {
            LogOff();
        }

        private void RoomChannel_Closing(object sender, EventArgs e)
        {
            LogOff();
        }

        private void BroadCast(Action<UserNCallback> act, int senderId)
        {
            Task.Factory.StartNew(() =>
            {
                var users = userCache.Values;
                foreach (var u in users)
                {
                    if (u.User.Id != senderId)
                    {
                        try
                        {
                            act(u);
                        }
                        catch
                        {
                            LogOff(u);
                        }
                    }
                    
                }
            });
        }

        private void LogOff(UserNCallback usrNC)
        {
            userCache.Remove(unc.User.Id);
            BroadCast((u) => u.Callback.UserLoggedOff(u.User.Id), usrNC.User.Id);
            usrNC.ServiceInstance.Dispose();
        }

        private void LogOff(int userId)
        {
            BroadCast((u) => u.Callback.UserLoggedOff(userId), userId);
            if (userCache.ContainsKey(userId))
            {
                userCache.Remove(userId);
                userCache[userId].ServiceInstance.Dispose();
            }
        }

        private void LogOff(RoomNCallback roomNC)
        {
            roomCache.Remove(roomNC.PublicIpAddress);
            roomNC.ServiceInstance.Dispose();
        }

        private void DoCommand(int roomId, int cmdId, UserNCallback sourceUser, int targetUserId)
        {
            var adminId = BuiltIns._9258Administrator.Id;
            switch (cmdId)
            {
                case Applications._9258App.UserCommands.AddToBlackListCommandId:
                    dataServiceClient.AddBlockList(sourceUser.User.Id, sourceUser.DataServiceToken, new BlockList { Application_Id = ApplicationId, BlockType_Id = BuiltIns.BlackListType.Id, Content = targetUserId.ToString() });
                    break;
                case Applications._9258App.UserCommands.BlockUserIdCommandId:
                    dataServiceClient.AddBlockList(sourceUser.User.Id, sourceUser.DataServiceToken, new BlockList { Application_Id = ApplicationId, BlockType_Id = BuiltIns.BlockUserType.Id, Content = targetUserId.ToString() });
                    break;
                case Applications._9258App.UserCommands.BlockUserIpCommandId:
                    dataServiceClient.AddBlockList(sourceUser.User.Id, sourceUser.DataServiceToken, new BlockList { Application_Id = ApplicationId, BlockType_Id = BuiltIns.BlockIPType.Id, Content = targetUserId.ToString() });
                    break;
                case Applications._9258App.UserCommands.BlockUserMacCommandId:
                    dataServiceClient.AddBlockList(sourceUser.User.Id, sourceUser.DataServiceToken, new BlockList { Application_Id = ApplicationId, BlockType_Id = BuiltIns.BlockMacType.Id, Content = targetUserId.ToString() });
                    break;
                case Applications._9258App.UserCommands.SetOrCancelRoomManagerCommandId:
                    var rr = dataServiceClient.GetRoomRole(sourceUser.User.Id, sourceUser.DataServiceToken, roomId, targetUserId, BuiltIns._9258RoomAdministratorRole.Id);
                    if (rr == null)
                    {
                        rr = new RoomRole { Room_Id = roomId, User_Id = targetUserId, Role_Id = BuiltIns._9258RoomAdministratorRole.Id };
                        dataServiceClient.AddRoomRole(sourceUser.User.Id, sourceUser.DataServiceToken,rr );
                        cache.AddRoomRole(rr);
                    }
                    else
                    {
                        dataServiceClient.DeleteRoomRole(sourceUser.User.Id, sourceUser.DataServiceToken, roomId, targetUserId, BuiltIns._9258RoomAdministratorRole.Id);
                        cache.RemoveRoomRole(roomId, BuiltIns._9258RoomAdministratorRole.Id, targetUserId);
                    }
                    break;
                case Applications._9258App.UserCommands.UpDownUserPrivateMicCommandId:
                case Applications._9258App.UserCommands.UpDownUserPublicMicCommandId:
                case Applications._9258App.UserCommands.BlockHornHallHornCommandId:
                case Applications._9258App.UserCommands.AllowConnectPrivateMicCommandId:
                case Applications._9258App.UserCommands.AllowConnectSecretMicCommandId:
                case Applications._9258App.UserCommands.KickOutOfRoomCommandId:
                    break;
            }
        }

		#endregion

		#region IDisposable Implementation

		private bool disposed = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
				}
                unc = null;
                rnc = null;
			}
		}

        

		~ChatService()
		{
			Dispose(false);
		}

		#endregion

		#region Service Implementation

        [OperationBehavior]
        public bool ScoreExchange(int userId,int scoreToExchange, int moneyToGet)
        {
            var userNC = userCache[userId];
            try
            {
                dataServiceClient.ScoreExchange(userId, userCache[userId].DataServiceToken, BuiltIns._9258ChatApplication.Id, userId, scoreToExchange, moneyToGet);
                userNC.UserInfo.Score -= scoreToExchange;
                if (!userNC.UserInfo.Money.HasValue)
                    userNC.UserInfo.Money = 0;
                userNC.UserInfo.Money += moneyToGet;
            }
            catch
            {
                return false;
            }
            return true;
        }

		[OperationBehavior]
		public User Login(int userId, string password, string macAddress)
		{
            string token = string.Empty;
            logger.Debug("Enter Login : UserId - " + userId);
            if (userId == BuiltIns._9258Administrator.Id)
            {
                if (Utility.GetMD5String(password) == BuiltIns._9258Administrator.Password)
                {
                    token = serviceToken;
                    logger.Debug("9258 Admin Login");
                }
                else
                {
                    logger.Debug("9258 Admin Login Failed");
                    return null;
                }
            }
            if (string.IsNullOrEmpty(token) && userId != BuiltIns._9258Administrator.Id)
            {
                token = dataServiceClient.Login(userId, password);
                logger.Debug("Normal Login");
            }
			if (!string.IsNullOrEmpty(token))
			{
                logger.Debug("Normal Login Succeed");
                bool relogin = false;
                if (userCache.ContainsKey(userId))
                {
                    logger.Debug("Relogin");
                    relogin = true;
                    try
                    {
                        var u = userCache[userId];
                        userCache.Remove(userId);
						userCache[userId].ServiceInstance.Dispose();
                        u.Callback.UserRelogin();                        
                    }
                    catch { }
                }
				OperationContext.Current.Channel.Faulted += Channel_Faulted;
				OperationContext.Current.Channel.Closing += Channel_Closing;
				User usr = dataServiceClient.GetUser(userId, token, userId);
				var info = dataServiceClient.GetUserInfo(BuiltIns._9258Administrator.Id, serviceToken, userId, ApplicationId);
                if (usr != null && info != null)
                {
                    logger.Debug("Normal Login Exit");
                    unc = new UserNCallback
                    {
                        User = usr,
                        Callback = OperationContext.Current.GetCallbackChannel<IChatServiceCallback>(),
                        PublicIpAddress = GetClientRemoteEndPoint().Address,
                        MacAddress = macAddress,
                        DataServiceToken = token,
                        UserInfo = info,
                        CommandIds = new SafeList<int>(),
                        ServiceInstance = this
                    };
                    userCache.Add(userId, unc);

                    if (!relogin)
                    {
                        BroadCast(u => u.Callback.UserLoggedIn(unc.User), unc.User.Id);
                    }
                    return usr;
                }
			}
            logger.Debug("Normal Login return Null");
			return null;
		}

		[OperationBehavior]
		public User Register(int userId,string account, string password, int gender)
		{
            return dataServiceClient.Register(userId, account, password, gender);            
		}

        [OperationBehavior]
        public bool UpdateUser(User user)
        {
            if (unc.User.Id == user.Id)
            {
                unc.User = user;
                dataServiceClient.UpdateUser(unc.User.Id, unc.DataServiceToken, user);
            }
            return true;
        }
        [OperationBehavior]
        public bool UpdateUserHeaderImange(Image theImage)
        {
            dataServiceClient.UpdateImage(unc.User.Id, unc.DataServiceToken, theImage);
            return true;
        }

		[OperationBehavior]
		public int GetOnlineUserCount()
		{
			return userCache.Count;
		}
		[OperationBehavior]
		public void LogOff()
		{
			if (unc != null)
			{
                LogOff(unc);
			}
            if (rnc != null)
            {
                LogOff(rnc);
            }
		}
		[OperationBehavior]
		public void KeepAlive()
		{
		}
        [OperationBehavior]
        public bool RoomLogin()
        {
            string publicIp = GetClientRemoteEndPoint().Address;
            try
            {
                if (roomCache.ContainsKey(publicIp))
                {
                    var r = roomCache[publicIp];
                    roomCache.Remove(publicIp);
                    roomCache[publicIp].Callback.RoomRelogin();
                    roomCache[publicIp].ServiceInstance.Dispose();
                }
            }
            catch { }
            OperationContext.Current.Channel.Faulted += RoomChannel_Faulted;
            OperationContext.Current.Channel.Closing += RoomChannel_Closing;
            rnc = new RoomNCallback
            {
                Callback = OperationContext.Current.GetCallbackChannel<IChatServiceCallback>(),
                PublicIpAddress = publicIp,
                ServiceInstance = this
            };
            roomCache.Add(publicIp, rnc);
            return true;
        }

		[OperationBehavior]
		public List<Model.Chat.RoomGroup> GetRoomGroups()
		{
			return cache.RoomGroups;
		}
		[OperationBehavior]
		public List<Model.Chat.Room> GetRooms()
		{
			return cache.Rooms;
		}
		[OperationBehavior]
		public List<Role> GetRoles()
		{
			return cache.Roles;
		}
		[OperationBehavior]
		public List<RoleCommandView> GetRoleCommands()
		{
			return cache.RoleCommands;
		}
		[OperationBehavior]
		public List<Model.Chat.GiftGroup> GetGiftGroups()
		{
			return cache.GiftGroups;
		}
		[OperationBehavior]
		public List<Model.Chat.Gift> GetGifts()
		{
			return cache.Gifts;
		}

		[OperationBehavior]
		public List<Command> GetCommands()
		{
			return cache.Commands;
		}

		[OperationBehavior]
		public List<ImageWithoutBody> GetImages(int start, int count)
		{
			return cache.Images.Skip(start-1).Take(count).ToList();
		}

		[OperationBehavior]
		public int GetImageCount()
		{
			return cache.Images == null ? 0 : cache.Images.Count;
		}

		[OperationBehavior]
		public int GetRoomRoleCount()
		{
			return cache.RoomRoles == null ? 0 : cache.RoomRoles.Count;
		}
		[OperationBehavior]
		public List<RoomRole> GetRoomRoles(int start, int count)
		{
			return cache.RoomRoles.Skip(start-1).Take(count).ToList();
		}
        [OperationBehavior]
        public int GetNextAvailableUserId(int roleId)
        {
            return dataServiceClient.GetNextAvailableUserId(BuiltIns._9258Administrator.Id, serviceToken, BuiltIns._9258ChatApplication.Id, BuiltIns.RegisterUserRole.Id);
        }
        [OperationBehavior]
        public Image GetImage(int imgId)
        {
            var img = dataServiceClient.GetImage(BuiltIns._9258Administrator.Id, serviceToken, imgId);
            lock (cache.Images)
            {
                cache.Images.Add(new ImageWithoutBody()
                {
                    Id = img.Id,
                    Ext = img.Ext,
                    ImageGroup = img.ImageGroup,
                    ImageType_Id = img.ImageType_Id,
                    IsBuiltIn = img.IsBuiltIn,
                    Name = img.Name
                });
            }
            return img;
        }
		[OperationBehavior]
        public UserApplicationInfo GetUserInfo(int userId)
        {
			var usr = userCache[userId];
			if (usr != null)
			{
				return usr.UserInfo;
			}
			return null;
        }

        [OperationBehavior]
        public MessageResult SendHornMessage(int roomId, int senderId, int cmdId)
        {
            MessageResult result = MessageResult.Succeed;
            var sender = userCache[senderId];
            if (!cache.HasCommand(roomId, cmdId, senderId, sender.UserInfo.Role_Id, -1))
            {
                result = MessageResult.NotEnoughPrivilege;
            }
            else
            {
                var cmd = cache.Commands.FirstOrDefault(c => c.Id == cmdId);
                try
                {
                    if (!sender.UserInfo.Money.HasValue || sender.UserInfo.Money < cmd.Money)
                        result = MessageResult.NotEnoughMoney;
                    else
                    {
                        sender.UserInfo.Money -= cmd.Money;
                        var usr = userCache[senderId];
                        dataServiceClient.UpdateUserInfo(senderId, usr.DataServiceToken, sender.UserInfo);
                    }
                }
                catch
                {
                    result = MessageResult.UnkownError;
                }
            }
            return result;
        }

        [OperationBehavior]
		public SendGiftResult SendGift(int roomId, int senderId, int receiverId, int giftId, int count)
		{
			SendGiftResult result = SendGiftResult.Succeed;
			var sender = userCache[senderId];
			var receiver = userCache[receiverId];
			if (!cache.HasCommand(roomId, Applications._9258App.FrontendCommands.SendGiftCommandId,senderId, sender.UserInfo.Role_Id,-1))
			{
				result = SendGiftResult.CannotSendGift;
			}
			else if (!cache.HasCommand(roomId, Applications._9258App.FrontendCommands.ReceiveGiftCommandId,receiverId,receiver.UserInfo.Role_Id,-1))
			{
				result = SendGiftResult.CannotReceiveGift;
			}
			else
			{
				var gift = cache.Gifts.FirstOrDefault(g => g.Id == giftId);
				try
				{
                    if (!sender.UserInfo.Money.HasValue || sender.UserInfo.Money < gift.Price * count)
					{
						result = SendGiftResult.NotEnoughMoney;
					}
					else
					{
						sender.UserInfo.Money -= gift.Price * count;
						var usr = userCache[senderId];
						dataServiceClient.UpdateUserInfo(senderId, usr.DataServiceToken, sender.UserInfo);

                        if (!receiver.UserInfo.Score.HasValue)
                            receiver.UserInfo.Score = 0;
                        receiver.UserInfo.Score += gift.Score * count;
                        usr = userCache[receiverId];
                        dataServiceClient.UpdateUserInfo(receiverId, usr.DataServiceToken, receiver.UserInfo);
					}
				}
				catch
				{
					result = SendGiftResult.UnkownError;
				}
			}
			return result;
		}
        
        [OperationBehavior]
        public bool ExecuteCommand(int roomId, int cmdId, int sourceUserId, int targetUserId)
        {
            UserNCallback sourceUesr = null;
            UserNCallback targetUser = null;
            if (userCache.ContainsKey(sourceUserId))
            {
                sourceUesr = userCache[sourceUserId];
            }
            if (userCache.ContainsKey(targetUserId))
            {
                targetUser = userCache[targetUserId];
            }
            if (sourceUesr == null)
            {
                LogOff(sourceUserId);
            }
            else if (targetUser == null)
            {
                LogOff(targetUserId);
            }
            else
            {
                if (cache.HasCommand(roomId, cmdId, sourceUserId, sourceUesr.UserInfo.Role_Id, targetUser.UserInfo.Role_Id))
                {
                    DoCommand(roomId, cmdId, sourceUesr, targetUserId);
                    return true;
                }
            }
            return false;
        }
        [OperationBehavior]
        public int CanUserLogin(int userId, string macAddress)
        {
            logger.Debug("CanUserLogin : " + userId);
            try
            {
                var blockList = dataServiceClient.GetUserBlockList(BuiltIns._9258Administrator.Id, serviceToken, macAddress);
                if (blockList != null && blockList.Count > 0)
                {
                    return blockList[0].BlockType_Id;
                }
                blockList = dataServiceClient.GetUserBlockList(BuiltIns._9258Administrator.Id, serviceToken, userId.ToString());
                if (blockList != null && blockList.Count > 0)
                {
                    return blockList[0].BlockType_Id;
                }
                var ep = GetClientRemoteEndPoint();
                blockList = dataServiceClient.GetUserBlockList(BuiltIns._9258Administrator.Id, serviceToken, ep.Address);
                if (blockList != null && blockList.Count > 0)
                {
                    return blockList[0].BlockType_Id;
                }
            }
            catch (Exception ex)
            {
                logger.Error("CanUserLogin Failed",ex);
            }
            return 0;
        }
        [OperationBehavior]
        public List<BlockType> GetBlockTypes()
        {
            return cache.BlockTypes;
        }
        [OperationBehavior]
		public long GetCacheVersion()
        {
            return cacheVersion;
        }
		[OperationBehavior]
		public void UpdateRoomOnlineUserCount(Dictionary<int, int> roomUsersCount)
		{
            lock (roomUserCountCache)
            {
                foreach (var pair in roomUsersCount)
                {
                    roomUserCountCache[pair.Key] = pair.Value;
                }
            }
		}
		[OperationBehavior]
		public Dictionary<int, int> GetRoomOnlineUserCount()
		{
            lock (roomUserCountCache)
            {
                return roomUserCountCache;
            }
		}
        [OperationBehavior]
        public List<ExchangeRate> GetExchangeRates()
        {
            return cache.ExchangeRates;
        }
		#endregion
	}

}
