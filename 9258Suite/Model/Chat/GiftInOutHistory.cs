using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Model.Chat
{
    [DataContract]
    [Table]
    public class GiftInOutHistory:IdedEntity
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.UInt32, IsIdentity = true)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.UInt32)]
        public int SourceUser_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.UInt32)]
        public int TargetUser_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.UInt32)]
        public int Gift_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.UInt32)]
        public int Count { get; set; }
        [DataMember]
        [Column(Type = DbType.DateTime)]
        public DateTime Time { get; set; }
        [DataMember]
        [Column(Type = DbType.UInt32)]
        public int Room_Id { get; set; }
    }
}
