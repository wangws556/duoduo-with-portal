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
    public class User : BuiltInImagedEntity
	{
		#region Entity Properties
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string Name { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string NickName { get; set; }
        [DataMember]
		[Column(Type = DbType.Int32)]
		public int ApplicationCreated_Id { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string Password { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string PasswordQuestion { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string PasswordAnswer { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string Email { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string Country { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string State { get; set; }
        [DataMember]
		[Column(Type = DbType.String)]
		public string City { get; set; }
        [DataMember]
		[Column(Type = DbType.Int32)]
		public int? Age { get; set; }
        [DataMember]
        [Column(Type = DbType.Boolean)]
        public bool? Gender { get; set; }
        [DataMember]
		[Column(Type = DbType.DateTime)]
		public DateTime? LastLoginTime { get; set; }        

		#endregion

        public override Core.ImageType GetImageType()
        {
            return BuiltIns.HeaderImageType;
        }
        public User()
        {
            Id = BuiltIns.UserDefinedUserStartId;
        }

        public override void AdjustId(int suggestedId)
        {
            if (Id < BuiltIns.UserDefinedUserStartId)
            {
                base.AdjustId(suggestedId);
            }
        }
				
	}
    [DataContract]
    [Table]
    public class UserApplicationInfo : ModelEntity 
    {		
        [DataMember]
        [Column(IsPrimaryKey=true,Type = DbType.Int32)]
        public int Application_Id { get; set; }
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public int User_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Money { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? AgentMoney { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int? Score { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Role_Id { get; set; }
    }

    [DataContract]
    [Table]
    public class UserConfig : IdedEntity
    {
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int User_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Tag { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int Order { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Value { get; set; }
    }

    public enum UserTag
    {

    }
}
