using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Common;
using YoYoStudio.Common.Notification;
using Snippets;
using YoYoStudio.RoomService.Client;
using YoYoStudio.Model.Json;
using YoYoStudio.Resource;
using YoYoStudio.Model.Core;
using System.Web.Script.Serialization;
using YoYoStudio.Model;

namespace YoYoStudio.Client.ViewModel
{
    [ComVisible(true)]
    [SnippetPropertyINPC(field="onlinieUserCount",property="OnlinieUserCount",type="int",defaultValue="0")]
    [SnippetPropertyINPC(type = "string", field = "userName", property = "UserName", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(type = "bool", field = "canCancel", property = "CanCancel", defaultValue = "true")]
    [SnippetPropertyINPC(type = "string", field = "password", property = "Password", defaultValue = "string.Empty")]
    public partial class HallWindowViewModel : WindowViewModel
    {
        public HallWindowViewModel()
        {
            busy.SetValue(true);
        }

        public override void Initialize()
        {
            ApplicationVM.UserLoggedInEvent += UserLoggedInEventHandler;
            ApplicationVM.UserLoggedOffEvent += UserLoggedOffEventHandler;
            ApplicationVM.OnlineUserCountChangedEvent += OnlineUserCountChangedEventHandler;
            base.Initialize();            
        }

        public void GetHtml(string htm)
        {
        }

		public void GetData(object data)
		{
			Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.TestData,data));
		}

        public override void WebPageLoadCompletedHandler()
        {
            base.WebPageLoadCompletedHandler();
        }

        public void Login(string userid, string pwd, bool remember, bool autoLogin)
        {
            try
            {
                var mac = Utility.GetMacAddress();

                int r = ApplicationVM.ChatClient.CanUserLogin(int.Parse(userid), mac);
                if (r == 0)
                {
                    User user = ApplicationVM.ChatClient.Login(int.Parse(userid), pwd, mac);
                    if (user != null)
                    {
                        UserViewModel uvm = new UserViewModel(user);
                        uvm.Initialize();
                        ApplicationVM.LocalCache.AllUserVMs.Add(user.Id, uvm);
                        ApplicationVM.LocalCache.CurrentUserVM = uvm;
                        ApplicationVM.HallWindowVM.Notify<UserViewModel>(() => Me);
                        ApplicationVM.IsAuthenticated = true;
                        UpdateLoginInfo(userid, pwd, remember, autoLogin);
                        Notify<bool>(() => IsAuthenticated);
                        CallJavaScript("LoginSucceed");
                    }
                    else
                    {
						CallJavaScript("AlertMessage", Resource.Messages.InvalidToken);
                    }
                }
                else
                {
                    var block = ApplicationVM.Cache.BlockTypes.FirstOrDefault(b => b.Id == r);
					CallJavaScript("AlertMessage", string.Format(Resource.Messages.UserBlocked, block.Name));
                }
            }
            catch
            {
				CallJavaScript("AlertMessage", Resource.Messages.InvalidToken);
            }
        }

        public void Close()
        {
            //if (ApplicationVM.IsAuthenticated)
            //{
            //    View_Close();
            //}
            //else
            //{
                ApplicationVM.ShutDown();
            //}
        }

