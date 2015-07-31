using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Snippets;
using YoYoStudio.Common;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model.Configuration;
using YoYoStudio.Resource;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "nickName", property = "NickName", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "accountId", property = "AccountId", type = "int", defaultValue = "-1")]
    [SnippetPropertyINPC(field = "gender", property = "Gender", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field = "isFemale", property = "IsFemale", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field = "email", property = "Email", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "passwordQuestion", property = "PasswordQuestion", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "passwordAnswer", property = "PasswordAnswer", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "age", property = "Age", type = "int", defaultValue = "-1")]
    [SnippetPropertyINPC(field = "lastLoginTime", property = "LastLoginTime", type = "DateTime", defaultValue = "DateTime.Now")]
    [SnippetPropertyINPC(field = "imageSource", property = "ImageSource", type = "ImageSource", defaultValue = "null")]
    public partial class PersonalInfoConfigurationViewModel : ConfigurationViewModel
    {

        public PersonalInfoConfigurationViewModel(PersonalInfoConfiguration config)
            : base(config)
        { }

        public override void Save()
        {
            if (Dirty)
            {
                if (Me != null)
                {
                    Me.NickName = NickName;
                    Me.Gender = Gender;
                    Me.Email = Email;
                    Me.PasswordQuestion = PasswordQuestion;
                    Me.PasswordAnswer = PasswordAnswer;
                    Me.Age = Age;
                    Me.Save();
                }
                ApplicationVM.ChatClient.UpdateUser(Me.GetConcretEntity<User>());
                ImageSource = Utility.BytesToBitmapImage(Me.ImageVM.TheImage);
                if (ImageSource == null)
                {
                    ImageSource = Utility.CreateBitmapSourceFromFile(Me.ImageVM.GetAbsoluteFile(true));
                }
                base.Save();
            }
        }

        public override void Reset()
        {
            if (Me != null)
            {
                Me.Reset();
                accountId.SetValue(Me.Id);
                nickName.SetValue(Me.NickName);
                gender.SetValue(Me.Gender);
                email.SetValue(Me.Email);
                passwordQuestion.SetValue(Me.PasswordQuestion);
                passwordAnswer.SetValue(Me.PasswordAnswer);
                age.SetValue(Me.Age);
                isFemale.SetValue(!Gender);
                lastLoginTime.SetValue(Me.LastLoginTime);
            }
            base.Reset();
        }

        public override void Initialize()
        {
            if (Me != null)
            {
                accountId.SetValue(Me.Id);
                nickName.SetValue(Me.NickName);
                gender.SetValue(Me.Gender);
                email.SetValue(Me.Email);
                passwordQuestion.SetValue(Me.PasswordQuestion);
                passwordAnswer.SetValue(Me.PasswordAnswer);
                age.SetValue(Me.Age);
                isFemale.SetValue(!Gender);
                lastLoginTime.SetValue(Me.LastLoginTime);
                if (Me.ImageVM != null)
                {
					imageSource.SetValue(Utility.CreateBitmapSourceFromFile(Me.ImageVM.GetAbsoluteFile(true)));
                }
            }
            base.Initialize();
        }

        protected override void InitializeResource()
        {
            title = Text.PersonalInfoConfiguration;
            base.InitializeResource();
        }

        protected override void InitializeCommand()
        {
            Command = new SecureCommand(CommandExecute, CanCommandExecute);
            base.InitializeCommand();
        }

        public void CommandExecute(SecureCommandArgs args)
        {
            ConfigurationItemViewModel civm = new ConfigurationItemViewModel(ApplicationVM.ProfileVM.PhotoSelectorVM);
            Messenger.Default.Send<EnumNotificationMessage<object, ConfigurationWindowAction>>(new EnumNotificationMessage<object, ConfigurationWindowAction>(ConfigurationWindowAction.ConfigurationStateChanged, civm));
        }

        public bool CanCommandExecute(SecureCommandArgs args)
        {
            return Me != null;
        }

        public string AssetsLabel { get { return Text.PersonalAssets; } }
        public string RoomsLabel { get { return Text.PersonalRooms; } }
        public string CarsLabel { get { return Text.PersonalCars; } }
        public string WeboLabel { get { return Text.PersonalWebo; } }
        public string BasicInfoLabel { get { return Text.PersonalInfo; } }

        public string ChangePhotoLabel { get { return Text.ChangePhotoLabel; } }
        public string NickNameLabel { get { return Text.NickNameLabel; } }
        public string AccountIdLabel { get { return Text.AccountIdLabel; } }
        public string GenderLabel { get { return Text.Gender; } }
        public string EmailLabel { get { return Text.EmailLabel; } }
        public string PasswordQuestionLabel { get { return Text.PasswordQuestionLabel; } }
        public string PasswordAnswerLabel { get { return Text.PasswordAnswerLabel; } }
        public string AgeLabel { get { return Text.AgeLabel; } }
        public string LastLoginTimeLabel { get { return Text.LastLoginTimeLabel; } }
        public string MaleLabel { get { return Text.Male; } }
        public string FemaleLabel { get { return Text.Female; } }

    }
}
