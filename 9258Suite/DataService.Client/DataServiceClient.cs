using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Media;

namespace YoYoStudio.DataService.Client.Generated
{
    public partial class DataServiceClient
    {
        static DataServiceClient()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (
                (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                });
        }
    }
    public class DSClient 
    {
        private int application_Id;
        private DataServiceClient client;

		public DSClient(int appid)
			: this(appid, Const.DataServiceName)
		{
		}

		public DSClient(int appid, string endPointConfigurationName)
        {
            application_Id = appid;
			client = new DataServiceClient(endPointConfigurationName);
        }

		public void Close()
		{
			client.Close();
		}

        public List<RoomRole> GetRoomRoles(int userId, string token, string condition, int start, int count)
        {
            return client.GetRoomRoles(application_Id, userId, token, condition, start, count);
        }

        public int GetRoomRoleCount(int userId, string token, string condition)
        {
            return client.GetRoomRoleCount(application_Id, userId, token, condition);
        }

        public void UpdateRoomRole(int userId, string token, RoomRole rr)
        {
            client.UpdateRoomRole(application_Id, userId, token, rr);
        }

        public string Login(int userId, string password)
        {
            return client.Login(application_Id, userId, password);
        }

        public void LogOff(int userId)
        {
            client.LogOff(application_Id, userId);
        }
        public void KeepAlive()
        {
            client.KeepAlive();
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.Application> GetApplications(int userId, string token, string condition, int start, int count)
        {
            return client.GetApplications(application_Id, userId, token, condition, start, count);
        }

		public System.Collections.Generic.List<YoYoStudio.Model.Core.Application> GetApplicationsForCommand(int userId, string token, int cmdid)
		{
			return client.GetApplicationsForCommand(application_Id, userId, token, cmdid);
		}

        public YoYoStudio.Model.Core.Application AddApplication(int userId, string token, YoYoStudio.Model.Core.Application application)
        {
            return client.AddApplication(application_Id, userId, token, application);
        }

        public void DeleteApplication(int userId, string token, int id)
        {
            client.DeleteApplication(application_Id, userId, token, id);
        }

        public void UpdateApplication(int userId, string token, YoYoStudio.Model.Core.Application application)
        {
            client.UpdateApplication(application_Id, userId, token, application);
        }

        public YoYoStudio.Model.Core.Application GetApplication(int userId, string token, int id)
        {
            return client.GetApplication(application_Id, userId, token, id);
        }

        public int GetApplicationCount(int userId, string token, string condition)
        {
            return client.GetApplicationCount(application_Id, userId, token, condition);
        }

        public void DeleteCommand(int userId, string token, int id)
        {
            client.DeleteCommand(application_Id, userId, token, id);
        }

        public void UpdateCommand(int userId, string token, YoYoStudio.Model.Core.Command command)
        {
            client.UpdateCommand(application_Id, userId, token, command);
        }

        public YoYoStudio.Model.Core.Command AddCommand(int userId, string token, YoYoStudio.Model.Core.Command command)
        {
            return client.AddCommand(application_Id, userId, token, command);
        }

        public YoYoStudio.Model.Core.Command GetCommand(int userId, string token, int id)
        {
            return client.GetCommand(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.Command> GetCommands(int userId, string token, string condition, int start, int count)
        {
            return client.GetCommands(application_Id, userId, token, condition, start, count);
        }

        public int GetCommandCount(int userId, string token, string condition)
        {
            return client.GetCommandCount(application_Id, userId, token, condition);
        }

        public void DeleteRole(int userId, string token, int id)
        {
            client.DeleteRole(application_Id, userId, token, id);
        }

        public void UpdateRole(int userId, string token, YoYoStudio.Model.Core.Role role)
        {
            client.UpdateRole(application_Id, userId, token, role);
        }

        public YoYoStudio.Model.Core.Role AddRole(int userId, string token, YoYoStudio.Model.Core.Role role)
        {
            return client.AddRole(application_Id, userId, token, role);
        }

        public YoYoStudio.Model.Core.Role GetRole(int userId, string token, int id)
        {
            return client.GetRole(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.Role> GetRoles(int userId, string token, string condition, int start, int count)
        {
            return client.GetRoles(application_Id, userId, token, condition, start, count);
        }

        public int GetRoleCount(int userId, string token, string condition)
        {
            return client.GetRoleCount(application_Id, userId, token, condition);
        }

        public void DeleteRoleCommand(int userId, string token, int id)
        {
            client.DeleteRoleCommand(application_Id, userId, token, id);
        }

        public void UpdateRoleCommand(int userId, string token, YoYoStudio.Model.Core.RoleCommand roleCommand)
        {
            client.UpdateRoleCommand(application_Id, userId, token, roleCommand);
        }

        public YoYoStudio.Model.Core.RoleCommand AddRoleCommand(int userId, string token, YoYoStudio.Model.Core.RoleCommand roleCommand)
        {
            return client.AddRoleCommand(application_Id, userId, token, roleCommand);
        }

        public YoYoStudio.Model.Core.RoleCommand GetRoleCommand(int userId, string token, int id)
        {
            return client.GetRoleCommand(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.RoleCommand> GetRoleCommands(int userId, string token, string condition, int start, int count)
        {
            return client.GetRoleCommands(application_Id, userId, token, condition, start, count);
        }

        public int GetRoleCommandCount(int userId, string token, string condition)
        {
            return client.GetRoleCommandCount(application_Id, userId, token, condition);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.RoleCommandView> GetRoleCommandsForUser(int userId, string token, int uid)
        {
            return client.GetRoleCommandsForUser(application_Id, userId, token, uid);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.RoleCommandView> GetRoleCommandViews(int userId, string token, string condition, int start, int count)
        {
            return client.GetRoleCommandViews(application_Id, userId, token, condition, start, count);
        }

        public int GetRoleCommandViewCount(int userId, string token, string condition)
        {
            return client.GetRoleCommandViewCount(application_Id, userId, token, condition);
        }

        public YoYoStudio.Model.Core.RoleCommandView GetRoleCommandView(int userId, string token, int id)
        {
            return client.GetRoleCommandView(application_Id, userId, token, id);
        }

        public bool HasCommand(int userId,int cmdTagetAppId, string token, int cmdId, int targetRoleId)
        {
            return client.HasCommand(application_Id, userId,cmdTagetAppId, token, cmdId, targetRoleId);
        }

        public void DeleteRoomGroup(int userId, string token, int id)
        {
            client.DeleteRoomGroup(application_Id, userId, token, id);
        }

        public void UpdateRoomGroup(int userId, string token, YoYoStudio.Model.Chat.RoomGroup roomGroup)
        {
            client.UpdateRoomGroup(application_Id, userId, token, roomGroup);
        }

        public YoYoStudio.Model.Chat.RoomGroup AddRoomGroup(int userId, string token, YoYoStudio.Model.Chat.RoomGroup roomGroup)
        {
            return client.AddRoomGroup(application_Id, userId, token, roomGroup);
        }

        public YoYoStudio.Model.Chat.RoomGroup GetRoomGroup(int userId, string token, int id)
        {
            return client.GetRoomGroup(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Chat.RoomGroup> GetRoomGroups(int userId, string token, string condition, int start, int count)
        {
            return client.GetRoomGroups(application_Id, userId, token, condition, start, count);
        }

        public int GetRoomGroupCount(int userId, string token, string condition)
        {
            return client.GetRoomGroupCount(application_Id, userId, token, condition);
        }

        public void DeleteRoom(int userId, string token, int id)
        {
            client.DeleteRoom(application_Id, userId, token, id);
        }

        public void UpdateRoom(int userId, string token, YoYoStudio.Model.Chat.Room room)
        {
            client.UpdateRoom(application_Id, userId, token, room);
        }

        public YoYoStudio.Model.Chat.Room AddRoom(int userId, string token, YoYoStudio.Model.Chat.Room room)
        {
            return client.AddRoom(application_Id, userId, token, room);
        }

        public YoYoStudio.Model.Chat.Room GetRoom(int userId, string token, int id)
        {
            return client.GetRoom(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Chat.Room> GetRooms(int userId, string token, string condition, int start, int count)
        {
            return client.GetRooms(application_Id, userId, token, condition, start, count);
        }

        public int GetRoomCount(int userId, string token, string condition)
        {
            return client.GetRoomCount(application_Id, userId, token, condition);
        }

        public void AssignRoomsToAgent(int userId, string token, int startId, int endId, int agentId)
        {
            client.AssignRoomsToAgent(application_Id, userId, token, startId, endId, agentId);
        }

        public YoYoStudio.Model.Chat.RoomRole AddRoomRole(int userId, string token, YoYoStudio.Model.Chat.RoomRole roomRole)
        {
            return client.AddRoomRole(application_Id, userId, token, roomRole);
        }

        public void DeleteRoomRole(int userId, string token, int room_Id, int user_Id, int role_Id)
        {
            client.DeleteRoomRole(application_Id, userId, token, room_Id, user_Id, role_Id);
        }

        
        public YoYoStudio.Model.Chat.RoomRole GetRoomRole(int userId, string token, int room_Id, int user_Id, int role_Id)
        {
            return client.GetRoomRole(application_Id, userId, token, room_Id, user_Id, role_Id);
        }

        public void DeleteUser(int userId, string token, int id)
        {
            client.DeleteUser(application_Id, userId, token, id);
        }

        public void UpdateUser(int userId, string token, YoYoStudio.Model.Core.User user)
        {
            client.UpdateUser(application_Id, userId, token, user);
        }

        public YoYoStudio.Model.Core.User AddUser(int userId, string token, YoYoStudio.Model.Core.User user)
        {
            return client.AddUser(application_Id, userId, token, user);
        }

        public YoYoStudio.Model.Core.User GetUser(int userId, string token, int id)
        {
            return client.GetUser(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.User> GetUsers(int userId, string token, string condition, int start, int count)
        {
            return client.GetUsers(application_Id, userId, token, condition, start, count);
        }

        public int GetUserCount(int userId, string token, string condition)
        {
            return client.GetUserCount(application_Id, userId, token, condition);
        }

        public string ResetPassword(int userId, string token, int id)
        {
            return client.ResetPassword(application_Id, userId, token, id);
        }

        public void DeleteGiftGroup(int userId, string token, int id)
        {
            client.DeleteGiftGroup(application_Id, userId, token, id);
        }

        public void UpdateGiftGroup(int userId, string token, YoYoStudio.Model.Chat.GiftGroup giftGroup)
        {
            client.UpdateGiftGroup(application_Id, userId, token, giftGroup);
        }

        public YoYoStudio.Model.Chat.GiftGroup AddGiftGroup(int userId, string token, YoYoStudio.Model.Chat.GiftGroup giftGroup)
        {
            return client.AddGiftGroup(application_Id, userId, token, giftGroup);
        }

        public YoYoStudio.Model.Chat.GiftGroup GetGiftGroup(int userId, string token, int id)
        {
            return client.GetGiftGroup(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Chat.GiftGroup> GetGiftGroups(int userId, string token, string condition, int start, int count)
        {
            return client.GetGiftGroups(application_Id, userId, token, condition, start, count);
        }

        public int GetGiftGroupCount(int userId, string token, string condition)
        {
            return client.GetGiftGroupCount(application_Id, userId, token, condition);
        }

        public void DeleteGift(int userId, string token, int id)
        {
            client.DeleteGift(application_Id, userId, token, id);
        }

        public void UpdateGift(int userId, string token, YoYoStudio.Model.Chat.Gift gift)
        {
            client.UpdateGift(application_Id, userId, token, gift);
        }

        public YoYoStudio.Model.Chat.Gift AddGift(int userId, string token, YoYoStudio.Model.Chat.Gift gift)
        {
            return client.AddGift(application_Id, userId, token, gift);
        }

        public YoYoStudio.Model.Chat.Gift GetGift(int userId, string token, int id)
        {
            return client.GetGift(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Chat.Gift> GetGifts(int userId, string token, string condition, int start, int count)
        {
            return client.GetGifts(application_Id, userId, token, condition, start, count);
        }

        public int GetGiftCount(int userId, string token, string condition)
        {
            return client.GetGiftCount(application_Id, userId, token, condition);
        }

        public void DeleteImage(int userId, string token, int id)
        {
            client.DeleteImage(application_Id, userId, token, id);
        }

        public void UpdateImage(int userId, string token, YoYoStudio.Model.Core.Image image)
        {
            client.UpdateImage(application_Id, userId, token, image);
        }

        public void AddImage(int userId, string token, YoYoStudio.Model.Core.Image image)
        {
            client.AddImage(application_Id, userId, token, image);
        }

        public YoYoStudio.Model.Core.Image GetImage(int userId, string token, int id)
        {
            return client.GetImage(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.ImageWithoutBody> GetImages(int userId, string token, string condition, int start, int count)
        {
            return client.GetImages(application_Id, userId, token, condition, start, count);
        }

        public int GetImageCount(int userId, string token, string condition)
        {
            return client.GetImageCount(application_Id, userId, token, condition);
        }

        public int AddUserIdLists(int userId, string token, int start, int end, int optUserId, int ownerId, int roleId, int applicationId, string password, bool isDirect)
        {
            return client.AddUserIdLists(application_Id, userId, token, start, end, optUserId, ownerId, roleId, applicationId, password, isDirect);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.UserIdList> GetUserIdLists(int userId, string token, int userIdAppid, string condition, int start, int count)
        {
            return client.GetUserIdLists(application_Id, userId, token, userIdAppid, condition, start, count);
        }

        public int GetUserIdListCount(int userId, string token,int userIdAppid,string condition)
        {
            return client.GetUserIdListCount(application_Id, userId, token, userIdAppid, condition);
        }

        public void UpdateUserIdList(int userId, string token, YoYoStudio.Model.Core.UserIdList idlist)
        {
            client.UpdateUserIdList(application_Id, userId, token, idlist);
        }

        public void DeleteUserIdList(int userId, string token, int userIdListAppid, int userid1)
        {
            client.DeleteUserIdList(application_Id, userId, token, userIdListAppid, userid1);
        }

        public YoYoStudio.Model.Core.UserIdList AddUserIdList(int userId, string token, YoYoStudio.Model.Core.UserIdList idlist)
        {
            return client.AddUserIdList(application_Id, userId, token, idlist);
        }

        public int AssignAgentUserIds(int userId, string token, int userIdListAppid, int start, int end, int agent)
        {
            return client.AssignAgentUserIds(application_Id, userId, token, userIdListAppid, start, end, agent);
        }

        public YoYoStudio.Model.Core.UserApplicationInfo GetUserInfo(int userId, string token, int id, int userInfoAppid)
        {
            return client.GetUserInfo(application_Id, userId, token, id, userInfoAppid);
        }

        public void DeleteUserInfo(int userId, string token, int id, int userInfoAppid)
        {
            client.DeleteUserInfo(application_Id, userId, token, id, userInfoAppid);
        }

        public YoYoStudio.Model.Core.UserApplicationInfo AddUserInfo(int userId, string token, YoYoStudio.Model.Core.UserApplicationInfo info)
        {
            return client.AddUserInfo(application_Id, userId, token, info);
        }

        public void UpdateUserInfo(int userId, string token, YoYoStudio.Model.Core.UserApplicationInfo info)
        {
            client.UpdateUserInfo(application_Id, userId, token, info);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.UserApplicationInfo> GetUserInfos(int userId, string token, string condition, int start, int count)
        {
            return client.GetUserInfos(application_Id, userId, token, condition, start, count);
        }

        public int GetUserInfoCount(int userId, string token, string condition)
        {
            return client.GetUserInfoCount(application_Id, userId, token, condition);
        }

        public bool Deposit(int userId, string token, int depositAppid, int id, int money, bool isAgent)
        {
            return client.Deposit(application_Id, userId, token, depositAppid, id, money, isAgent);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.DepositHistory> GetDepositHistories(int userId, string token, string condition, int start, int count)
        {
            return client.GetDepositHistories(application_Id, userId, token, condition, start, count);
        }

        public int GetDepositHistoryCount(int userId, string token, string condition)
        {
            return client.GetDepositHistoryCount(application_Id, userId, token, condition);
        }

        public void DeleteDepositHistory(int userId, string token, int id)
        {
            client.DeleteDepositHistory(application_Id, userId, token, id);
        }

        public void ScoreDeposit(int userId, string token, int depositAppid, int id, int score)
        {
            client.ScoreDeposit(application_Id, userId, token, depositAppid, id, score);
        }

        public void ScoreExchange(int userId, string token, int scoreExchangeAppid, int id, int score, int money)
        {
            client.ScoreExchange(application_Id, userId, token, scoreExchangeAppid, id, score, money);
        }

        public System.Collections.Generic.List<ExchangeHistory> GetExchangeHistories(int userId, string token, string condition, bool exchangeCache, int start, int count)
        {
            return client.GetExchangeHistories(application_Id, userId, token, condition,exchangeCache, start, count);
        }

        public int GetExchangeHistoryCount(int userId, string token, string condtion,bool exchangeCache)
        {
            return client.GetExchangeHistoryCount(application_Id, userId, token, condtion, exchangeCache);
        }

        public void DeleteExchangeHistory(int userId, string token, int id)
        {
            client.DeleteExchangeHistory(application_Id, userId, token, id);
        }

        public List<ExchangeHistory> GetExchangeHistoryForSettlement(int userId, string token, string condition, int start = -1, int count = -1)
        {
            return client.GetExchangeHistoryForSettlement(application_Id, userId, token, condition, start, count);
        }

        public void SettlementExchange(int userId, string token, ExchangeHistory history)
        {
            client.SettlementExchange(application_Id, userId, token, history);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.BlockList> GetBlockLists(int userId, string token, string condition, int start, int count)
        {
            return client.GetBlockLists(application_Id, userId, token, condition, start, count);
        }

        public int GetBlockListCount(int userId, string token, string condition)
        {
            return client.GetBlockListCount(application_Id, userId, token, condition);
        }

        public YoYoStudio.Model.Core.BlockList AddBlockList(int userId, string token, YoYoStudio.Model.Core.BlockList blockL)
        {
            return client.AddBlockList(application_Id, userId, token, blockL);
        }

        public void DeleteBlockList(int userId, string token, int id)
        {
            client.DeleteBlockList(application_Id, userId, token, id);
        }

        public void UpdateBlockList(int userId, string token, YoYoStudio.Model.Core.BlockList blockL)
        {
            client.UpdateBlockList(application_Id, userId, token, blockL);
        }

        public YoYoStudio.Model.Core.BlockList GetBlockList(int userId, string token, int id)
        {
            return client.GetBlockList(application_Id, userId, token, id);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.BlockHistory> GetBlockHistory(int userId, string token, string condition, int start, int count)
        {
            return client.GetBlockHistory(application_Id, userId, token, condition, start, count);
        }

        public int GetBlockHistoryCount(int userId, string token, string condition)
        {
            return client.GetBlockHistoryCount(application_Id, userId, token, condition);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.BlockType> GetBlockTypes(int userId, string token, string condition, int start, int count)
        {
            return client.GetBlockTypes(application_Id, userId, token, condition, start, count);
        }

        public int GetBlockTypeCount(int userId, string token, string condition)
        {
            return client.GetBlockTypeCount(application_Id, userId, token, condition);
        }

        public YoYoStudio.Model.Core.BlockType GetBlockType(int userId, string token, int id)
        {
            return client.GetBlockType(application_Id, userId, token, id);
        }

        public YoYoStudio.Model.Core.BlockType AddBlockType(int userId, string token, YoYoStudio.Model.Core.BlockType blockType)
        {
            return client.AddBlockType(application_Id, userId, token, blockType);
        }

        public void DeleteBlockType(int userId, string token, int id)
        {
            client.DeleteBlockType(application_Id, userId, token, id);
        }

        public void UpdateBlockType(int userId, string token, YoYoStudio.Model.Core.BlockType blockType)
        {
            client.UpdateBlockType(application_Id, userId, token, blockType);
        }

        public System.Collections.Generic.List<YoYoStudio.Model.Core.ExchangeRate> GetAllExchangeRate(int userId, string token, string condition, int start, int count)
        {
            return client.GetAllExchangeRate(application_Id, userId, token, condition, start, count);
        }

        public int GetExchangeRateCount(int userId, string token, string condition)
        {
            return client.GetExchangeRateCount(application_Id, userId, token, condition);
        }

        public YoYoStudio.Model.Core.ExchangeRate AddExchangeRate(int userId, string token, YoYoStudio.Model.Core.ExchangeRate eRate)
        {
            return client.AddExchangeRate(application_Id, userId, token, eRate);
        }

        public void DeleteExchangeRate(int userId, string token, int id)
        {
            client.DeleteExchangeRate(application_Id, userId, token, id);
        }

        public void UpdateExchangeRate(int userId, string token, YoYoStudio.Model.Core.ExchangeRate eRate)
        {
            client.UpdateExchangeRate(application_Id, userId, token, eRate);
        }

        public ExchangeHistory AddExchangeHistory(int userId, string token, ExchangeHistory history)
        {
            return client.AddExchangeHistory(application_Id, userId, token, history);
        }

        public void CancelExchangeCache(int userid, string token, List<ExchangeHistory> history)
        {
            client.CancelExchangeCache(application_Id, userid, token, history);
        }

        public void SettleExchangeCache(int userId, string token, List<ExchangeHistory> history)
        {
            client.SettleExchangeCache(application_Id, userId, token, history);
        }

        public void ConfirmExchangeCache(int userId, string token, List<ExchangeHistory> history)
        {
            client.ConfirmExchangeCache(application_Id, userId, token, history);
        }

        public void DeleteRoomConfig(int userId, string token, int id)
        {
            client.DeleteRoomConfig(application_Id, userId, token, id);
        }

        public RoomConfig UpdateRoomConfig(int userId, string token, RoomConfig config)
        {
            return client.UpdateRoomConfig(application_Id, userId, token, config);
        }

        public RoomConfig AddRoomConfig(int userId, string token, RoomConfig config)
        {
            return client.AddRoomConfig(application_Id, userId, token, config);
        }

        public RoomConfig GetRoomConfig(int userId, string token, int id)
        {
            return client.GetRoomConfig(application_Id, userId, token, id);
        }

        public List<RoomConfig> GetRoomConfigs(int userId, string token, string condition, int start, int count)
        {
            return client.GetRoomConfigs(application_Id, userId, token, condition,start,count);
        }

        public int GetRoomConfigCount(int userId, string token, string condition)
        {
            return client.GetRoomConfigCount(application_Id, userId, token, condition);
        }

        public User Register(int userId, string account, string pwd, int sex)
        {
            return client.Register(application_Id, userId, account, pwd, sex);
        }

        public void UpdateUserProfileInfo(User user, string token, Image img)
        {
            client.UpdateUserProfileInfo(application_Id, user, token, img);
        }

        public int GetNextAvailableUserId(int userId, string token, int userIdAppid, int roleId)
        {
            return client.GetNextAvailableUserId(application_Id, userId, token, userIdAppid, roleId);
        }

        public List<BlockList> GetUserBlockList(int userId, string token,string content)
        {
            return client.GetUserBlockList(application_Id, userId, token, content);
        }

        public List<MusicInfo> GetMusics(int appid, int userId, string token)
        {
            return client.GetMusics(appid, userId, token);

        }

        public void DeleteMusics(int appid, int userId, string token, List<MusicInfo> toDelete)
        {
            client.DeleteMusics(appid, userId, token, toDelete);
        }

        public void UploadMusics(int appid, int userId, string token, List<Byte[]> toUpload)
        {
            client.UploadMusics(appid, userId, token, toUpload);
        }
    }
}
