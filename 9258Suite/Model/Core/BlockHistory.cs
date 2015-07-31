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
	public class BlockType : IdedEntity
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
        public int? Days { get; set; }
       
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool IsBuiltIn { get; set; }

        public BlockType()
        {
            Id = BuiltIns.UserDefinedBlockTypeStartId;
        }
    }

    [DataContract]
    [Table]
    public class BlockList : IdedEntity
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int BlockType_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Content { get; set; }
    }
   
    [DataContract]
    [Table]
    public class BlockHistory : IdedEntity
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int OptUser_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.DateTime)]
        public DateTime Time { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int BlockType_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool IsBlock { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Content { get; set; }
    }
}
