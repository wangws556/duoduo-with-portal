using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Resource;

namespace YoYoStudio.Model
{
	public class BuiltIns
	{
        static BuiltIns()
        {
            Assembly a = Assembly.GetAssembly(typeof(BuiltIns));
            Stream stream = a.GetManifestResourceStream("YoYoStudio.Model.NotFound.png");
            byte[] data = new byte[stream.Length];
            stream.Read(data,0,(int)stream.Length);
            DefaultImage.TheImage = data;
        }

		#region Applications

		public static Application AllApplication = new Application { Id = 1, Name = Text.AllApplication, IsBuiltIn = true };
		public static Application _9258ChatApplication = new Application { Id = 2, Name = Text._9258ChatApplication, IsBuiltIn = false };
		public static Application BackendApplication = new Application { Id = 3, Name = Text.BackendApplication, IsBuiltIn = true };

		//User added ApplicationId starts from 10001
        public const int UserDefinedApplicationStartId = 10001;

		#endregion

		#region Commands

		public const int BackendCommandType = 1;
		public const int FrontendCommandType = 2;
		public const int UserCommandType = 4;
        public const int NormalCommandType = 8;
		public const int AllCommandType = -1;

		//Command for All aplications
		public static Command AllCommand = new Command { Id = 1, Application_Id = AllApplication.Id, Name = Text.AllCommand, IsBuiltIn = true, CommandType = BackendCommandType };
		public static Command DefaultBackendCommand = new Command { Id = 2, Application_Id = AllApplication.Id, IsBuiltIn = true, CommandType = BackendCommandType, Name = Text.DefaultCommand };
		public static Command DefaultFrontendCommand = new Command { Id = 3, Application_Id = AllApplication.Id, IsBuiltIn = true, CommandType = FrontendCommandType, Name = Text.DefaultCommand };
		public static Command DefaultUserCommand = new Command { Id = 4, Application_Id = AllApplication.Id, IsBuiltIn = true, CommandType = UserCommandType, Name = Text.DefaultCommand };
		public static Command ReadOnlyCommand = new Command { Id = 5, Application_Id = AllApplication.Id, IsBuiltIn = true, CommandType = AllCommandType, Name = Text.ReadOnlyCommand };

		//BuiltinBackendCommandId starts from 51
        public const int StartBackendCommandId = 51;
		public static Command DefineRoleCommand = new Command { Id = StartBackendCommandId, Application_Id = AllApplication.Id, Name = Text.DefineRole, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "RoleManagement" };
		public static Command DefineCommandCommand = new Command { Id = StartBackendCommandId + 1, Application_Id = AllApplication.Id, Name = Text.DefineCommand, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "CommandManagement" };
		public static Command DefineBackendCommandCommand = new Command { Id = StartBackendCommandId + 2, Application_Id = AllApplication.Id, Name = Text.DefineBackendCommand, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "BackendCommandManagement" };
		public static Command DefineFrontendCommandCommand = new Command { Id = StartBackendCommandId + 3, Application_Id = AllApplication.Id, Name = Text.DefineFrontendCommand, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "FrontendCommandManagement" };
		public static Command DefineUserCommandCommand = new Command { Id = StartBackendCommandId + 4, Application_Id = AllApplication.Id, Name = Text.DefineUserCommand, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "UserCommandManagement" };
		public static Command DefineApplicationCommand = new Command { Id = StartBackendCommandId + 5, Application_Id = AllApplication.Id, Name = Text.DefineApplication, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "ApplicationManagement" };
		public static Command DefineUserIdCommand = new Command { Id = StartBackendCommandId + 6, Application_Id = AllApplication.Id, Name = Text.UserIdManagement, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "UserIdManagement" };
		public static Command DefineUserCommand = new Command { Id = StartBackendCommandId + 7, Application_Id = AllApplication.Id, Name = Text.DefineUser, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "UserManagement" };
		public static Command QueryDepositCommand = new Command { Id = StartBackendCommandId + 8, Application_Id = AllApplication.Id, Name = Text.UserDepositHistoryInquiry, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "UserDepositHistory" };
		public static Command QueryExchangeCommand = new Command { Id = StartBackendCommandId + 9, Application_Id = AllApplication.Id, Name = Text.UserExchangeHisotryInquiry, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "UserExchangeHistory" };
		public static Command UserDepositCommand = new Command { Id = StartBackendCommandId + 10, Application_Id = AllApplication.Id, Name = Text.UserDeposit, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "" };
		public static Command AgentDepositCommand = new Command { Id = StartBackendCommandId + 11, Application_Id = AllApplication.Id, Name = Text.AgentDeposit, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "" };
		public static Command ScoreDepositCommand = new Command { Id = StartBackendCommandId + 12, Application_Id = AllApplication.Id, Name = Text.ScoreDeposit, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "" };
		public static Command UserRoleUpDownCommand = new Command { Id = StartBackendCommandId + 13, Application_Id = AllApplication.Id, Name = Text.UserRoleUpDown, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "" };
		public static Command AllocateAgentUserIdCommand = new Command { Id = StartBackendCommandId + 14, Application_Id = AllApplication.Id, Name = Text.AllocatAgentUserId, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "" };
		public static Command ExchangeCommand = new Command { Id = StartBackendCommandId + 15, Application_Id = AllApplication.Id, Name = Text.Exchange, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "" };
		public static Command SettlementCommand = new Command { Id = StartBackendCommandId + 16, Application_Id = AllApplication.Id, Name = Text.SettlementManagement, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "ExchangeManagement" };
		public static Command DefineExchangeRateCommand = new Command { Id = StartBackendCommandId + 17, Application_Id = AllApplication.Id, Name = Text.DefineExchangeRate, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "ExchangeRateManagement" };
		public static Command BlockCommand = new Command { Id = StartBackendCommandId + 18, Application_Id = AllApplication.Id, Name = Text.BlockCommand, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "BlockManagement" };
		public static Command BlockTypeCommand = new Command { Id = StartBackendCommandId + 19, Application_Id = AllApplication.Id, Name = Text.BlockTypeCommand, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "BlockTypeManagement" };


