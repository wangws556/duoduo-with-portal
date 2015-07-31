using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Persistent.ORMapping;

namespace YoYoStudio.Model.Core
{
    [DataContract]
	[Table]
	public class UserLevel : Entity
	{
        [DataMember]
		[Column(IsPrimaryKey=true,Type = DbType.Int32)]
		public int Id { get; set; }
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
		public int? Image_Id { get; set; }
	}
}
