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
    public class Application : BuiltInImagedEntity
	{
		#region Entity Properties
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
		[Column(Type=DbType.String)]
		public string Name { get; set; }
        [DataMember]
		[Column(Type=DbType.String)]
		public string HomeAddress { get; set; }
        [DataMember]
		[Column(Type=DbType.String)]
		public string Description { get; set; }
        

		#endregion

        public Application()
        {
            Id = BuiltIns.UserDefinedApplicationStartId;
        }

        public List<Role> Roles { get; set; }

        public override Core.ImageType GetImageType()
        {
            return BuiltIns.ApplicationImageType;
        }               
	}
}
