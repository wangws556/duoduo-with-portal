using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Model.Chat
{
    [DataContract]
    [Table]
    public class GiftGroup : ImagedEntity
    {
        #region Entity Members
        [DataMember]
        [Column(IsPrimaryKey=true,Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Name { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Description { get; set; }

        #endregion

        public List<Gift> Gifts { get; set; }

        
        public override Core.ImageType GetImageType()
        {
            return BuiltIns.GiftGroupImageType;
        }
    }
    [DataContract]
    [Table]
    public class Gift : ImagedEntity
    {
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
        public int Price { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Score { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Money { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? RunWay { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? RoomBroadcast { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? WorldBroadcast { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Unit { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? GiftGroup_Id { get; set; }

        public override Core.ImageType GetImageType()
        {
            return BuiltIns.GiftImageType;
        }
    }
}
