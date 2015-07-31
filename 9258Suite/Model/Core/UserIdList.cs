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
    [DataContract]
    [Table]
	public class UserIdList : ModelEntity 
    {
        [DataMember]
        [Column(Type = DbType.Int32,IsPrimaryKey=true)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(IsPrimaryKey=true,Type = DbType.Int32)]
        public int User_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Owner_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Role_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool IsUsed { get; set; }
    }
    [DataContract]
    [Table]
	public class UserIdHistory : IdedEntity 
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32,IsIdentity=true)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int OptUser_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Role_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int StartId { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? EndId { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(Type=DbType.DateTime)]
        public DateTime Time { get; set; }
    }
}
