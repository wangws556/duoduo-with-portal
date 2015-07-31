/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. YoYoStudio CONFIDENTIAL
/// </copyright>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.Common.Extensions;

namespace YoYoStudio.Common.ORM
{
	public class ColumnInfo
	{
		public DbType Type { get; set; }
		public PropertyInfo PropertyInfo { get; set; }
		public string Name { get; set; }
		public int Size { get; set; }
		public bool IsPrimaryKey { get; set; }
		public bool AllowsNull { get; set; }
        public bool IsIdentity { get; set; }

		private ColumnInfo()
		{

		}

		public void SetValue(object obj, object value)
		{
			PropertyInfo.SetValue(obj, value == DBNull.Value ? null : value,null);
		}

		public object GetValue(object obj)
		{
			object result = PropertyInfo.GetValue(obj,null);
			if (result == null)
			{
				result = DBNull.Value;
			}
			return result;
		}

		public static ColumnInfo GetInfo(PropertyInfo propertyInfo)
		{
			ColumnInfo column = null;
			ColumnAttribute attribute = propertyInfo.GetCustomAttribute<ColumnAttribute>(false);
			if (attribute != null)
			{
				column = new ColumnInfo();
				if (string.IsNullOrEmpty(attribute.Name))
				{
					column.Name = propertyInfo.Name;
				}
				else
				{
					column.Name = attribute.Name;
				}
				column.Size = attribute.Size;
				column.IsPrimaryKey = attribute.IsPrimaryKey;
				column.Type = attribute.Type;
				column.AllowsNull = attribute.AllowNull;
				column.PropertyInfo = propertyInfo;
                column.IsIdentity = attribute.IsIdentity;
			}
			return column;
		}
	}

	public class CollectionInfo
	{
		public FieldInfo FieldInfo { get; set; }
		public CollectionType CollectionType;
		public string Name;
		public Type TargetType;
		public string IntermediateTableName;
		public string MapSourceKey;
		public string MapTargetKey;

		private CollectionInfo()
		{
		}

		public IEnumerable GetValue(object obj)
		{
			return FieldInfo.GetValue(obj) as IEnumerable;
		}

		public static CollectionInfo GetInfo(FieldInfo fieldInfo)
		{
			CollectionInfo info = null;
			CollectionAttribute attribute = fieldInfo.GetCustomAttribute<CollectionAttribute>(false);
			if (attribute != null)
			{
				info = new CollectionInfo();
				info.CollectionType = attribute.CollectionType;
				info.Name = string.IsNullOrEmpty(attribute.Name)
					? Char.ToUpper(fieldInfo.Name[0]) + fieldInfo.Name.Substring(1)
					: attribute.Name; ;
				info.MapSourceKey = string.IsNullOrEmpty(attribute.MapSourceKey)
					? fieldInfo.DeclaringType.Name + "_Id"
					: attribute.MapSourceKey;
				info.TargetType = attribute.TargetType == null
					? fieldInfo.FieldType.GetGenericArguments()[0]
					: attribute.TargetType;
				info.MapTargetKey = string.IsNullOrEmpty(attribute.MapTargetKey)
					? info.TargetType.Name + "_Id"
					: attribute.MapTargetKey;
				info.IntermediateTableName = attribute.IntermediateTableName;
				info.FieldInfo = fieldInfo;
			}
			return info;
		}
	}

	public abstract class ColumnCollectionInfo
	{
		protected static Hashtable cache = new Hashtable();
		protected Hashtable columns = new Hashtable(StringComparer.OrdinalIgnoreCase);
		protected Hashtable allColumns = new Hashtable(StringComparer.OrdinalIgnoreCase);
		protected Hashtable primaryKeys = new Hashtable(StringComparer.OrdinalIgnoreCase);
		protected Hashtable collections = new Hashtable(StringComparer.OrdinalIgnoreCase);

		protected ColumnCollectionInfo()
		{

		}

		public abstract string Name { get; protected set; }
		public abstract string SchemaName { get; protected set; }

        public string GetNameWithSquare(string leftSquare, string rightSquare)
        {
            return leftSquare + Name + rightSquare;
        }

		public ColumnCollectionInfo BaseInfo { get; set; }

