using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Model.Core;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common;

namespace YoYoStudio.Client.ViewModel
{
  
    public partial class RegisterViewModel:ApplicationableViewModel
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }

        public RegisterViewModel() { }

        public void Register(string account, string pwd, int sex)
        {
            UserIdList userIdList = Singleton<ApplicationViewModel>.Instance.Cache.AvailableUserIds.Where(r => r.IsUsed == false).First();

            if (userIdList != null)
            {
                User newUser = Singleton<ApplicationViewModel>.Instance.ChatClient.Register(userIdList, account, Utility.GetMD5String(pwd), sex);
                if(newUser != null)
                    Messenger.Default.Send<EnumNotificationMessage<Object, MainWindowAction>>(new EnumNotificationMessage<object, MainWindowAction>(MainWindowAction.RegisterSuccess,new UserViewModel(newUser)));
            }
        }

        public void Close()
        {
            Messenger.Default.Send<EnumNotificationMessage<Object, MainWindowAction>>(new EnumNotificationMessage<object, MainWindowAction>(MainWindowAction.RegisterCancel));
        }
    }

}
