using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "nickName", property = "NickName", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "money", property = "Money", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "score", property = "Score", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "roleVM", property = "RoleVM", type = "RoleViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "roomRoleVMs", property = "RoomRoleVMs", type = "ObservableCollection<RoomRoleViewModel>", defaultValue = "null")]
	public partial class UserViewModel : ImagedEntityViewModel
	{
        public UserApplicationInfo UserInfo { get; private set; }

		public UserViewModel(User usr):base(usr)
		{
            Name = usr.Name;
            NickName = usr.NickName;            
        }

        public override void Initialize()
        {
            User usr = GetConcretEntity<User>();
            UserInfo = ApplicationVM.Cache.UserInfos.FirstOrDefault(i => i.User_Id == usr.Id &&
                (i.Application_Id == ApplicationVM.ApplicationId || i.Application_Id == BuiltIns.AllApplication.Id));
            if (UserInfo != null)
            {
                if (UserInfo.Money.HasValue)
                {
                    Money = UserInfo.Money.Value;
                }
                if (UserInfo.Score.HasValue)
                {
                    Score = UserInfo.Score.Value;
                }
                RoleVM = ApplicationVM.LocalCache.AllRoleVMs.FirstOrDefault(r => r.Id == UserInfo.Role_Id);
            }
           
            var rrs = ApplicationVM.LocalCache.AllRoomRoleVMs.Where(rr => rr.UserId == Id);
            if (rrs != null)
            {
                RoomRoleVMs = new System.Collections.ObjectModel.ObservableCollection<RoomRoleViewModel>(rrs);
            }
            base.Initialize();
        }

        public bool IsRoleInRoom(RoomViewModel room, Role role)
        {
            return RoomRoleVMs != null && RoomRoleVMs.FirstOrDefault(rr => rr.RoomId == room.Id && rr.RoleId == role.Id) != null;
        }

        public bool HasCommand(UserViewModel target, CommandViewModel cmd, RoomViewModel room)
        {
            if (RoleVM != null && target.RoleVM != null)
            {
                var rcv =  RoleVM.RoleCommandVMs.FirstOrDefault(rc => rc.TargetRoleVM == target.RoleVM && rc.CommandVM == cmd);
                if (rcv != null)
                {
                    if (rcv.IsManagerCommand)
                    {
						return IsRoleInRoom(room, BuiltIns.RoomAdministratorRole);
                    }
                    return true;
                }
            }
            return false;
        }

        #region Json

		public ClientUserModel JsonData
		{
			get
			{
				return ToJson() as ClientUserModel;
			}
		}

        protected override object ToJson()
        {
            return new ClientUserModel(GetConcretEntity<User>(), UserInfo);
        }

        #endregion

    }
}
