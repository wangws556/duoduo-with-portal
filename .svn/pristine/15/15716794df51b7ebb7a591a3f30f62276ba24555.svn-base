using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;
using YoYoStudio.Model.Core;
using YoYoStudio.Resource;

namespace YoYoStudio.Model.Chat
{
    [DataContract]
    [Table]
    public class Room : ImagedEntity
    {
        #region Entity Members
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Name { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Description { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? HostUser_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? AgentUser_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? RoomGroup_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? MaxUserCount { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public bool Hide { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int PublicMicCount { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int PrivateMicCount { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int SecretMicCount { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool PublicChatEnabled { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool PrivateChatEnabled { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool GiftEnabled { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string ServiceIp { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public int? PublicMicTime { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Password { get; set; }
        [DataMember]
        [Column(Type=DbType.Boolean)]
        public bool? Enabled { get; set; }

        #endregion

        public List<RoomBlackList> BlackLists { get; set; }
        public List<RoomRole> RoomRoles { get; set; }

        public Room()
            : base()
        {
            Name = "默认房间";
            HostUser_Id = null;
            AgentUser_Id = null;
        }

        public override Core.ImageType GetImageType()
        {
            return BuiltIns.RoomImageType;
        }

    }
    [DataContract]
    [Table]
    public class RoomGroup : ImagedEntity
    {
        #region Entity Members
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Name { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Description { get; set; }
        [DataMember]
		[Column(Type = DbType.Int32)]
		public int? ParentGroup_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool? Enabled { get; set; }

        #endregion

        public List<RoomGroup> SubRoomGroups { get; set; }
        public List<Room> Rooms { get; set; }

        public override Core.ImageType GetImageType()
        {
            return BuiltIns.RoomGroupImageType;
        }

        public override void BeforeSave()
        {
            base.BeforeSave();
            if (ParentGroup_Id.HasValue && ParentGroup_Id.Value <= 0)
            {
                ParentGroup_Id = null;
            }
        }
    }
	[DataContract]
	[Table]
    public class RoomRole : ModelEntity
	{
        #region Entity Members
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public int Room_Id { get; set; }
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public int User_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Role_Id { get; set; }
        #endregion
	}
	[DataContract]
	[Table]
	public class RoomBlackList : ModelEntity
	{
		[DataMember]
		[Column(IsPrimaryKey = true, Type = DbType.Int32)]
		public int Room_Id { get; set; }
		[DataMember]
		[Column(IsPrimaryKey = true, Type = DbType.Int32)]
		public int User_Id { get; set; }
	}

    [DataContract]
    [Table]
    public class RoomConfig : IdedEntity
    {
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Room_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Tag { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Order { get; set; }
        [DataMember]
        [Column(Type=DbType.String)]
        public string Value { get; set; }
        [DataMember]
        [Column(IsPrimaryKey=true,Type = DbType.Int32)]
        public override int Id { get; set; }
    }

    public enum RoomTags
    {
    }
}
