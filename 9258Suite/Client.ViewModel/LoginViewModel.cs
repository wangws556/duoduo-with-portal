using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using System.Windows;
using System.Runtime.InteropServices;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Common;
using YoYoStudio.Model.Core;
using YoYoStudio.Common.Notification;

namespace YoYoStudio.Client.ViewModel
{
    [ComVisible(true)]
    [SnippetPropertyINPC(type="string",field="userName", property="UserName", defaultValue="string.Empty")]
    [SnippetPropertyINPC(type = "string", field = "password", property = "Password", defaultValue = "string.Empty")]
    public partial class LoginViewModel : ApplicationableViewModel
    {
        public void Login(string userid, string pwd, bool remember)
        {
            try
            {
                User user = Singleton<ApplicationViewModel>.Instance.ChatClient.Login(int.Parse(userid), pwd, Utility.GetMacAddress());
                if (user != null)
                {
                    UserViewModel uvm = new UserViewModel(user);
                    uvm.Initialize();
                    ApplicationVM.LocalCache.AllUserVMs.Add(user.Id, uvm);
                    Singleton<ApplicationViewModel>.Instance.LocalCache.CurrentUserVM = uvm;
                    Singleton<ApplicationViewModel>.Instance.HallVM.User = uvm;
                    Messenger.Default.Send<EnumNotificationMessage<object, MainWindowAction>>(new EnumNotificationMessage<object, MainWindowAction>(MainWindowAction.LoginSuccess, uvm));
                }
                else
                {
                    Messenger.Default.Send<EnumNotificationMessage<object, MainWindowAction>>(new EnumNotificationMessage<object, MainWindowAction>(MainWindowAction.LoginFail));
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void Register()
        {
            if (Singleton<ApplicationViewModel>.Instance.Cache.AvailableUserIds.Count > 0)
            {
                Messenger.Default.Send<EnumNotificationMessage<Object, MainWindowAction>>(new EnumNotificationMessage<object, MainWindowAction>(MainWindowAction.Register));
            }
            else
                MessageBox.Show("暂时不能注册，请稍后再试。");
        }

        public void Close()
        {
            Messenger.Default.Send<EnumNotificationMessage<Object, MainWindowAction>>(new EnumNotificationMessage<object, MainWindowAction>(MainWindowAction.CloseApp));
        }
    }
}
