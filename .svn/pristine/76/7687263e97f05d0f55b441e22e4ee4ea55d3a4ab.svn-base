using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;

namespace YoYoStudio.Client.ViewModel
{
	public class LocalCache
	{
		private LocalCache()
		{
            AllSecureCommandVMs = new List<SecureCommandViewModel>();
			AllRoomGroupVMs = new List<RoomGroupViewModel>();
			AllRoomVMs = new List<RoomViewModel>();
			RoomServicePort = int.Parse(ConfigurationManager.AppSettings["RoomServicePort"]);
            RoomAudioServicePort = int.Parse(ConfigurationManager.AppSettings["RoomAudioServicePort"]);
            VideoFps = int.Parse(ConfigurationManager.AppSettings["VideoFps"]);
            VideoQuality = int.Parse(ConfigurationManager.AppSettings["VideoQuality"]);
            PublicChatMessageCount = int.Parse(ConfigurationManager.AppSettings["PublicChatMessageCount"]);
            PrivateChatMessageCount = int.Parse(ConfigurationManager.AppSettings["PrivateChatMessageCount"]);
            MessagePerSecond = int.Parse(ConfigurationManager.AppSettings["MessagePerSecond"]);
            AllImages = new Dictionary<int, Dictionary<int, ImageViewModel>>();
            AllGiftGroupVMs = new List<GiftGroupViewModel>();
            AllGiftVMs = new List<GiftViewModel>();
            AllUserVMs = new SafeDictionary<int, UserViewModel>();
            AllRoleVMs = new List<RoleViewModel>();
            AllCommandVMs = new List<CommandViewModel>();
            AllRoleCommandVMs = new List<RoleCommandViewModel>();
            AllRoomRoleVMs = new List<RoomRoleViewModel>();
            AllExchangeRateVMs = new List<ExchangeRateViewModel>();

            foreach(var imgType in BuiltIns.ImageTypes)
            {
                AllImages.Add(imgType.Id,new Dictionary<int,ImageViewModel>());
            }
		}

        public void ClearCache()
        {
            AllSecureCommandVMs.Clear();
            AllRoomGroupVMs.Clear();
            AllRoomVMs.Clear();
            AllImages.Clear();
            AllGiftGroupVMs.Clear();
            AllGiftVMs.Clear();
            AllUserVMs.Clear();
            AllRoleVMs.Clear();
            AllCommandVMs.Clear();
            AllRoleCommandVMs.Clear();
            AllRoomRoleVMs.Clear();
            AllExchangeRateVMs.Clear();
        }

		public int RoomServicePort { get; private set; }
        public int RoomAudioServicePort { get; private set; }
        public int VideoFps { get; private set; }
        public int VideoQuality { get; private set; }
        public int PublicChatMessageCount { get; set; }
        public int PrivateChatMessageCount { get; set; }
        public int MessagePerSecond { get; set; }
        public int HornMsgMoney 
        {
            get
            {
                if (AllCommandVMs != null)
                {
                    var cmd = AllCommandVMs.FirstOrDefault(c => c.Id == Applications._9258App.FrontendCommands.HornCommandId);
                    if (cmd != null)
                        return cmd.Money;
                }
                return 0;
            }
        }
        public int HallHornMsgMoney 
        {
            get
            {
                if (AllCommandVMs != null)
                {
                    var cmd = AllCommandVMs.FirstOrDefault(c => c.Id == Applications._9258App.FrontendCommands.HallHornCommandId);
                    if (cmd != null)
                        return cmd.Money;
                }
                return 0;
            }
        }
        public int GlobalHornMsgMoney
        {
            get
            {
                if (AllCommandVMs != null)
                {
                    var cmd = AllCommandVMs.FirstOrDefault(c => c.Id == Applications._9258App.FrontendCommands.GlobalHornCommandId);
                    if (cmd != null)
                        return cmd.Money;
                }
                return 0;
            }
        }
		public UserViewModel CurrentUserVM { get; set; }

		public List<RoomGroupViewModel> AllRoomGroupVMs { get; set; }

		public List<RoomViewModel> AllRoomVMs { get; set; }

        public List<GiftGroupViewModel> AllGiftGroupVMs { get; set; }

        public List<GiftViewModel> AllGiftVMs { get; set; }

        public List<RoleViewModel> AllRoleVMs { get; set; }

        public List<CommandViewModel> AllCommandVMs { get; set; }

        public List<RoleCommandViewModel> AllRoleCommandVMs { get; set; }

        public List<RoomRoleViewModel> AllRoomRoleVMs { get; set; }
        //imageTypeId, imageId
        public Dictionary<int, Dictionary<int, ImageViewModel>> AllImages { get; set; }

        public SafeDictionary<int, UserViewModel> AllUserVMs { get; set; }

        public List<SecureCommandViewModel> AllSecureCommandVMs { get; set; }

        public List<ExchangeRateViewModel> AllExchangeRateVMs { get; set; }

	}
}
