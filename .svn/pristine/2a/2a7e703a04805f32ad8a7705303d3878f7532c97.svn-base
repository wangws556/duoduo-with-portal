using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "hide", property = "Hide", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field = "micIndex", property = "MicIndex", type = "int", defaultValue = "-1")]
    [SnippetPropertyINPC(field = "micAction", property = "MicAction", type = "MicAction", defaultValue = "MicAction.None")]
    [SnippetPropertyINPC(field = "micStatus", property = "MicStatus", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field ="musicStatus", property = "MusicStatus", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "micType", property = "MicType", type = "MicType", defaultValue = "MicType.None")]
    [SnippetPropertyINPC(field = "streamGuid", property = "StreamGuid", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "email", property = "Email", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "passwordQuestion", property = "PasswordQuestion", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "passwordAnswer", property = "PasswordAnswer", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "gender", property = "Gender", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field = "lastLoginTime", property = "LastLoginTime", type = "DateTime", defaultValue = "DateTime.Now")]
    [SnippetPropertyINPC(field = "nickName", property = "NickName", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "password", property = "Password", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "money", property = "Money", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "score", property = "Score", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "age", property = "Age", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "roleVM", property = "RoleVM", type = "RoleViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "roomWindowVM", property = "RoomWindowVM", type = "RoomWindowViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "roomRoleVMs", property = "RoomRoleVMs", type = "ObservableCollection<RoomRoleViewModel>", defaultValue = "null")]
	public partial class UserViewModel : ImagedEntityViewModel
	{
        public UserApplicationInfo UserInfo { get; private set; }
        public bool IsInitialized { get; set; }

        public UserViewModel(User usr)
            : base(usr)
        {
            name.SetValue(usr.Name);
            nickName.SetValue(usr.NickName);
            email.SetValue(usr.Email);
            passwordQuestion.SetValue(usr.PasswordQuestion);
            passwordAnswer.SetValue(usr.PasswordAnswer);
            gender.SetValue(usr.Gender.HasValue ? usr.Gender.Value : false);
            lastLoginTime.SetValue(usr.LastLoginTime.HasValue ? usr.LastLoginTime.Value : DateTime.Now);
            age.SetValue(usr.Age.HasValue ? usr.Age.Value : 0);
            password.SetValue(usr.Password);
            IsInitialized = false;
        }

        public override void Initialize()
        {
            User usr = GetConcretEntity<User>();
			UserInfo = ApplicationVM.ChatClient.GetUserInfo(Id);
            if (UserInfo != null)
            {
                if (UserInfo.Money.HasValue)
                {
                    money.SetValue(UserInfo.Money.Value);
                }
                else
                {
                    money.SetValue(0);
                }
                if (UserInfo.Score.HasValue)
                {
                    score.SetValue(UserInfo.Score.Value);
                }
                else
                {
                    score.SetValue(0);
                }
                roleVM.SetValue(ApplicationVM.LocalCache.AllRoleVMs.FirstOrDefault(r => r.Id == UserInfo.Role_Id));
            }
            var rrs = ApplicationVM.LocalCache.AllRoomRoleVMs.Where(rr => rr.UserId == Id);
            if (rrs != null)
            {
                roomRoleVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<RoomRoleViewModel>(rrs));
            }
            IsInitialized = true;
            base.Initialize();
        }

        #region Command

        public bool IsRoleInRoom(int roomId, int roleId)
        {
            return ApplicationVM.Cache.IsUserInRoomRole(roomId, Me.Id, roleId);
        }        

        public bool HasCommand(int roomId, int cmdId, int targetRoleId)
        {
            return ApplicationVM.Cache.HasCommand(roomId, cmdId, Me.Id, Me.RoleVM.Id, targetRoleId);
        }

        #endregion

        public bool IsMe()
        {
            return this == ApplicationVM.LocalCache.CurrentUserVM;
        }

        #region Mic

        public void OnMic(MicType micType, int micIndex, string streamGuid, int micStatus)
        {
            MicType = micType;
            MicIndex = micIndex;
            StreamGuid = streamGuid;
            MicAction = Model.Chat.MicAction.OnMic;
            MicStatus = micStatus;
        }

        public void Toggle(int micStatus)
        {
            MicAction = Model.Chat.MicAction.Toggle;
            MicStatus = micStatus;
        }

        public void DownMic()
        {
            MicType = Model.Chat.MicType.None;
            MicIndex = -1;
            StreamGuid = string.Empty;
            MicAction = Model.Chat.MicAction.None;
            MicStatus = MicStatusMessage.MicStatus_Off;
        }

        
        #endregion

        public override void Save()
        {
            User user = GetConcretEntity<User>();
            user.NickName = NickName;
            user.Email = Email;
            user.PasswordAnswer = PasswordAnswer;
            user.PasswordQuestion = PasswordQuestion;
            user.Gender = Gender;
            user.Age = Age;
            user.Password = Password;
            base.Save();

        }
                

        public override void Reset()
        {
            User user = GetConcretEntity<User>();
            NickName = user.NickName;
            Email = user.Email;
            PasswordAnswer = user.PasswordAnswer;
            PasswordQuestion = user.PasswordQuestion;
            Gender = user.Gender.HasValue ? user.Gender.Value : false;
            Age = user.Age.HasValue ? user.Age.Value : 0;
            base.Reset();
        }

        #region Json

        public override object ToJson()
        {
			var json = new ClientUserModel(GetConcretEntity<User>(), UserInfo) {
				RoleImageUrl = RoleVM.ImageVM.StaticImageFile,
				icon = ImageVM.StaticImageFile,
			};
            if (RoomWindowVM != null)
            {
                json.CanSendGift = HasCommand(RoomWindowVM.RoomVM.Id, Applications._9258App.FrontendCommands.SendGiftCommandId, -1);
                json.CanReceiveGift = HasCommand(RoomWindowVM.RoomVM.Id, Applications._9258App.FrontendCommands.ReceiveGiftCommandId, -1);
                json.CanSendHornMsg = HasCommand(RoomWindowVM.RoomVM.Id, Applications._9258App.FrontendCommands.HornCommandId, -1);
                json.CanSendHallHornMsg = HasCommand(RoomWindowVM.RoomVM.Id, Applications._9258App.FrontendCommands.HallHornCommandId, -1);
                json.CanSendGlobalHornMsg = HasCommand(RoomWindowVM.RoomVM.Id, Applications._9258App.FrontendCommands.GlobalHornCommandId, -1);
                json.IsOnMic = MicStatus != MicStatusMessage.MicStatus_Off;
            }
            if (json.IsOnMic)
            {
                json.CameraImageUrl = ApplicationVM.OnMicImageUrl;
            }
            else
            {
                json.CameraImageUrl = ApplicationVM.DownMicImageUrl;
            }
            return json;
        }

        public override string GetJson(bool reGenerate = false)
        {
            if (reGenerate == true)
            {
                UserInfo = ApplicationVM.ChatClient.GetUserInfo(Id);
            }

            return base.GetJson(reGenerate);
        }

        #endregion

    }
}
