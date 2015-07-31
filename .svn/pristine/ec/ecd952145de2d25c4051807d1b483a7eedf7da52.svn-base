using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model.Chat;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "roomId", property = "RoomId", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "userId", property = "UserId", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "roleId", property = "RoleId", type = "int", defaultValue = "0")]
    public partial class RoomRoleViewModel : EntityViewModel
    {
        public RoomRoleViewModel(RoomRole roomRole)
            : base(roomRole)
        {
            roomId.SetValue(roomRole.Room_Id);
            userId.SetValue(roomRole.User_Id);
            roleId.SetValue(roomRole.Role_Id);
        }
    }
}
