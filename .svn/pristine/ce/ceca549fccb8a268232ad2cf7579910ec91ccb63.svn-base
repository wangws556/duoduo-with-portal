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
    public class Command : BuiltInImagedEntity
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Name { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Description { get; set; }        
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int CommandType { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string ActionName { get; set; }
        [DataMember]
        [Column(Type=DbType.Int32)]
        public int? Money { get; set; }

        public override Core.ImageType GetImageType()
        {
            return BuiltIns.CommandImageType;
        }
        public Command()
        {
            Id = BuiltIns.UserDefinedCommandStartId;
        }
    }
    [DataContract]
    [Table]
    public class RoleCommand : BuiltInEntity 
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int SourceRole_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int TargetRole_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Command_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool IsManagerCommand { get; set; }
		[DataMember]
		[Column(Type = DbType.Int32)]
		public int Application_Id { get; set; }

        public RoleCommand()
        {
            Id = BuiltIns.UserDefinedRoleCommandStartId;
        }
    }

    [DataContract]
    [Table]
    public class RoleCommandView : BuiltInEntity
    {
        [DataMember]
        [Column(Type=DbType.Int32,IsPrimaryKey=true)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int SourceRole_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int TargetRole_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Command_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool IsManagerCommand { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Command_Name { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int CommandType { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Money { get; set; }       
		[DataMember]
		[Column(Type = DbType.Int32)]
		public int Command_Application_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string ActionName { get; set; }

        private RoleCommand roleCommand = null;
        public RoleCommand GetRoleCommand()
        {
            if (roleCommand == null)
            {
                roleCommand = new RoleCommand
                {
                    Id = Id,
                    Command_Id = Command_Id,
                    IsBuiltIn = IsBuiltIn,
                    IsManagerCommand = IsManagerCommand,
                    SourceRole_Id = SourceRole_Id,
                    TargetRole_Id = TargetRole_Id

                };
            }
            return roleCommand;
        }

        public RoleCommandView(RoleCommand rc, Command cmd)
        {
            roleCommand = rc;
            if (roleCommand != null)
            {
                Id = roleCommand.Id;
                Command_Id = roleCommand.Command_Id;
                IsBuiltIn = roleCommand.IsBuiltIn;
                IsManagerCommand = roleCommand.IsManagerCommand;
                SourceRole_Id = roleCommand.SourceRole_Id;
                TargetRole_Id = roleCommand.TargetRole_Id;
            }
            if (cmd != null && Command_Id == cmd.Id)
            {
                Command_Name = cmd.Name;
                Application_Id = cmd.Application_Id;
                CommandType = cmd.CommandType;
                Money = cmd.Money;
            }
        }

        public RoleCommandView()
            : this(null,null)
        {
        }
    }
}
