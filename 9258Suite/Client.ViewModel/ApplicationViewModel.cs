using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.ChatService.Client;
using System.Reflection;
using YoYoStudio.Common;
using YoYoStudio.Model.Chat;
using System.Timers;
using YoYoStudio.RoomService.Client;
using System.IO;
using YoYoStudio.Model;
using System.Runtime.InteropServices;
using log4net;
using YoYoStudio.Model.Json;

namespace YoYoStudio.Client.ViewModel
{
    [ComVisible(true)]
	[SnippetPropertyINPC(type = "HallWindowViewModel", field = "hallWindowVM", property = "HallWindowVM", defaultValue = "new HallWindowViewModel()")]
    [SnippetPropertyINPC(type = "RoomWindowViewModel", field = "roomWindowVM", property = "RoomWindowVM", defaultValue = "null")]
    [SnippetPropertyINPC(type = "RegisterWindowViewModel", field = "registerWindowVM", property = "RegisterWindowVM", defaultValue = "null")]
    [SnippetPropertyINPC(type = "ConfigurationWindowViewModel", field = "configurationWindowVM", property = "ConfigurationWindowVM", defaultValue = "null")]
    [SnippetPropertyINPC(field = "logoImagePath", property = "LogoImagePath", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field="isAuthenticated", property="IsAuthenticated", type="bool",defaultValue="false")]
    public partial class ApplicationViewModel : TitledViewModel
    {
        public ClientCache Cache { get; private set; }
        public ChatServiceClient ChatClient { get; private set; }        
		public LocalCache LocalCache { get; private set; }
        public ProfileViewModel ProfileVM { get; private set; }
        public string ApplicationName { get; private set; }
        public ILog Logger { get; private set; }
        public int ApplicationId { get; private set; }
        public string OnMicImageUrl { get; private set; }
        public string DownMicImageUrl { get; private set; }

		public string Root { get; private set; }
		public string FlexFile { get; private set; }
        public string MusicFlexFile { get; private set; }

        public event Action<List<RoomViewModel>> OnlineUserCountChangedEvent;

        private ApplicationViewModel()
        {
            ChatClient = new ChatServiceClient(callback);
            Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.Config.XmlConfigurator.Configure();
            ApplicationId = BuiltIns._9258ChatApplication.Id;
			Root = Environment.CurrentDirectory;
			FlexFile = Path.Combine(Root, Const.FlexFile);
            MusicFlexFile = Path.Combine(Root, Const.MusicFlexFile);
            ApplicationName = "9258";
            ProfileVM = Singleton<ProfileViewModel>.Instance;
            LocalCache = Singleton<LocalCache>.Instance;
            OnMicImageUrl = Path.Combine(Root, @"Images\onMic.jpg");
            DownMicImageUrl = Path.Combine(Root, @"Images\downMic.jpg");
        }

        public string GetImageGroupPath(int imageTypeId, string imageGroup, bool absolute)
        {
            string path = Const.ImageRootFolderName + "\\" + imageTypeId.ToString() + "\\" + imageGroup;
            return absolute ? Path.Combine(Root, path) : path;
        }

		public void SwitchUser()
		{
			LogOff();
			ChatClient = new ChatServiceClient(callback);
		}

        public override void Initialize()
        {
            base.Initialize();
            InitializeMenu();
			ProfileVM.Initialize();
        }

        #region Context Menus

        private MenuModel GetMenu(int cmdId)
        {
			return LocalCache.AllCommandVMs.FirstOrDefault(c => c.Id == cmdId).ToJson() as MenuModel;
        }
        
        private void InitializeMenu()
        {
            #region UserListMenu

			userListMenus = new List<MenuModel>();

            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.KickOutOfRoomCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.AddToBlackListCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.AllowConnectPrivateMicCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.AllowConnectSecretMicCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.BlockHornHallHornCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.BlockUserIdCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.BlockUserIpCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.BlockUserMacCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.SetOrCancelRoomManagerCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.UpDownUserPrivateMicCommandId));
            userListMenus.Add(GetMenu(Applications._9258App.UserCommands.UpDownUserPublicMicCommandId));

            #endregion
			managerListMenus = new List<MenuModel>();
			micUserListMenus = new List<MenuModel>();
        }

        private List<MenuModel> userListMenus;

		private List<MenuModel> managerListMenus;

		private List<MenuModel> micUserListMenus;

		public List<MenuModel> GetUserListMenus()
        {
			return new List<MenuModel>(userListMenus);
        }

		public List<MenuModel> GetManagerListMenus()
        {
			return new List<MenuModel>(managerListMenus);
        }

		public List<MenuModel> GetMicUserListMenus()
        {
			return new List<MenuModel>(micUserListMenus);
        }

        #endregion

    }
}