        public void Register()
        {
            try
            {
                int nextUserId = ApplicationVM.ChatClient.GetNextAvailableUserId(BuiltIns.RegisterUserRole.Id);
                ApplicationVM.RegisterWindowVM = new RegisterWindowViewModel();
                ApplicationVM.RegisterWindowVM.UserId = nextUserId;
                ApplicationVM.RegisterWindowVM.Initialize();
                Messenger.Default.Send<EnumNotificationMessage<Object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.Register, ApplicationVM.RegisterWindowVM));
            }
            catch
            {
                Messenger.Default.Send<EnumNotificationMessage<Object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.RegisterUserIdNotAvailable));
            }            
        }

        public string CancelLabel { get; private set; }

        #region Json

        public string RoomGroupsJson
        {
            get
            {
                var groups = new List<TreeNodeModel>();
                foreach (var g in ApplicationVM.LocalCache.AllRoomGroupVMs.Where(rgv => rgv.ParentRoomGroupVM == null))
                {
                    groups.Add(g.ToJson() as TreeNodeModel);
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(groups);
            }
        }

        #endregion

        public void EnterRoom(int roomId)
        {
            if (Me.RoomWindowVM != null)
            {
                Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.AlreadyInRoom, roomId));
                return;
            }
			RoomViewModel roomVM = ApplicationVM.LocalCache.AllRoomVMs.FirstOrDefault(r => r.Id == roomId);
			if (roomVM != null)
			{
				roomVM.Enter();
			}
			else
			{
				Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.RoomNotExist));
			}
        }

        #region Private

        private void UpdateLoginInfo(string userid, string pwd, bool remember, bool autoLogin)
        { 
            LoginEntryViewModel loginVM = ApplicationVM.ProfileVM.LoginEntryVMs.FirstOrDefault(r=>r.UserId== userid);
            if (loginVM == null)
            {
                ApplicationVM.ProfileVM.LoginEntryVMs.Add(new LoginEntryViewModel(new Model.Configuration.LoginEntry { UserId = userid, Password = pwd, Remember = remember }));
            }
            else
            {
                loginVM.UserId = userid;
                loginVM.Password = pwd;
                loginVM.Remember = remember;
            }

            if (ApplicationVM.ProfileVM.LastLoginVM == null)
            {
                ApplicationVM.ProfileVM.LastLoginVM = new LoginEntryViewModel(new Model.Configuration.LoginEntry { UserId = string.Empty, Password = string.Empty, Remember = false });
            }
            
            ApplicationVM.ProfileVM.LastLoginVM.UserId = userid;
            ApplicationVM.ProfileVM.LastLoginVM.Password = pwd;
            ApplicationVM.ProfileVM.LastLoginVM.Remember = remember;
            
            ApplicationVM.ProfileVM.AutoLogin = autoLogin;
            ApplicationVM.ProfileVM.Save();
        }

        private void OnlineUserCountChangedEventHandler(List<RoomViewModel> changedRooms)
        {
            List<TreeNodeModel> roomGroupsUserCount = new List<TreeNodeModel>();
            ApplicationVM.LocalCache.AllRoomGroupVMs.ForEach(g =>
                {
                    var c = g.GetOnlineUserCount();
                    if (g.OnlineUserCount != c)
                    {
                        g.OnlineUserCount = c;
                        var node = new TreeNodeModel { id = g.Id, count = c, rootid = g.RootRoomGroupVM.Id, name = g.Name };
                        roomGroupsUserCount.Add(node);
                    }
                });
            List<NodeModel> roomsUserCount = new List<NodeModel>();
            changedRooms.ForEach(r =>
                {
                    var node = new NodeModel { id = r.Id, count = r.OnlineUserCount, rootid = r.RootRoomGroupVM.Id, name = r.Name };
                    roomsUserCount.Add(node);
                });
            JavaScriptSerializer js = new JavaScriptSerializer();
            CallJavaScript("UpdateOnlineUserCountAsync", js.Serialize(roomGroupsUserCount), js.Serialize(roomsUserCount));
        }

        protected override void ReleaseUnManagedResource()
        {
            ApplicationVM.UserLoggedInEvent -= UserLoggedInEventHandler;
            ApplicationVM.UserLoggedOffEvent -= UserLoggedOffEventHandler;
            base.ReleaseUnManagedResource();
        }

        protected override void InitializeResource()
        {
            title = Text._9258ChatApplication;
            welcomeMessage.SetValue(Text.WelcomeMessage);
            busyMessage.SetValue(Messages.ConnectingToServer);
            CancelLabel = Text.Cancel;
            base.InitializeResource();
        }

        void UserLoggedOffEventHandler(int obj)
        {
            if (ApplicationVM.LocalCache.AllUserVMs.ContainsKey(obj))
            {
                ApplicationVM.LocalCache.AllUserVMs.Remove(obj);
            }
            OnlinieUserCount--;
        }

        void UserLoggedInEventHandler(User obj)
        {
            if (!ApplicationVM.LocalCache.AllUserVMs.ContainsKey(obj.Id))
            {
                UserViewModel uvm = new UserViewModel(obj);
                ApplicationVM.LocalCache.AllUserVMs.Add(obj.Id, uvm);
            }
            OnlinieUserCount++;
        }

        #endregion

        
    }
}
