
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
namespace YoYoStudio.ChatService.Client
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IChatService", CallbackContract = typeof(IChatServiceCallback), SessionMode = System.ServiceModel.SessionMode.Required)]
	public interface IChatService
	{
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/CanUserLogin", ReplyAction = "http://tempuri.org/IChatService/CanUserLoginResponse")]
        int CanUserLogin(int userId, string macAddress);

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/Login", ReplyAction = "http://tempuri.org/IChatService/LoginResponse")]
		YoYoStudio.Model.Core.User Login(int userId, string password, string macAddress);
        
		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetOnlineUserCount", ReplyAction = "http://tempuri.org/IChatService/GetOnlineUserCountResponse")]
		int GetOnlineUserCount();

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsTerminating = true, Action = "http://tempuri.org/IChatService/LogOff")]
		void LogOff();

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IChatService/KeepAlive")]
		void KeepAlive();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/RoomLogin", ReplyAction = "http://tempuri.org/IChatService/RoomLoginResponse")]
        bool RoomLogin();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetRoomGroups", ReplyAction = "http://tempuri.org/IChatService/GetRoomGroupsResponse")]
		YoYoStudio.Model.Chat.RoomGroup[] GetRoomGroups();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetRooms", ReplyAction = "http://tempuri.org/IChatService/GetRoomsResponse")]
		YoYoStudio.Model.Chat.Room[] GetRooms();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetRoles", ReplyAction = "http://tempuri.org/IChatService/GetRolesResponse")]
		YoYoStudio.Model.Core.Role[] GetRoles();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetRoleCommands", ReplyAction = "http://tempuri.org/IChatService/GetRoleCommandsResponse")]
		YoYoStudio.Model.Core.RoleCommandView[] GetRoleCommands();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetGiftGroups", ReplyAction = "http://tempuri.org/IChatService/GetGiftGroupsResponse")]
		YoYoStudio.Model.Chat.GiftGroup[] GetGiftGroups();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetGifts", ReplyAction = "http://tempuri.org/IChatService/GetGiftsResponse")]
		YoYoStudio.Model.Chat.Gift[] GetGifts();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetCommands", ReplyAction = "http://tempuri.org/IChatService/GetCommandsResponse")]
		YoYoStudio.Model.Core.Command[] GetCommands();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetImages", ReplyAction = "http://tempuri.org/IChatService/GetImagesResponse")]
		YoYoStudio.Model.Core.ImageWithoutBody[] GetImages(int start, int count);

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetImageCount", ReplyAction = "http://tempuri.org/IChatService/GetImageCountResponse")]
		int GetImageCount();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/Register", ReplyAction = "http://tempuri.org/IChatService/RegisterResponse")]
        YoYoStudio.Model.Core.User Register(int userId, string account, string password, int gender);

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetNextAvailableUserId", ReplyAction = "http://tempuri.org/IChatService/GetNextAvailableUserIdResponse")]
		int GetNextAvailableUserId(int roleId);

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetRoomRoleCount", ReplyAction = "http://tempuri.org/IChatService/GetRoomRoleCountResponse")]
		int GetRoomRoleCount();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetRoomRoles", ReplyAction = "http://tempuri.org/IChatService/GetRoomRolesResponse")]
		YoYoStudio.Model.Chat.RoomRole[] GetRoomRoles(int start, int count);

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/UpdateUser", ReplyAction = "http://tempuri.org/IChatService/UpdateUserResponse")]
		bool UpdateUser(YoYoStudio.Model.Core.User user);

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/UpdateUserHeaderImange", ReplyAction = "http://tempuri.org/IChatService/UpdateUserHeaderImangeResponse")]
		bool UpdateUserHeaderImange(YoYoStudio.Model.Core.Image theImage);
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetImage", ReplyAction = "http://tempuri.org/IChatService/GetImageResponse")]
        Image GetImage(int imgId);
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetUserInfo", ReplyAction = "http://tempuri.org/IChatService/GetUserInfoResponse")]
        UserApplicationInfo GetUserInfo(int userId);
		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/SendGift", ReplyAction = "http://tempuri.org/IChatService/SendGiftResponse")]
		SendGiftResult SendGift(int roomId, int senderId, int receiverId, int giftId, int count);
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/ExecuteCommand", ReplyAction = "http://tempuri.org/IChatService/ExecuteCommandResponse")]
        bool ExecuteCommand(int roomId, int cmdId, int sourceUserId, int targetUserId);
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetBlockTypes", ReplyAction = "http://tempuri.org/IChatService/GetBlockTypesResponse")]
        BlockType[] GetBlockTypes();
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetCacheVersion", ReplyAction = "http://tempuri.org/IChatService/GetCacheVersionResponse")]
        long GetCacheVersion();
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/SendHornMessage", ReplyAction = "http://tempuri.org/IChatService/SendHornMessageResponse")]
        MessageResult SendHornMessage(int roomId, int senderId, int cmdId);
		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/UpdateRoomOnlineUserCount", ReplyAction = "http://tempuri.org/IChatService/UpdateRoomOnlineUserCountResponse")]
		void UpdateRoomOnlineUserCount(System.Collections.Generic.Dictionary<int, int> roomUsersCount);
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetRoomOnlineUserCount", ReplyAction = "http://tempuri.org/IChatService/GetRoomOnlineUserCountResponse")]
		System.Collections.Generic.Dictionary<int, int> GetRoomOnlineUserCount();
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/GetExchangeRates", ReplyAction = "http://tempuri.org/IChatService/GetExchangeRatesResponse")]
        System.Collections.Generic.List<ExchangeRate> GetExchangeRates();
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IChatService/ScoreExchange", ReplyAction = "http://tempuri.org/IChatService/ScoreExchangeResponse")]
        bool ScoreExchange(int userId, int scoreToExchange, int moneyToGet);
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public interface IChatServiceCallback
	{

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IChatService/UserLoggedIn")]
		void UserLoggedIn(YoYoStudio.Model.Core.User user);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IChatService/UserLoggedOff")]
		void UserLoggedOff(int userId);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IChatService/Receive")]
		void Receive(YoYoStudio.Model.Message message);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IChatService/RoomChanged")]
		void RoomChanged(YoYoStudio.Model.Chat.Room room);

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IChatService/UserRelogin")]
        void UserRelogin();

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IChatService/RoomRelogin")]
        void RoomRelogin();
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public interface IChatServiceChannel : IChatService, System.ServiceModel.IClientChannel
	{
	}

	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public partial class ChatServiceClient : System.ServiceModel.DuplexClientBase<IChatService>, IChatService
	{

		public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance) :
			base(callbackInstance)
		{
		}

		public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
			base(callbackInstance, endpointConfigurationName)
		{
		}

		public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
			base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
			base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
			base(callbackInstance, binding, remoteAddress)
		{
		}

		public YoYoStudio.Model.Core.User Login(int userId, string password, string macAddress)
		{
			return base.Channel.Login(userId, password, macAddress);
		}

		public int GetOnlineUserCount()
		{
			return base.Channel.GetOnlineUserCount();
		}

		public void LogOff()
		{
			base.Channel.LogOff();
		}

		public void KeepAlive()
		{
			base.Channel.KeepAlive();
		}

        public bool RoomLogin()
		{
            return base.Channel.RoomLogin();
		}

		public YoYoStudio.Model.Chat.RoomGroup[] GetRoomGroups()
		{
			return base.Channel.GetRoomGroups();
		}

		public YoYoStudio.Model.Chat.Room[] GetRooms()
		{
			return base.Channel.GetRooms();
		}

		public YoYoStudio.Model.Core.Role[] GetRoles()
		{
			return base.Channel.GetRoles();
		}

		public YoYoStudio.Model.Core.RoleCommandView[] GetRoleCommands()
		{
			return base.Channel.GetRoleCommands();
		}

		public YoYoStudio.Model.Chat.GiftGroup[] GetGiftGroups()
		{
			return base.Channel.GetGiftGroups();
		}

		public YoYoStudio.Model.Chat.Gift[] GetGifts()
		{
			return base.Channel.GetGifts();
		}

		public YoYoStudio.Model.Core.Command[] GetCommands()
		{
			return base.Channel.GetCommands();
		}

		public YoYoStudio.Model.Core.ImageWithoutBody[] GetImages(int start, int count)
		{
			return base.Channel.GetImages(start, count);
		}

		public int GetImageCount()
		{
			return base.Channel.GetImageCount();
		}

		public YoYoStudio.Model.Core.User Register(int userId, string account, string password, int gender)
		{
            return base.Channel.Register(userId, account, password, gender);
		}

        public int GetNextAvailableUserId(int roleId)
		{
            return base.Channel.GetNextAvailableUserId(roleId);
		}

		public int GetRoomRoleCount()
		{
			return base.Channel.GetRoomRoleCount();
		}

		public YoYoStudio.Model.Chat.RoomRole[] GetRoomRoles(int start, int count)
		{
			return base.Channel.GetRoomRoles(start, count);
		}

		public bool UpdateUser(YoYoStudio.Model.Core.User user)
		{
			return base.Channel.UpdateUser(user);
		}

		public bool UpdateUserHeaderImange(YoYoStudio.Model.Core.Image theImage)
		{
			return base.Channel.UpdateUserHeaderImange(theImage);
		}

        public Image GetImage(int imgId)
        {
            return base.Channel.GetImage(imgId);
        }


        public UserApplicationInfo GetUserInfo(int userId)
        {
            return base.Channel.GetUserInfo(userId);
        }


		public SendGiftResult SendGift(int roomId, int senderId, int receiverId, int giftId, int count)
		{
			return base.Channel.SendGift(roomId, senderId, receiverId, giftId, count);
		}


        public MessageResult SendHornMessage(int roomId, int senderId, int cmdId)
        {
            return base.Channel.SendHornMessage(roomId, senderId, cmdId);
        }


        public bool ExecuteCommand(int roomId, int cmdId, int sourceUserId, int targetUserId)
        {
            return base.Channel.ExecuteCommand(roomId, cmdId, sourceUserId, targetUserId);
        }

        public int CanUserLogin(int userId, string macAddress)
        {
            return base.Channel.CanUserLogin(userId, macAddress);
        }

        public BlockType[] GetBlockTypes()
        {
            return base.Channel.GetBlockTypes();
        }


        public long GetCacheVersion()
        {
            return base.Channel.GetCacheVersion();
        }

		public void UpdateRoomOnlineUserCount(System.Collections.Generic.Dictionary<int, int> roomUsersCount)
		{
			base.Channel.UpdateRoomOnlineUserCount(roomUsersCount);
		}


		public System.Collections.Generic.Dictionary<int, int> GetRoomOnlineUserCount()
		{
			return base.Channel.GetRoomOnlineUserCount();
		}

        public System.Collections.Generic.List<ExchangeRate> GetExchangeRates()
        {
            return base.Channel.GetExchangeRates();
        }

        public bool ScoreExchange(int userId, int scoreToExchange, int moneyToGet)
        {
            return base.Channel.ScoreExchange(userId, scoreToExchange, moneyToGet);
        }
	}

}