		//Command for 9258 chat application, id starts from 501
		public const int _9258StartBackendCommandId = 501;
		public static Command DefineRoomGroupCommand = new Command { Id = _9258StartBackendCommandId, Application_Id = _9258ChatApplication.Id, Name = Text.DefineRoomGroup, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "RoomGroupManagement" };
		public static Command DefineRoomCommand = new Command { Id = _9258StartBackendCommandId + 1, Application_Id = _9258ChatApplication.Id, Name = Text.DefineRoom, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "RoomManagement" };
		public static Command DefineGiftCommand = new Command { Id = _9258StartBackendCommandId + 2, Application_Id = _9258ChatApplication.Id, Name = Text.DefineGift, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "GiftManagement" };
		public static Command DefineGiftGroupCommand = new Command { Id = _9258StartBackendCommandId + 3, Application_Id = _9258ChatApplication.Id, Name = Text.DefineGiftGroup, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "GiftGroupManagement" };
		public static Command QueryGiftInOutCommand = new Command { Id = _9258StartBackendCommandId + 4, Application_Id = _9258ChatApplication.Id, Name = Text.GiftInOutHistoryInquiry, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "GiftInOutInquiryManagement" };
        //public static Command MusicManagementCommand = new Command { Id = _9258StartBackendCommandId + 5, Application_Id = _9258ChatApplication.Id, Name = Text.MusicManagement, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "MusicManagement" };
        


        public const int UserDefinedCommandStartId = 10001;

		//Commands that are not saved into database, starts from 1000001
        public const int NotSaveDBCommandStartId = 1000001;
        public static Command PersonalInfoCommand = new Command { Id = NotSaveDBCommandStartId, Application_Id = AllApplication.Id, Name = Text.PersonalInfoCommand, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "PersonalInfoManagement" };
        public static Command PasswordCommand = new Command { Id = NotSaveDBCommandStartId + 1, Application_Id = AllApplication.Id, Name = Text.PasswordCommand, IsBuiltIn = true, CommandType = BackendCommandType, ActionName = "PasswordManagement" };

        //Comands that doesn't not need to check security, not saved in DB, starts from 1010001
        public const int _9258NormalCommandStartId = 1010001;
        public static Command OnMicCommand = new Command { Id = _9258NormalCommandStartId, Application_Id = _9258ChatApplication.Id, Name = Text.OnMicLabel, IsBuiltIn = true, CommandType = NormalCommandType };
        public static Command ScoreToMoneyCommand = new Command { Id = _9258NormalCommandStartId + 1, Application_Id = _9258ChatApplication.Id, Name = Text.ScoreToMoney, IsBuiltIn = true, CommandType = NormalCommandType};

		#endregion

		#region Roles

		public static Role AllRole = new Role { Id = 1, Application_Id = AllApplication.Id, Name = Text.AllRole, IsBuiltIn = true };
		public static Role DefaultRole = new Role { Id = 2, Application_Id = AllApplication.Id, Name = Text.DefaultRole, IsBuiltIn = true };
		public static Role RegisterUserRole = new Role { Id = 3, Application_Id = AllApplication.Id, Name = Text.RegisterUser, IsBuiltIn = true };
        public static Role AgentRole = new Role { Id = 4, Application_Id = AllApplication.Id, Name = Text.Agent, IsBuiltIn = true };
        public static Role AdministratorRole = new Role { Id = 5, Application_Id = AllApplication.Id, Name = Text.Administrator, IsBuiltIn = true };
		public static Role ReadOnlyRole = new Role{Id = 6, Application_Id = AllApplication.Id, Name = Text.ReadOnly, IsBuiltIn = true};
		//Role for 9258 chat application, id starts from 500
        public const int _9258StartRoleId = 500;

