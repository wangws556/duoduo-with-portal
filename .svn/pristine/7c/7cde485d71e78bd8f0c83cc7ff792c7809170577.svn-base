using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class RoomModel:JsonModel
    {
        public int? HostUser_Id { get; set; }
        public int? RoomGroup_Id { get; set; }
        public int? AgentUser_Id { get; set; }
        public int? MaxUserCount { get; set; }
        public bool Hide { get; set; }
        public int PublicMicCount { get; set; }
        public int PrivateMicCount { get; set; }
        public int SecretMicCount { get; set; }
        public int PublicChatEnabled { get; set; }
        public int PrivateChatEnabled { get; set; }
        public int GiftEnabled { get; set; }
        public string ServiceIp { get; set; }
        public int? PublicMicTime { get; set; }
        public string Password { get; set; }
        public int Enabled { get; set; }

        public RoomModel() : this(null) { }

        public RoomModel(Room room)
            : base(room)
        {
            if (room != null)
            {
                Name = room.Name;
                Description = room.Description;
                HostUser_Id = room.HostUser_Id;
                AgentUser_Id = room.AgentUser_Id;
                RoomGroup_Id = room.RoomGroup_Id;
                MaxUserCount = room.MaxUserCount;
                Hide = room.Hide;
                PublicMicCount = room.PublicMicCount;
                PrivateMicCount = room.PrivateMicCount;
                SecretMicCount = room.SecretMicCount;
                PublicChatEnabled = room.PublicChatEnabled?1:0;
                PrivateChatEnabled = room.PrivateChatEnabled?1:0;
                GiftEnabled = room.GiftEnabled?1:0;
                ServiceIp = room.ServiceIp;
                PublicMicTime = room.PublicMicTime;
                Password = room.Password;
                Enabled = room.Enabled.HasValue?(room.Enabled.Value?1:0):0;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new Room
            {
                Name = Name,
                Description = Description,
                HostUser_Id = HostUser_Id,
                AgentUser_Id = AgentUser_Id,
                RoomGroup_Id = RoomGroup_Id,
                MaxUserCount = MaxUserCount,
                Hide = Convert.ToBoolean(Hide),
                PublicMicCount = PublicMicCount,
                PrivateMicCount = PrivateMicCount,
                SecretMicCount = SecretMicCount,
                PublicChatEnabled = Convert.ToBoolean( PublicChatEnabled),
                PrivateChatEnabled = Convert.ToBoolean( PrivateChatEnabled),
                GiftEnabled = Convert.ToBoolean(GiftEnabled),
                ServiceIp = ServiceIp,
                PublicMicTime = PublicMicTime,
                Password = Password,
                Enabled = Convert.ToBoolean(Enabled)
            };
        }

        public List<RoomRoleModel> RoomRoles;
    }

    [Serializable]
    public class RoomGroupModel : JsonModel
    {
        public int? ParentGroup_Id { get; set; }
        public int Enabled { get; set; }

        public RoomGroupModel() : this(null) { }

        public RoomGroupModel(RoomGroup roomG)
            : base(roomG)
        {
            if (roomG != null)
            {
                Name = roomG.Name;
                Description = roomG.Description;
                ParentGroup_Id = roomG.ParentGroup_Id;
                Enabled = (roomG.Enabled.HasValue && roomG.Enabled.Value)?1:0;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new RoomGroup
            {
                Name = Name,
                Description = Description,
                ParentGroup_Id = ParentGroup_Id,
                Enabled = Enabled == 1
            };
        }
    }

    [Serializable]
    public class RoomRoleModel : JsonModel
    {
        public int Room_Id { get; set; }
        public int User_Id { get; set; }
        public int Role_Id { get; set; }

        public RoomRoleModel(RoomRole roomR)
            :base(roomR)
        {
            if (roomR != null)
            {
                Room_Id = roomR.Room_Id;
                User_Id = roomR.User_Id;
                Role_Id = roomR.Role_Id;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new RoomRole
            {
                Room_Id = Room_Id,
                User_Id = User_Id,
                Role_Id = Role_Id,
            };
        }
    }
}