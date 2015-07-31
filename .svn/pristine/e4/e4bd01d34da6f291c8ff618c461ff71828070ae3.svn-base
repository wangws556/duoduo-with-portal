using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.Common;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model
{
    [Serializable]
    public class Cache
    {
        protected int applicationId;
        protected int countPerBatch = 500;

        protected Cache(int appId)
        {
            this.applicationId = appId;
        }

        public void BuildRelationship()
        {
            foreach (var rg in RoomGroups)
            {
                rg.SubRoomGroups = new List<RoomGroup>();
                rg.SubRoomGroups.AddRange(RoomGroups.Where(g => g.ParentGroup_Id.HasValue && g.ParentGroup_Id.Value == rg.Id));
                rg.Rooms = Rooms.Where(r => r.RoomGroup_Id == rg.Id).ToList();
            }
            GiftGroups.ForEach(giftGroup =>
            {
                giftGroup.Gifts = Gifts.Where(g => g.GiftGroup_Id == giftGroup.Id).ToList();
            });
            foreach (var room in Rooms)
            {
                room.RoomRoles = new List<RoomRole>();
                var rrs = RoomRoles.Where(r => r.Room_Id == room.Id);
                if (rrs != null)
                {
                    room.RoomRoles.AddRange(rrs);
                }
            }
            foreach (var role in Roles)
            {
                if (role.Application_Id == applicationId || role.Application_Id == BuiltIns.AllApplication.Id)
                {
                    role.RoleCommands = new List<RoleCommandView>();
                    var rcs = RoleCommands.Where(rc => (rc.SourceRole_Id == role.Id || rc.SourceRole_Id == BuiltIns.AllRole.Id)
                        && (rc.Application_Id == applicationId || rc.Application_Id == BuiltIns.AllApplication.Id));

                    if (rcs != null)
                    {
                        role.RoleCommands.AddRange(rcs);
                    }
                }
            }
        }

        public virtual void RefreshCache(params object[] args)
        {
        }

        public List<RoomGroup> RoomGroups { get; set; }

        public List<Room> Rooms { get; set; }

        public List<GiftGroup> GiftGroups { get; set; }

        public List<Gift> Gifts { get; set; }

        public List<Role> Roles { get; set; }

        public List<BlockType> BlockTypes { get; set; }

        public List<RoleCommandView> RoleCommands { get; set; }

        public List<Command> Commands { get; set; }

        public List<ImageWithoutBody> Images { get; set; }

        public List<RoomRole> RoomRoles { get; set; }

        public Application Application { get; set; }

        public List<ExchangeRate> ExchangeRates { get; set; }

        public void AddRoomRole(RoomRole roomRole)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == roomRole.Room_Id);
            if (room != null)
            {
                lock (RoomRoles)
                {
                    if (RoomRoles.FirstOrDefault(rr => rr.Room_Id == roomRole.Room_Id && rr.User_Id == roomRole.User_Id && rr.Role_Id == roomRole.Role_Id) == null)
                    {
                        RoomRoles.Add(roomRole);
                        lock (room.RoomRoles)
                        {
                            room.RoomRoles.Add(roomRole);
                        }
                    }
                }
            }
        }

        public void RemoveRoomRole(int roomId, int roleId, int userId)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                lock (RoomRoles)
                {
                    var roomRole = RoomRoles.FirstOrDefault(rr => rr.Room_Id == roomId && rr.User_Id == userId && rr.Role_Id == roleId);
                    if (roomRole != null)
                    {
                        lock (room.RoomRoles)
                        {
                            room.RoomRoles.Remove(roomRole);
                        }
                        RoomRoles.Remove(roomRole);
                    }
                }
            }
        }

        public bool IsUserInRoomRole(int roomId, int userId, int roleId)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                lock (room.RoomRoles)
                {
                    return room.RoomRoles.FirstOrDefault(r => r.Role_Id == roleId && r.User_Id == userId) != null;
                }
            }
            return false;
        }

        protected RoleCommandView GetRoleCommand(int roomId, int cmdId, int sourceUserId, int sourceRoleId, int targetRoleId)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                return null;
            }
            var cmd = Commands.FirstOrDefault(c => c.Id == cmdId);
            var sourceRole = Roles.FirstOrDefault(r => r.Id == sourceRoleId);
            var targetRole = Roles.FirstOrDefault(r => r.Id == targetRoleId);
            if (targetRole == null)
            {
                targetRole = Roles.FirstOrDefault(r => r.Id == BuiltIns.AllRole.Id);
            }
            if (cmd == null || sourceRole == null || targetRole == null)
            {
                return null;
            }
            var allRole = Roles.FirstOrDefault(r => r.Id == BuiltIns.AllRole.Id);
            var roleCmd = sourceRole.RoleCommands.FirstOrDefault(rc =>
                    (rc.Command_Id == cmd.Id || rc.Command_Id == BuiltIns.AllCommand.Id)
                    && (rc.TargetRole_Id == targetRole.Id || rc.TargetRole_Id == allRole.Id));
            if (roleCmd == null)
            {
                roleCmd = allRole.RoleCommands.FirstOrDefault(rc =>
                    (rc.Command_Id == cmd.Id || rc.Command_Id == BuiltIns.AllCommand.Id)
                    && (rc.TargetRole_Id == targetRole.Id || rc.TargetRole_Id == allRole.Id));
            }
            return roleCmd;
        }

        public virtual bool HasCommand(int roomId, int cmdId, int sourceUserId, int sourceRoleId, int targetRoleId)
        {
            return GetRoleCommand(roomId, cmdId, sourceUserId, sourceRoleId, targetRoleId) != null;
        }
    }
}
