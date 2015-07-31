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
    public class Role : BuiltInImagedEntity
    {
        #region Entity Members
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
        [Column(Type = DbType.String)]
        public int Order { get; set; }

        #endregion

        public List<RoleCommandView> RoleCommands { get; set; }

        
        public override Core.ImageType GetImageType()
        {
            return BuiltIns.RoleImageType;
        }

        public Role()
        {
            Id = BuiltIns.UserDefinedRoleStartId;
        }
    }
}
