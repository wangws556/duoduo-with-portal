using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.ViewModel
{
    public partial class HallWindowViewModel
    {
        public SecureCommand SwitchUserCommand { get; set; }

        protected override void InitializeCommand()
        {
            SwitchUserCommand = new SecureCommand(SwitchUserCommandExecute, CanSwitchUserCommandExecute);
            CancelCommand = new SecureCommand(CancelCommandExecute, CanCancelCommandExecute);
            base.InitializeCommand();
        }

        private bool CanSwitchUserCommandExecute(SecureCommandArgs args)
        {
            return ApplicationVM.LocalCache.CurrentUserVM != null;
        }

        private void SwitchUserCommandExecute(SecureCommandArgs args)
        {
            if (CanSwitchUserCommandExecute(args))
            {
                Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.SwitchUser));
            }
        }

        public SecureCommand CancelCommand { get; set; }

        private void CancelCommandExecute(SecureCommandArgs args)
        {
            ApplicationVM.ShutDown();
        }

        private bool CanCancelCommandExecute(SecureCommandArgs args)
        {
            return CanCancel;
        }
    }
}
