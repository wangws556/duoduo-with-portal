using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;

namespace YoYoStudio.ChatService.Library
{
	[ServiceContract(CallbackContract=typeof(IChatServiceCallback),SessionMode=SessionMode.Required)]
	public interface IChatService
	{
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        int CanUserLogin(int userId, string macAddress);
        [OperationContract(IsOneWay = false, IsInitiating = false)]
		User Login(int userId, string password, string macAddress);
		[OperationContract(IsOneWay = false, IsInitiating = true)]
        int GetOnlineUserCount();
        [OperationContract(IsOneWay = true, IsTerminating = true)]
        void LogOff();
		[OperationContract(IsOneWay = true)]
        void KeepAlive();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        bool RoomLogin();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<RoomGroup> GetRoomGroups();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<Room> GetRooms();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<Role> GetRoles();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<RoleCommandView> GetRoleCommands();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<GiftGroup> GetGiftGroups();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<Gift> GetGifts();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<Command> GetCommands();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        List<ImageWithoutBody> GetImages(int start, int count);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        int GetImageCount();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        User Register(int userId, string account, string password, int gender);
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        int GetRoomRoleCount();
        [OperationContract(IsOneWay=false,IsInitiating=true)]
        List<RoomRole> GetRoomRoles(int start, int count);
        [OperationContract(IsOneWay = false)]
        bool UpdateUser(User user);
        [OperationContract(IsOneWay = false)]
        bool UpdateUserHeaderImange(Image theImage);
        [OperationContract(IsOneWay = false)]
        int GetNextAvailableUserId(int roleId);
        [OperationContract(IsOneWay = false)]
        Image GetImage(int imgId);
        [OperationContract(IsOneWay = false)]
        UserApplicationInfo GetUserInfo(int userId);
		[OperationContract(IsOneWay = false)]
		SendGiftResult SendGift(int roomId, int senderId, int receiverId, int giftId, int count);
        [OperationContract(IsOneWay=false)]
        bool ExecuteCommand(int roomId, int cmdId,int sourceUserId, int targetUserId);
        [OperationContract(IsOneWay = false)]
        List<BlockType> GetBlockTypes();
        [OperationContract(IsOneWay = false)]
        long GetCacheVersion();
        [OperationContract(IsOneWay = false)]
        MessageResult SendHornMessage(int roomId, int senderId, int cmdId);
		[OperationContract(IsOneWay = true)]
		void UpdateRoomOnlineUserCount(Dictionary<int, int> roomUsersCount);
		[OperationContract(IsOneWay = false)]
		Dictionary<int, int> GetRoomOnlineUserCount();
        [OperationContract(IsOneWay = false)]
        List<ExchangeRate> GetExchangeRates();
        [OperationContract(IsOneWay = false)]
        bool ScoreExchange(int userId,int scoreToExchange, int moneyToGet);
	}

	public interface IChatServiceCallback
	{
		[OperationContract(IsOneWay=true)]
        void UserLoggedIn(User user);
		[OperationContract(IsOneWay = true)]
        void UserLoggedOff(int userId);
		[OperationContract(IsOneWay = true)]
        void Receive(Message message);
        [OperationContract(IsOneWay = true)]
        void RoomChanged(Room room);
        [OperationContract(IsOneWay = true)]
        void UserRelogin();
        [OperationContract(IsOneWay = true)]
        void RoomRelogin();
	}
}