		public static Role _9258AdministratorRole = new Role { Id = _9258StartRoleId, Application_Id = _9258ChatApplication.Id, Name = Text.Administrator, IsBuiltIn = true };
		public static Role _9258RoomAdministratorRole = new Role { Id = _9258StartRoleId + 1, Application_Id = _9258ChatApplication.Id, Name = Text.RoomAdministrator, IsBuiltIn = true };
		public static Role _9258RoomDirectorRole = new Role { Id = _9258StartRoleId + 2, Application_Id = _9258ChatApplication.Id, Name = Text.RoomDirectory, IsBuiltIn = true };

        public const int UserDefinedRoleStartId = 10001;

		#endregion

		#region RoleCommand

		public static RoleCommand AdministratorRoleCommand = new RoleCommand { Id = 1, Command_Id = AllCommand.Id, IsManagerCommand = false, SourceRole_Id = AdministratorRole.Id, TargetRole_Id = AllRole.Id, Application_Id = AllApplication.Id, IsBuiltIn = true };
		public static RoleCommand ReadOnlyRoleCommand = new RoleCommand { Id = 2, Command_Id = ReadOnlyCommand.Id, IsManagerCommand = false, SourceRole_Id = ReadOnlyRole.Id, TargetRole_Id = AllRole.Id, Application_Id = AllApplication.Id, IsBuiltIn = true };
		//9258 RoleCommand starts from 500
		public static int _9258StartRoleCommandId = 500;
		public static RoleCommand _9258AdministratorRoleCommand = new RoleCommand { Id = _9258StartRoleCommandId, SourceRole_Id = _9258AdministratorRole.Id, TargetRole_Id = AllRole.Id, Command_Id = AllCommand.Id, Application_Id = _9258ChatApplication.Id, IsBuiltIn = true, IsManagerCommand = false };

        public const int UserDefinedRoleCommandStartId = 10001;

		#endregion

		#region User

        public static User Administrator = new User { Id = 1, Name = Text.Administrator, ApplicationCreated_Id = AllApplication.Id, Gender = true, Password = Utility.GetMD5String("9258"), NickName = Text.Administrator, IsBuiltIn = true };
        public static User ReadOnlyUser = new User { Id = 2, Name = "", ApplicationCreated_Id = AllApplication.Id, NickName = "", Gender = true, Password = Utility.GetMD5String("9258"), IsBuiltIn = true };

		//9258 Builtin Users starts from 20
        public const int _9258BuiltinUserStartId = 20;
        public static User _9258Administrator = new User { Id = _9258BuiltinUserStartId, Name = Text.Administrator, ApplicationCreated_Id = _9258ChatApplication.Id, Password = Utility.GetMD5String("9258"), NickName = Text.Administrator, IsBuiltIn = true };

        public const int UserDefinedUserStartId = 101;

		public static UserApplicationInfo AdministratorInfo = new UserApplicationInfo { User_Id = Administrator.Id, Application_Id = AllApplication.Id, Role_Id = AdministratorRole.Id };
		public static UserApplicationInfo _9258AdministratorInfo = new UserApplicationInfo { User_Id = _9258Administrator.Id, Application_Id = _9258ChatApplication.Id, Role_Id = AdministratorRole.Id };
		public static UserApplicationInfo ReadOnlyUserInfo = new UserApplicationInfo { User_Id = ReadOnlyUser.Id, Application_Id = AllApplication.Id, Role_Id = ReadOnlyRole.Id };
		#endregion

		#region ImageType

		public static ImageType AllImageType = new ImageType { Id = 1, Name = Text.AllImage, IsBuiltIn = true };
		public static ImageType ApplicationImageType = new ImageType { Id = 2, Name = Text.ApplicationImage, IsBuiltIn = true };
		public static ImageType RoleImageType = new ImageType { Id = 3, Name = Text.RoleImage, IsBuiltIn = true };
		public static ImageType CommandImageType = new ImageType { Id = 4, Name = Text.CommandImage, IsBuiltIn = true };
		public static ImageType RoomGroupImageType = new ImageType { Id = 5, Name = Text.RoomGroupImage, IsBuiltIn = true };
		public static ImageType RoomImageType = new ImageType { Id = 6, Name = Text.RoomImage, IsBuiltIn = true };
		public static ImageType GiftGroupImageType = new ImageType { Id = 7, Name = Text.GiftGroupImage, IsBuiltIn = true };
		public static ImageType GiftImageType = new ImageType { Id = 8, Name = Text.GiftImage, IsBuiltIn = true };
		public static ImageType SmileImageType = new ImageType { Id = 9, Name = Text.SmileImage, IsBuiltIn = true };
		public static ImageType HeaderImageType = new ImageType { Id = 10, Name = Text.HeaderImage, IsBuiltIn = true };
        public static ImageType StampImageType = new ImageType { Id = 11, Name = Text.StampImage, IsBuiltIn = true };

