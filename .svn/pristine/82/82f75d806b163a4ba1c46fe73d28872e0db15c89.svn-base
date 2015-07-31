/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. YoYoStudio CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoYoStudio.Common.ORM
{
	[AttributeUsage(AttributeTargets.Property )]
	public class ColumnAttribute : Attribute
	{
		public static DbType NullDbType = (DbType)(-1);
		public string Name;
		public DbType Type = NullDbType;
		public int Size = -1;
		public bool IsPrimaryKey = false;
		public bool AllowNull = false;
        public bool IsIdentity = false;
	}

	public enum CollectionType
	{
		OneToMany,
		ManyToMany,
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class CollectionAttribute : Attribute
	{
		public CollectionType CollectionType = CollectionType.OneToMany;
		public string Name;
		public Type TargetType;
		public string IntermediateTableName;
		public string MapSourceKey;
		public string MapTargetKey;
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class TableAttribute : Attribute
	{
		public string Name;
		public bool IsRoot = true;
		public string SchemaName = "dbo";		
	}
}
