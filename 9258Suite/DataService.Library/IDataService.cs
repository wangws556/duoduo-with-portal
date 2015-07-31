using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Chat;
using YoYoStudio.Exceptions;
using YoYoStudio.Model;
using YoYoStudio.Model.Media;

namespace YoYoStudio.DataService.Library
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]    
    public interface IDataService
    {
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        string Login(int appid, int userId, string password);
        [OperationContract]
        void LogOff(int appid, int userId);

        [OperationContract]
        void KeepAlive();

        #region Application

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<Application> GetApplications(int appid, int userId, string token, string condition = "",int start=-1, int count=-1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<Application> GetApplicationsForCommand(int appid, int userId, string token, int cmdid);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Application AddApplication(int appid, int userId, string token, Application application);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteApplication(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateApplication(int appid, int userId, string token, Application application);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Application GetApplication(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetApplicationCount(int appid, int userId, string token, string condition = "");

        #endregion

        #region Commands

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteCommand(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateCommand(int appid, int userId, string token, Command command);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Command AddCommand(int appid, int userId, string token, Command command);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Command GetCommand(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<Command> GetCommands(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetCommandCount(int appid, int userId, string token, string condition = "");

        #endregion

        #region Role

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteRole(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateRole(int appid, int userId, string token, Role role);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Role AddRole(int appid, int userId, string token, Role role);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Role GetRole(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<Role> GetRoles(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetRoleCount(int appid, int userId, string token, string condition = "");

        #endregion

		#region RoleCommand

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteRoleCommand(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateRoleCommand(int appid, int userId, string token, RoleCommand roleCommand);

		[OperationContract]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoleCommand AddRoleCommand(int appid, int userId, string token, RoleCommand roleCommand);

        [OperationContract]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoleCommand GetRoleCommand(int appid, int userId, string token, int id);

        [OperationContract]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<RoleCommand> GetRoleCommands(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetRoleCommandCount(int appid, int userId, string token, string condition = "");

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<RoleCommandView> GetRoleCommandsForUser(int appid, int userId, string token, int uid);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<RoleCommandView> GetRoleCommandViews(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetRoleCommandViewCount(int appid, int userId, string token, string condition = "");

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoleCommandView GetRoleCommandView(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        bool HasCommand(int appid, int cmdTargetAppId, int userId, string token, int cmdId, int targetRoleId);

		#endregion

		#region Room Group

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteRoomGroup(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateRoomGroup(int appid, int userId, string token, RoomGroup roomGroup);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoomGroup AddRoomGroup(int appid, int userId, string token, RoomGroup roomGroup);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoomGroup GetRoomGroup(int appid, int userId, string token, int id);

		[OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<RoomGroup> GetRoomGroups(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetRoomGroupCount(int appid, int userId, string token, string condition = "");

		#endregion

		#region Room

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteRoom(int appid, int userId, string token, int id);

		[OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Room UpdateRoom(int appid, int userId, string token, Room room);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Room AddRoom(int appid, int userId, string token, Room room);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Room GetRoom(int appid, int userId, string token, int id);

		[OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<Room> GetRooms(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetRoomCount(int appid, int userId, string token, string condition = "");

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void AssignRoomsToAgent(int appid, int userId, string token, int startId, int endId, int agentId);

		#endregion

        #region RoomConfig

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteRoomConfig(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoomConfig UpdateRoomConfig(int appid, int userId, string token, RoomConfig config);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoomConfig AddRoomConfig(int appid, int userId, string token, RoomConfig room);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoomConfig GetRoomConfig(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<RoomConfig> GetRoomConfigs(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetRoomConfigCount(int appid, int userId, string token, string condition = "");
        
        #endregion

        #region RoomRole

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoomRole AddRoomRole(int appid, int userId, string token, RoomRole roomRole);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteRoomRole(int appid, int userId, string token, int room_Id, int user_Id, int role_Id);
        
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        RoomRole GetRoomRole(int appid, int userId, string token, int room_Id, int user_Id, int role_Id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<RoomRole> GetRoomRoles(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetRoomRoleCount(int appid, int userId, string token,string condition);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateRoomRole(int appid, int userId, string token, RoomRole rr);

        #endregion

        #region User

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteUser(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateUser(int appid, int userId, string token, User user);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        User AddUser(int appid, int userId, string token, User user);

		[OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        User GetUser(int appid, int userId, string token, int id);

		[OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<User> GetUsers(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetUserCount(int appid, int userId, string token, string condition = "");

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        string ResetPassword(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        User Register(int appid, int userId, string account, string pwd, int sex);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateUserProfileInfo(int appid, User user, string token, Image img);

		#endregion

        #region GiftGroup

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteGiftGroup(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateGiftGroup(int appid, int userId, string token, GiftGroup giftGroup);
        [OperationContract(IsOneWay=false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        GiftGroup AddGiftGroup(int appid, int userId, string token, GiftGroup giftGroup);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        GiftGroup GetGiftGroup(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<GiftGroup> GetGiftGroups(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetGiftGroupCount(int appid, int userId, string token, string condition = "");

        #endregion

        #region Gift

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteGift(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateGift(int appid, int userId, string token, Gift gift);
        [OperationContract(IsOneWay=false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Gift AddGift(int appid, int userId, string token, Gift gift);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Gift GetGift(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<Gift> GetGifts(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetGiftCount(int appid, int userId, string token, string condition = "");        
        #endregion

        #region Image
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteImage(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateImage(int appid, int userId, string token, Image image);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void AddImage(int appid, int userId, string token, Image image);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        Image GetImage(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<ImageWithoutBody> GetImages(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetImageCount(int appid, int userId, string token, string condition = "");
        #endregion

        #region UserIdList

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int AddUserIdLists(int appid, int userId, string token, int start, int end, int optUserId, int ownerId, int roleId, int applicationId, string password, bool isDirect);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<UserIdList> GetUserIdLists(int appid, int userId, string token, int userIdAppid, string condition = "", int start = -1, int count = -1);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetUserIdListCount(int appid, int userId, string token, int userIdAppid, string condition = "");
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateUserIdList(int appid, int userId, string token, UserIdList idlist);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteUserIdList(int appid, int userId, string token, int userIdListAppid, int userid);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        UserIdList AddUserIdList(int appid, int userId, string token, UserIdList idlist);
        [OperationContract]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int AssignAgentUserIds(int appid, int userId, string token, int userIdListAppid, int start, int end, int agent);
        [OperationContract]
        int GetNextAvailableUserId(int appid, int userId, string token, int userIdAppid, int roleId);
        #endregion

        #region UserInfo

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        UserApplicationInfo GetUserInfo(int appid, int userId, string token, int id, int userInfoAppid);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteUserInfo(int appid, int userId, string token, int id, int userInfoAppid);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        UserApplicationInfo AddUserInfo(int appid, int userId, string token, UserApplicationInfo info);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateUserInfo(int appid, int userId, string token, UserApplicationInfo info);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<UserApplicationInfo> GetUserInfos(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetUserInfoCount(int appid, int userId, string token, string condition = "");

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        bool Deposit(int appid, int userId, string token, int depositAppid, int id, int money, bool isAgent);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<DepositHistory> GetDepositHistories(int appid, int userId, string token, string condition, int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetDepositHistoryCount(int appid, int userId, string token, string condition);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteDepositHistory(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void ScoreDeposit(int appid, int userId, string token, int depositAppid, int id, int score);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void ScoreExchange(int appid, int userId, string token, int scoreExchangeAppid, int id, int score, int money);

        [OperationContract(IsOneWay=false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<ExchangeHistory> GetExchangeHistories(int appid, int userId, string token, string condition, bool exchangeCache = false,int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetExchangeHistoryCount(int appid, int userId, string token, string condtion, bool exchangeCache = false);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteExchangeHistory(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay=false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<ExchangeHistory> GetExchangeHistoryForSettlement(int appid, int userId, string token, string condition, int start = -1, int count = -1);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void SettlementExchange(int appid, int userId, string token, ExchangeHistory history);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        ExchangeHistory AddExchangeHistory(int appid, int userId, string token, ExchangeHistory history);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void CancelExchangeCache(int appid, int userId, string token, List<ExchangeHistory> history);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void SettleExchangeCache(int appid, int userId, string token, List<ExchangeHistory> history);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void ConfirmExchangeCache(int appid, int userId, string token, List<ExchangeHistory> history);

        #endregion       

        #region Block

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<BlockList> GetBlockLists(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);
        [OperationContract(IsOneWay=false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetBlockListCount(int appid, int userId, string token, string condition = "");
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        BlockList AddBlockList(int appid, int userId, string token, BlockList blockL);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteBlockList(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateBlockList(int appid, int userId, string token, BlockList blockL);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        BlockList GetBlockList(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<BlockList> GetUserBlockList(int appid, int userId, string token, string blockContent);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<BlockHistory> GetBlockHistory(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);
        [OperationContract(IsOneWay=false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetBlockHistoryCount(int appid, int userId, string token, string condition = "");        
      
        #endregion

        #region BlockType

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<BlockType> GetBlockTypes(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetBlockTypeCount(int appid, int userId, string token, string condition = "");
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        BlockType GetBlockType(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        BlockType AddBlockType(int appid, int userId, string token, BlockType blockType);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteBlockType(int appid, int userId, string token, int id);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateBlockType(int appid, int userId, string token, BlockType blockType);

        #endregion

        #region ExchangeRate

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        List<ExchangeRate> GetAllExchangeRate(int appid, int userId, string token, string condition = "", int start = -1, int count = -1);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        int GetExchangeRateCount(int appid, int userId, string token, string condition = "");
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        ExchangeRate AddExchangeRate(int appid, int userId, string token, ExchangeRate eRate);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void DeleteExchangeRate(int appid, int userId, string token, int id);
        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(DatabaseExceptionMsg))]
        void UpdateExchangeRate(int appid, int userId, string token, ExchangeRate eRate);

        #endregion

        #region Musics

        [OperationContract(IsOneWay = false)]
        List<MusicInfo> GetMusics(int appid, int userId, string token);

        [OperationContract(IsOneWay = true)]
        void DeleteMusics(int appid, int userId, string token, List<MusicInfo> toDelete);

        [OperationContract(IsOneWay = true)]
        void UploadMusics(int appid, int userId, string token, List<Byte[]> toUpload);

        #endregion
    }
}