		public CollectionInfo GetCollectionInfo(string collectionName)
		{
			if (collections.Contains(collectionName))
			{
				return collections[collectionName] as CollectionInfo;
			}
			return BaseInfo == null ? null : BaseInfo.GetCollectionInfo(collectionName);
		}

		public ColumnInfo this[string name]
		{
			get
			{
				if (allColumns.ContainsKey(name))
				{
					return (ColumnInfo)allColumns[name];
				}
				else if (PrimaryKeys.ContainsKey(name))
				{
					return (ColumnInfo)PrimaryKeys[name];
				}
				return null;
			}
		}

		public ICollection PrimaryKeyNames
		{
			get
			{
				return PrimaryKeys.Keys;
			}
		}

		public ICollection ColumnNames
		{
			get
			{
				return columns.Keys;
			}
		}

		public ICollection AllColumnNames
		{
			get
			{
				return allColumns.Keys;
			}
		}

		public abstract string GetFromClause(string join, string on, string equal, string dot, string leftSquare, string rightSquare, string and, bool includeSchema);

		public Hashtable PrimaryKeys
		{
			get
			{
				if (BaseInfo == null)
				{
					return primaryKeys;
				}
				else
				{
					return BaseInfo.PrimaryKeys;
				}
			}
		}

		public Hashtable Columns
		{
			get
			{
				return columns;
			}
		}

		public Hashtable AllColumns
		{
			get
			{
				return allColumns;
			}
		}

		public Hashtable Collections
		{
			get
			{
				return collections;
			}
		}

		protected string keys;
		public virtual string GetKeys(string comma, string dot, string leftSquare, string rightSquare, bool includeTableName = false)
		{
			if (string.IsNullOrEmpty(keys))
			{				
				List<string> names = new List<string>();
				foreach(string name in PrimaryKeyNames)
				{   
                    if (includeTableName)
                    {
                        names.Add(leftSquare + Name + rightSquare + dot + leftSquare + name + rightSquare);
                    }
                    else
                    {
                        names.Add(leftSquare + name + rightSquare);
                    }
				}
				keys = string.Join(comma, names);
			}
			return keys;
		}

		protected string selectColumns;
		protected string selectColumnsWithKey;
		public virtual string GetSelectColumns(string comma, bool includeKey, bool includeIdentity,string dot, string leftSquare, string rightSquare, bool includeTableName = false)
		{
			string result = includeKey ? selectColumnsWithKey : selectColumns;
			if (string.IsNullOrEmpty(result))
			{
				List<string> names = new List<string>();
				if (includeKey)
				{
					foreach (string name in PrimaryKeyNames)
					{
                        ColumnInfo col = primaryKeys[name] as ColumnInfo;
                        if (!includeIdentity && col.IsIdentity)
                        {
                            continue;
                        }
                        if (includeTableName)
                        {
                            names.Add(leftSquare + Name + rightSquare + dot + leftSquare + name + rightSquare);
                        }
                        else
                        {
                            names.Add(leftSquare + name + rightSquare);
                        }
					}
				}
				foreach (string name in ColumnNames)
				{
					names.Add(leftSquare + name + rightSquare);
				}
				result = string.Join(comma, names.ToArray());

				if (includeKey)
				{
					selectColumnsWithKey = result;
				}
				else
				{
					selectColumns = result;
				}
			}
			return result;
		}
		protected string allSelectColumns;
		protected string allSelectColumnsWithKey;
        public virtual string GetAllSelectColumns(string comma, bool includeKey, string dot, string leftSquare, string rightSquare, bool includeTableName = false)
        {
            string result = includeKey ? allSelectColumnsWithKey : allSelectColumns;
            if (string.IsNullOrEmpty(result))
            {
                List<string> names = new List<string>();
                if (includeKey)
                {
                    foreach (string name in PrimaryKeyNames)
                    {
                        if (includeTableName)
                        {
                            names.Add(leftSquare + Name + rightSquare + dot + leftSquare + name + rightSquare);
                        }
                        else
                        {
                            names.Add(leftSquare + name + rightSquare);
                        }
                    }
                }
                foreach (string name in AllColumnNames)
                {
                    names.Add(leftSquare + name + rightSquare);
                }
                result = string.Join(comma, names.ToArray());
                if (includeKey)
                {
                    allSelectColumnsWithKey = result;
                }
                else
                {
                    allSelectColumns = result;
                }

            }
            return result;
        }