		#endregion

        #region Image
        
        public static Image DefaultImage = new Image { Id = 0, Ext = "png", ImageType_Id = AllImageType.Id,IsBuiltIn = true, Name = "NotFound" }; 

        #endregion

        #region BlockType

        public static BlockType BlockUserType = new BlockType { Id = 1, Name = Text.BlockUser, Days = 3, IsBuiltIn = true };
		public static BlockType BlockIPType = new BlockType { Id = 2, Name = Text.BlockIP, Days = 3, IsBuiltIn = true };
		public static BlockType BlockMacType = new BlockType { Id = 3, Name = Text.BlockMac, Days = 3, IsBuiltIn = true };
        public static BlockType BlackListType = new BlockType { Id = 4, Name = Text.BlackList, Days = 9999, IsBuiltIn = true };

        public const int UserDefinedBlockTypeStartId = 10001;

		#endregion

        #region BuiltIn Lists

        public static List<Application> Applications = new List<Application> { AllApplication, _9258ChatApplication, BackendApplication };
        public static List<Role> Roles = new List<Role> { AllRole, AdministratorRole, RegisterUserRole, AgentRole, DefaultRole, ReadOnlyRole,
            //9258 Roles
            _9258AdministratorRole,_9258RoomAdministratorRole, _9258RoomDirectorRole };
        public static List<Command> Commands = new List<Command> { AllCommand, DefaultBackendCommand, DefaultFrontendCommand, DefaultUserCommand, ReadOnlyCommand,
            DefineRoleCommand,DefineCommandCommand,DefineBackendCommandCommand,DefineFrontendCommandCommand,DefineUserCommandCommand,DefineApplicationCommand,
            DefineUserIdCommand,DefineUserCommand,QueryDepositCommand,QueryExchangeCommand,UserDepositCommand,AgentDepositCommand,ScoreDepositCommand,
            UserRoleUpDownCommand,AllocateAgentUserIdCommand,SettlementCommand,DefineExchangeRateCommand,BlockCommand,BlockTypeCommand,ExchangeCommand,
            //9258 Commands
            DefineRoomGroupCommand,DefineRoomCommand,DefineGiftCommand,DefineGiftGroupCommand,QueryGiftInOutCommand,ExchangeCommand  //,MusicManagementCommand
        };
        public static List<RoleCommand> RoleCommands = new List<RoleCommand> { AdministratorRoleCommand,ReadOnlyRoleCommand,
        //9258 RoleCommands
        _9258AdministratorRoleCommand
        };
        public static List<User> Users = new List<User> { Administrator, ReadOnlyUser, _9258Administrator };
        public static List<UserApplicationInfo> UserInfos = new List<UserApplicationInfo> { AdministratorInfo, _9258AdministratorInfo, ReadOnlyUserInfo };
        public static List<ImageType> ImageTypes = new List<ImageType> { AllImageType, ApplicationImageType, RoleImageType, CommandImageType, RoomGroupImageType, RoomImageType, GiftGroupImageType, GiftImageType, SmileImageType, HeaderImageType,StampImageType };
        public static List<BlockType> BlockTypes = new List<BlockType> { BlockUserType, BlockIPType, BlockMacType,BlackListType };
        public static List<Image> Images = new List<Image> { DefaultImage };
        #endregion

        #region Exclude Ids

        public static List<int> ExcludeApplicationIds = new List<int> { AllApplication.Id, BackendApplication.Id };
		public static List<int> ExcludeCommandIds = new List<int> { AllCommand.Id, DefaultBackendCommand.Id, DefaultBackendCommand.Id, DefaultFrontendCommand.Id, DefaultUserCommand.Id, ReadOnlyCommand.Id };
		public static List<int> ExcludeRoleIds = new List<int> { AllRole.Id, AdministratorRole.Id, DefaultRole.Id, _9258AdministratorRole.Id,ReadOnlyRole.Id };
		public static List<int> ExculdeRoleCommandIds = new List<int> { AdministratorRoleCommand.Id,  _9258AdministratorRoleCommand.Id };
		public static List<int> ExcludeUserIds = new List<int> { Administrator.Id, ReadOnlyUser.Id, _9258Administrator.Id };

		#endregion
	}
}
