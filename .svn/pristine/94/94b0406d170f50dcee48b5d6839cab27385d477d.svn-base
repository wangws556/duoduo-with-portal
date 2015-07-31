using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.ViewModel
{
    public partial class UserViewModel
    {
        #region OpenConfigurationCommand

        public SecureCommand OpenConfigurationCommand { get; set; }

        private void OpenConfigurationCommandExecute(SecureCommandArgs args)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(
                new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.OpenConfigurationWindow));
        }

        private bool CanOpenConfigurationCommandExecute(SecureCommandArgs args)
        {
            return ApplicationVM.LocalCache.CurrentUserVM != null && ApplicationVM.LocalCache.CurrentUserVM == this;
        }

        #endregion

        #region SelectUserCommand

        public SecureCommand SelectUserCommand { get; set; }

        private void SelectUserCommandExecute(SecureCommandArgs args)
        {
            if (CanSelectUserCommandExecute(args))
            {
                RoomWindowVM.SelectUser(this);
                ApplicationVM.RoomWindowVM.CallJavaScript("addMicAndChatUser", NickName, Id);
            }
        }

        private bool CanSelectUserCommandExecute(SecureCommandArgs args)
        {
            return RoomWindowVM != null;
        }

        #endregion

        protected override void InitializeCommand()
        {
            OpenConfigurationCommand = new SecureCommand(OpenConfigurationCommandExecute, CanOpenConfigurationCommandExecute);
            SelectUserCommand = new SecureCommand(SelectUserCommandExecute, CanSelectUserCommandExecute);
            base.InitializeCommand();
        }
    }
}
