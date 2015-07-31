using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Model.Core;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common;
using YoYoStudio.Resource;
using YoYoStudio.Model;
using System.IO;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "errorMessage", property = "ErrorMessage", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "accountDescription", property = "AccountDescription", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "passwordDescription", property = "PasswordDescription", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "completeReigster", property = "CompleteReigster", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "registerAgreement", property = "RegisterAgreement", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "view", property = "View", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "account", property = "Account", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "password", property = "Password", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "confirmPassword", property = "ConfirmPassword", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "userId", property = "UserId", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "sex", property = "Sex", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "accountIdDescription", property = "AccountIdDescription", type = "string", defaultValue = "string.Empty")]
    public partial class RegisterWindowViewModel : WindowViewModel
    {
        public RegisterWindowViewModel()
        {

        }

        private User user = null;

        public void Register(string account, string pwd, int sex)
        {
            User newUser = ApplicationVM.ChatClient.Register(UserId, account, Utility.GetMD5String(pwd), sex);
            if (newUser != null)
            {
                if (newUser.Image_Id.HasValue)
                {
                    Image img = ApplicationVM.ChatClient.GetImage(newUser.Image_Id.Value);
                    var imgVM = ApplicationVM.AddImage(img);
					File.WriteAllBytes(imgVM.GetAbsoluteFile(true), img.TheImage);
                }
                user = newUser;
                ErrorMessage = string.Format(Resource.Messages.RegisterSucceeded, user.Id);
            }
            else
            {
                user = null;
                ErrorMessage = Resource.Messages.RegisterFailed;
            }
        }

        public override void Initialize()
        {
            Busy = UserId > 0;
            base.Initialize();
        }

        public string AccountLabel { get; private set; }
        public string AccountIdLabel { get; private set; }
        public string PasswordLabel { get; private set; }
        public string ConfirmPasswordLabel { get; private set; }
        public string GenderLabel { get; private set; }
        public string TryNowLabel { get; private set; }

        protected override void InitializeResource()
        {
            title = Text.RegisterUser;
            AccountLabel = Text.AccountNameLabel;
            AccountIdLabel = Text.AccountIdLabel;
            PasswordLabel = Text.PasswordLabel;
            PasswordDescription = Text.PasswordDescription;
            ConfirmPasswordLabel = Text.ConfirmPasswordLabel;
            GenderLabel = Text.Gender;
            TryNowLabel = Text.TryNow;
            registerAgreement.SetValue(Text.RegisterAgreement);
            completeReigster.SetValue(Text.CompleteRegister);
            accountIdDescription.SetValue(Text.AccountIdDescription);
            accountDescription.SetValue(Text.AccountNameDescription);
            view.SetValue(Text.View);

            if (!Busy)
            {
                errorMessage.SetValue(Resource.Messages.NoRegisterUserIdAvailable);
            }
            base.InitializeResource();
        }

        public SecureCommand TryNowCommand { get; set; }

        protected override void InitializeCommand()
        {
            TryNowCommand = new SecureCommand(TryNowCommandExecute, CanTryNowCommandExecute);
            base.InitializeCommand();
        }

        private bool CanTryNowCommandExecute(SecureCommandArgs args)
        {
            return user != null;
        }

        private void TryNowCommandExecute(SecureCommandArgs args)
        {
            Messenger.Default.Send<EnumNotificationMessage<Object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.RegisterSuccess, user));
        }

        protected override void ReleaseUnManagedResource()
        {
            if (user != null)
            {
                Messenger.Default.Send<EnumNotificationMessage<Object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.RegisterSuccess, user));
            }
            else
            {
                Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.RegisterCancel));
            }
            base.ReleaseUnManagedResource();
        }

        protected override void ReleaseManagedResource()
        {
            ApplicationVM.RegisterWindowVM = null;
            base.ReleaseManagedResource();
        }
    }

}
