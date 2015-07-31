using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Model.Core
{
	public enum RequestStatus
	{		
		None = 0,
		NotSubmitted = 1,
		Submitted,
		Processed,
		Closed
	}

    [DataContract]
    [Table]
	public class DepositHistory : IdedEntity
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32,IsIdentity=true)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int OptUser_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int User_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.DateTime)]
        public DateTime Time { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Money { get; set; }
        [DataMember]
        [Column(Type=DbType.Boolean)]
        public bool? IsAgent { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Score { get; set; }
    }
    [DataContract]
    [Table]
    public class ExchangeHistory : IdedEntity 
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32,IsIdentity=true)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int OptUser_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int64)]
        public int User_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.DateTime)]
        public DateTime ApplyTime { get; set; }
        [DataMember]
        [Column(Type = DbType.DateTime)]
        public DateTime SettlementTime { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Score { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Money { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Cache { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Status { get; set; }
    }
}