		protected virtual void SetInfo(Type type)
		{
			BaseInfo = GetInfo(type.BaseType);
			if (BaseInfo != null)
			{
				foreach (ColumnInfo col in BaseInfo.AllColumns.Values)
				{
					allColumns[col.Name] = col;
				}
			}
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public );
			foreach (var property in properties)
			{
				ColumnInfo column = ColumnInfo.GetInfo(property);
				if (column != null)
				{
					if (column.IsPrimaryKey)
					{
						primaryKeys[column.Name] = column;
					}
					else
					{
						columns[column.Name] = column;
						allColumns[column.Name] = column;
					}
				}
			}

			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (var field in fields)
			{
				CollectionInfo collection = CollectionInfo.GetInfo(field);
				if (collection != null)
				{
					collections[collection.Name] = collection;
				}
			}
		}

		public static ColumnCollectionInfo GetInfo(Type type)
		{
			ColumnCollectionInfo result = cache[type] as ColumnCollectionInfo;
			if (result == null)
			{
				result = TableInfo.GetInfo(type);
				if (result != null)
				{
					cache[type] = result;
				}
			}
			return result;
		}

	}

	public class TableInfo : ColumnCollectionInfo
	{
		public override string Name { get; protected set; }
		public override string SchemaName { get; protected set; }

		private TableInfo()
		{
		}

		protected override void SetInfo(Type type)
		{
			base.SetInfo(type);
			TableAttribute attribute = type.GetCustomAttribute<TableAttribute>(false);
			if (attribute != null)
			{
				Name = string.IsNullOrEmpty(attribute.Name) ? type.Name : attribute.Name;
				SchemaName = string.IsNullOrEmpty(attribute.SchemaName) ? "dbo" : attribute.SchemaName;
			}
		}

		public static new TableInfo GetInfo(Type type)
		{
			TableInfo info = null;
			TableAttribute attribute = type.GetCustomAttribute<TableAttribute>();
			if (attribute != null)
			{
				info = new TableInfo();
				info.SetInfo(type);
			}
			return info;
		}

		protected string fromClause;
		protected string fromClauseWithSchema;
		public override string GetFromClause(string join, string on, string equal, string dot, string leftSquare, string rightSquare, string and, bool includeSchema)
		{
			string result = includeSchema ? fromClauseWithSchema : fromClause;
			if (string.IsNullOrEmpty(result))
			{
				if (BaseInfo == null)
				{
					result = includeSchema ? (leftSquare + SchemaName + rightSquare + dot + leftSquare + Name + rightSquare) : (leftSquare + Name + rightSquare);
				}
				else
				{
					string baseFromClause = BaseInfo.GetFromClause(join, on, equal, dot, leftSquare, rightSquare, and, includeSchema);
					if (includeSchema)
					{
						result = baseFromClause + join + leftSquare + SchemaName + rightSquare + leftSquare + Name + rightSquare;
					}
					else
					{
						result = baseFromClause + join + leftSquare + Name + rightSquare;
					}
					if (PrimaryKeys.Count > 0)
					{
						result += on;
						foreach (ColumnInfo primaryKey in PrimaryKeys.Values)
						{
							if (includeSchema)
							{
								result += leftSquare + SchemaName + rightSquare + leftSquare + Name + rightSquare + dot + leftSquare + primaryKey.Name + rightSquare + equal + leftSquare + SchemaName + rightSquare + dot + leftSquare + Name + rightSquare + dot + leftSquare + primaryKey.Name + rightSquare;
							}
							else
							{
								result += leftSquare + Name + rightSquare + dot + leftSquare + primaryKey.Name + rightSquare + equal + leftSquare + BaseInfo.Name + rightSquare + dot + leftSquare + primaryKey.Name + rightSquare;
							}
						}
					}
				}
				if (includeSchema)
				{
					fromClauseWithSchema = result;
				}
				else
				{
					fromClause = result;
				}
			}
			return result;
		}
	}
}
