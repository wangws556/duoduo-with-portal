/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. YoYoStudio CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Data;
using YoYoStudio.Common;

namespace YoYoStudio.Common.ORM
{
    public class SPParameter
    {
        public string Name { get; set; }
        public DbType Type { get; set; }
        public object Value { get; set; }
        public ParameterDirection Direction { get; set; }
        public SPParameter()
        {
            Direction = ParameterDirection.Input;
        }
    }

	public interface IORMapper : IEntityAccesser
	{
		string And { get; }
		string Comma { get; }
		string ConditionSql { get; }
		string CountSql { get; }
        string MaxSql { get; }
		string DeleteSql { get; }
		string Dot { get; }
		string Equal { get; }
		string FindAttributeSql { get; }
		string FindSql { get; }
		string InsertSql { get; }
		string Join { get; }
		string KeySqlValueParameter { get; }
		string KeySqlValueParameterName { get; }
		string On { get; }
		string OrderSql { get; }
		string SemiColon { get; }
		string SetSql { get; }
		string UpdateSql { get; }
		string UpdateSqlValueParameter { get; }
		string UpdateSqlValueParameterName { get; }
		string WhereSql { get; }
		string LeftSquare { get; }
		string RightSquare { get; }
		string RowNumSql { get; }
		string BetweenSql { get; }

		IDbConnection NewConnection();

		void CreateDatabase();
		void DropDatabaseIfExist();        
	}

	[Serializable]
	public abstract class Entity
	{
		public bool Loaded { get; set; }
	}
}
