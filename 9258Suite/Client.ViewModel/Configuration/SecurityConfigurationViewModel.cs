using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Snippets;
using YoYoStudio.Common;
using YoYoStudio.Common.Notification;
using YoYoStudio.Model.Configuration;
using YoYoStudio.Model.Core;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "newPassword", property = "NewPassword", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "oldPassword", property = "OldPassword", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "confirmPassword", property = "ConfirmPassword", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "autoLogin", property = "AutoLogin", type = "bool", defaultValue = "false")]
    public partial class SecurityConfigurationViewModel : ConfigurationViewModel, IDataErrorInfo
    {
        private bool inputValid = true;
        public SecurityConfigurationViewModel(SecurityConfiguration config)
            : base(config)
        { }

        public override void Save()
        {
            if (Dirty)
            {
                if (inputValid && String.Compare(Me.Password, Utility.GetMD5String(OldPassword)) == 0)
                {
                    if (Me != null)
                    {
                        Me.Password = Utility.GetMD5String(NewPassword);
                    }

                    ApplicationVM.ChatClient.UpdateUser(Me.GetConcretEntity<User>());

                    ApplicationVM.ProfileVM.AutoLogin = AutoLogin;

                    base.Save();
                }

                else if (AutoLogin != ApplicationVM.ProfileVM.AutoLogin)
                {
                    ApplicationVM.ProfileVM.AutoLogin = AutoLogin;
                    ApplicationVM.ProfileVM.Save();
                }
                else
                {
                    Messenger.Default.Send<EnumNotificationMessage<object, ConfigurationWindowAction>>(new EnumNotificationMessage<object, ConfigurationWindowAction>(ConfigurationWindowAction.PasswordInvalid, this));
                }
            }
        }

        public override void Reset()
        {
            newPassword.SetValue(string.Empty);
            oldPassword.SetValue(string.Empty);
            confirmPassword.SetValue(string.Empty);
            autoLogin.SetValue(false);
            base.Reset();
        }

        public string NewPasswordLabel { get; private set; }
        public string OldPasswordLabel { get; private set; }
        public string ConfirmPasswordLabel { get; private set; }
        public string AutoLoginLabel { get; set; }

        protected override void InitializeResource()
        {
            title = Text.SecurityConfiguration;
            NewPasswordLabel = Text.NewPasswordLabel;
            OldPasswordLabel = Text.OldPasswordLabel;
            ConfirmPasswordLabel = Text.ConfirmPasswordLabel;
            AutoLoginLabel = Text.AutoLoginLabel;
            base.InitializeResource();
        }

        #region IDataErrorInfo

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                inputValid = true;
                if (columnName == "NewPassword")
                {
                    if (String.IsNullOrEmpty(NewPassword))
                    {
                        return null;
                    }
                    if (NewPassword.Length < 6 || NewPassword.Length > 10)
                    {
                        inputValid = false;
                        return Text.PasswordLength;
                    }

                    bool hasCapital = false;
                    char[] charArray = NewPassword.ToCharArray();
                    foreach (char c in charArray)
                    {
                        if (Char.IsUpper(c))
                        {
                            hasCapital = true;
                            break;
                        }
                    }
                    if (!hasCapital)
                    {
                        inputValid = false;
                        return Text.PasswordRequireCapital;
                    }
                    return null;
                }
                if (columnName == "ConfirmPassword")
                {
                    if (!String.IsNullOrEmpty(NewPassword) &&
                        !String.IsNullOrEmpty(ConfirmPassword) &&
                        !ConfirmPassword.Equals(NewPassword))
                    {
                        inputValid = false;
                        return Text.PasswordMismatch;
                    }
                    return null;
                }
                return null;
            }
        }

        #endregion
    }
}
