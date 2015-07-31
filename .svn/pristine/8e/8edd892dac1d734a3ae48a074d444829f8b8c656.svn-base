using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Model.Core
{
    [DataContract]
    [Table]
    public class ExchangeRate :IdedEntity
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32, IsIdentity = true)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.DateTime)]
        public DateTime ValidTime { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? ScoreToMoney { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? MoneyToCache { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? ScoreToCache { get; set; }
    }
}
