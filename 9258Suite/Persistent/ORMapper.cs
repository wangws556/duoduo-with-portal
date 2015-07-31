/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. YoYoStudio CONFIDENTIAL
/// </copyright>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Persistent
{
    
	public abstract class ORMapper : IORMapper
	{
		#region Insert

		public void Insert<T>(T obj) where T : Entity, new()
		{
			using (IDbConnection conn = NewConnection())
			{
				conn.Open();

				GetInsertCommands(obj, conn).ForEach(command => { using (command) { command.ExecuteNonQuery(); } });
			}
		}

		public List<IDbCommand> GetInsertCommands<T>(T obj, IDbConnection connection) where T : Entity, new()
		{
			return GetInsertCommands(obj, ColumnCollectionInfo.GetInfo(obj.GetType()), connection);
		}

		private List<IDbCommand> GetInsertCommands(Object obj, ColumnCollectionInfo dataInfo, IDbConnection connection)
		{
			List<IDbCommand> result = new List<IDbCommand>();
			if (dataInfo.BaseInfo != null)
			{
				result.AddRange(GetInsertCommands(obj, dataInfo.BaseInfo, connection));
			}
			if (!string.IsNullOrEmpty(dataInfo.Name))
			{
				IDbCommand command = connection.CreateCommand();
				command.CommandText = GetInsertStatement(obj, dataInfo, command);
				result.Add(command);
			}
			// add commands for many to many relationships
			var manyToManyInfos = dataInfo.Collections.Values.Cast<CollectionInfo>().Where(t => t.CollectionType == CollectionType.ManyToMany);
			foreach (CollectionInfo info in manyToManyInfos)
			{
				result.AddRange(GetInsertCommands(obj, dataInfo, info, connection));
			}

			return result;
		}

		private List<IDbCommand> GetInsertCommands(Object obj, ColumnCollectionInfo dataInfo, CollectionInfo collectionInfo, IDbConnection connection)
		{
			List<IDbCommand> result = new List<IDbCommand>();

			IEnumerable targets = collectionInfo.GetValue(obj);
			if (targets != null)
			{
				foreach (var target in targets)
				{
					IDbCommand command = connection.CreateCommand();
					command.CommandText = GetInsertStatement(obj, target, dataInfo, collectionInfo, command);
					result.Add(command);
				}
			}

			return result;
		}

		private string GetInsertStatement(Object obj, ColumnCollectionInfo dataInfo, IDbCommand command)
		{
			int index = 0;
			List<string> values = new List<string>();
			foreach (ColumnInfo column in dataInfo.PrimaryKeys.Values)
			{
                if (!column.IsIdentity)
                {
                    string parameterName = string.Format(UpdateSqlValueParameter, index++);
                    values.Add(parameterName);
                    AddParameter(column, parameterName, column.GetValue(obj), command);
                }
			}
			foreach (ColumnInfo column in dataInfo.Columns.Values)
			{
				string parameterName = string.Format(UpdateSqlValueParameter, index++);
				values.Add(parameterName);
				AddParameter(column, parameterName, column.GetValue(obj), command);
			}
			

			return string.Format(InsertSql, dataInfo.Name, dataInfo.GetSelectColumns(Comma, true,false, Dot, LeftSquare, RightSquare), string.Join(Comma, values));
		}

		private string GetInsertStatement(Object src, Object target, ColumnCollectionInfo srcDataInfo, CollectionInfo collectionInfo, IDbCommand command)
		{
			var targetDataInfo = ColumnCollectionInfo.GetInfo(target.GetType());

			int index = 0;
			List<string> values = new List<string>();
			foreach (ColumnInfo column in srcDataInfo.PrimaryKeys.Values)
			{
                if (!column.IsIdentity)
                {
                    string parameterName = string.Format(UpdateSqlValueParameter, index++);
                    values.Add(parameterName);
                    AddParameter(column, parameterName, column.GetValue(src), command);
                }
			}
			foreach (ColumnInfo column in targetDataInfo.PrimaryKeys.Values)
			{
                if (!column.IsIdentity)
                {
                    string parameterName = string.Format(UpdateSqlValueParameter, index++);
                    values.Add(parameterName);
                    AddParameter(column, parameterName, column.GetValue(target), command);
                }
			}

			return string.Format(
				InsertSql,
				collectionInfo.IntermediateTableName,
				collectionInfo.MapSourceKey + Comma + collectionInfo.MapTargetKey,
				string.Join(Comma, values));
		}

		#endregion

		#region Update

		public void Update<T>(T obj) where T : Entity, new()
		{
			using (IDbConnection conn = NewConnection())
			{
				conn.Open();

				GetUpdateCommands(obj, conn).ForEach(command => { using (command) { command.ExecuteNonQuery(); } });
			}
		}

		public List<IDbCommand> GetUpdateCommands<T>(T obj, IDbConnection connection) where T : Entity, new()
		{
			return GetUpdateCommands(obj, ColumnCollectionInfo.GetInfo(obj.GetType()), connection);
		}

		private List<IDbCommand> GetUpdateCommands(Object obj, ColumnCollectionInfo dataInfo, IDbConnection connection)
		{
			List<IDbCommand> result = new List<IDbCommand>();
			if (dataInfo.BaseInfo != null)
			{
				result.AddRange(GetUpdateCommands(obj, dataInfo.BaseInfo, connection));
			}

			if (dataInfo.Columns.Count > 0)
			{
				IDbCommand command = connection.CreateCommand();
				command.CommandText = GetUpdateStatement(obj, dataInfo, command);
				result.Add(command);
			}

			var manyToManyInfos = dataInfo.Collections.Values.Cast<CollectionInfo>().Where(t => t.CollectionType == CollectionType.ManyToMany);
			foreach (CollectionInfo info in manyToManyInfos)
			{
				result.AddRange(GetUpdateCommands(obj, dataInfo, info, connection));
			}

			return result;
		}

		private List<IDbCommand> GetUpdateCommands(Object obj, ColumnCollectionInfo dataInfo, CollectionInfo collectionInfo, IDbConnection conn)
		{
			List<IDbCommand> commands = new List<IDbCommand>();

			IEnumerable colllectionValue = collectionInfo.GetValue(obj);
			if (colllectionValue != null)
			{
				var newTargets = colllectionValue.Cast<Object>().ToList();
				if (newTargets.Count == 0)
				{
					commands.Add(GetClearCollectionCommand(obj, dataInfo, collectionInfo, conn));
				}
				else
				{
					var targetDataInfo = ColumnCollectionInfo.GetInfo(collectionInfo.TargetType);
					ColumnInfo targetPrimaryKeyInfo = targetDataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();

					var oldPrimaryKeys = GetCollectionTargets(obj, dataInfo, collectionInfo, targetPrimaryKeyInfo.GetValue(newTargets.First()), conn);
					foreach (var newTarget in newTargets)
					{
						if (!oldPrimaryKeys.Contains(targetPrimaryKeyInfo.GetValue(newTarget)))
						{
							IDbCommand command = conn.CreateCommand();
							command.CommandText = GetInsertStatement(obj, newTarget, dataInfo, collectionInfo, command);
							commands.Add(command);
						}
					}

					var newPrimaryKeys = newTargets.Select(t => targetPrimaryKeyInfo.GetValue(t));
					foreach (var primaryKey in oldPrimaryKeys)
					{
						if (newPrimaryKeys.Where(t => t.Equals(primaryKey)).Count() == 0)
						{
							commands.Add(GetClearCollectionCommand(obj, dataInfo, collectionInfo, conn, targetPrimaryKeyInfo, primaryKey));
						}
					}
				}
			}

			return commands;
		}

		private List<T> GetCollectionTargets<T>(Object src, ColumnCollectionInfo dataInfo, CollectionInfo collectionInfo, T targetKey, IDbConnection conn)
			where T : class, new()
		{
			List<T> result = new List<T>();

			using (var command = conn.CreateCommand())
			{
				string sql = string.Format(FindSql, collectionInfo.MapTargetKey, collectionInfo.IntermediateTableName);

				ColumnInfo primaryKeyColumn = dataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();
				string parameterName = string.Format(UpdateSqlValueParameter, 0);
				string whereSql = collectionInfo.MapSourceKey + Equal + parameterName;
				AddParameter(primaryKeyColumn, parameterName, primaryKeyColumn.GetValue(src), command);

				sql = string.Format(WhereSql, sql, whereSql);

				command.CommandText = sql;
				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						T obj = reader.GetValue(0) as T;
						result.Add(obj);
					}
				}
			}

			return result;
		}

		private IDbCommand GetClearCollectionCommand(
			Object obj,
			ColumnCollectionInfo dataInfo,
			CollectionInfo collectionInfo,
			IDbConnection conn,
			ColumnInfo targetPrimaryKeyInfo = null,
			object targetPrimaryKey = null)
		{
			IDbCommand command = conn.CreateCommand();

			string sql = string.Format(DeleteSql, collectionInfo.IntermediateTableName);

			ColumnInfo primaryKeyColumn = dataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();
			string parameterName = string.Format(UpdateSqlValueParameter, 0);
			string whereSql = collectionInfo.MapSourceKey + Equal + parameterName;
			AddParameter(primaryKeyColumn, parameterName, primaryKeyColumn.GetValue(obj), command);

			if (targetPrimaryKeyInfo != null && targetPrimaryKey != null)
			{
				parameterName = string.Format(UpdateSqlValueParameter, 1);
				whereSql = whereSql + And + collectionInfo.MapTargetKey + Equal + parameterName;
				AddParameter(targetPrimaryKeyInfo, parameterName, targetPrimaryKey, command);
			}

			sql = string.Format(WhereSql, sql, whereSql);
			command.CommandText = sql;

			return command;
		}

		private string GetUpdateStatement(Object obj, ColumnCollectionInfo dataInfo, IDbCommand command)
		{
			int index = 0;
			List<string> values = new List<string>();
			foreach (ColumnInfo column in dataInfo.Columns.Values)
			{
				if (!column.IsPrimaryKey)
				{
					string parameterName = string.Format(UpdateSqlValueParameter, index++);
					values.Add(string.Format(SetSql, column.Name, parameterName));
					AddParameter(column, parameterName, column.GetValue(obj), command);
				}
			}
			string sql = string.Format(UpdateSql, dataInfo.Name, string.Join(Comma, values));
			string condition = GetKeyCondition(obj, dataInfo, command);
			if (!string.IsNullOrEmpty(condition))
			{
				sql = string.Format(WhereSql, sql, condition);
			}
			return sql;
		}

		#endregion

		#region Delete

		public void Delete<T>(T obj) where T : Entity, new()
		{
			using (IDbConnection conn = NewConnection())
			{
				conn.Open();

				GetDeleteCommands(obj, conn).ForEach(command => { using (command) { command.ExecuteNonQuery(); } });
			}
		}

		public List<IDbCommand> GetDeleteCommands<T>(T obj, IDbConnection connection) where T : Entity, new()
		{
			return GetDeleteCommands(obj, ColumnCollectionInfo.GetInfo(obj.GetType()), connection);
		}

		private List<IDbCommand> GetDeleteCommands(Object obj, ColumnCollectionInfo dataInfo, IDbConnection connection)
		{
			List<IDbCommand> result = new List<IDbCommand>();

			IDbCommand command = connection.CreateCommand();
			command.CommandText = GetDeleteStatement(obj, dataInfo, command);
			result.Add(command);

			if (dataInfo.BaseInfo != null)
			{
				result.AddRange(GetDeleteCommands(obj, dataInfo.BaseInfo, connection));
			}

			return result;
		}

		private string GetDeleteStatement(Object obj, ColumnCollectionInfo dataInfo, IDbCommand command)
		{
			string sql = string.Format(DeleteSql, dataInfo.Name);
			string condition = GetKeyCondition(obj, dataInfo, command);
			if (!string.IsNullOrEmpty(condition))
			{
				sql = string.Format(WhereSql, sql, condition);
			}
			return sql;
		}

		#endregion

		#region Select

		public void Load<T>(T obj) where T : Entity, new()
		{
			using (IDbConnection conn = NewConnection())
			{
				conn.Open();

				using (IDbCommand command = GetSelectCommand(obj, conn))
				{
					var dataInfo = ColumnCollectionInfo.GetInfo(obj.GetType());
					using (IDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
					{
						if (reader.Read())
						{
							for (int i = 0; i < reader.FieldCount; i++)
							{
								dataInfo[reader.GetName(i)].SetValue(obj, reader.GetValue(i));
							}
							obj.Loaded = true;
						}
					}
				}
			}
		}

		public IDbCommand GetSelectCommand<T>(T obj, IDbConnection conn) where T : Entity, new()
		{
			IDbCommand command = conn.CreateCommand();
			var dataInfo = ColumnCollectionInfo.GetInfo(obj.GetType());
			string sql = string.Format(FindSql, dataInfo.GetAllSelectColumns(
				Comma,
				true,
				Dot, LeftSquare, RightSquare),
				dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, false));
			string condition = GetKeyCondition(obj, dataInfo, command);
			if (!string.IsNullOrEmpty(condition))
			{
				sql = string.Format(WhereSql, sql, condition);
			}
			command.CommandText = sql;
			return command;
		}

		#endregion

		#region Search

		public int GetCount<T>(string condition = "") where T : Entity, new()
		{
			using (IDbConnection conn = NewConnection())
			{
				conn.Open();
				IDbCommand command = GetCountCommand<T>(conn, condition);
                object result = command.ExecuteScalar();
                return (int)result;
			}
		}

        public int GetMax<T>() where T : Entity, new()
        {
            if (GetCount<T>() == 0)
            {
                return 0;
            }
            else
            {
                using (IDbConnection conn = NewConnection())
                {
                    conn.Open();
                    IDbCommand command = GetMaxCommand<T>(conn);
                    object result = command.ExecuteScalar();
                    return (int)result;
                }
            }
        }

        public IDbCommand GetMaxCommand<T>(IDbConnection conn) where T : Entity, new()
        {
            IDbCommand command = conn.CreateCommand();
            var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T));
            command.CommandText = string.Format(MaxSql,"[Id]",dataInfo.GetNameWithSquare(LeftSquare,RightSquare));
            return command;
        }

		public IDbCommand GetCountCommand<T>(IDbConnection conn, string condition = "") where T : Entity, new()
		{
			IDbCommand command = conn.CreateCommand();
			var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T));
			string sql = string.Empty;
			sql = string.Format(CountSql, dataInfo.GetNameWithSquare(LeftSquare,RightSquare));
			if (!string.IsNullOrEmpty(condition))
			{
				sql = string.Format(WhereSql, sql, condition);
			}
			command.CommandText = sql;
			return command;
		}

		public List<T> Search<T>(string condition = "", string order = "", int start = -1, int count = -1) where T : Entity, new()
		{
			using (IDbConnection conn = NewConnection())
			{
				conn.Open();
				return ExecuteSearchCommand<T>(GetSearchCommand<T>(conn, condition, order,start,count));
			}
		}

		public List<TEntity> Search<TOwner, TEntity>(TOwner owner, Expression<Func<TOwner, IEnumerable>> exp, string order = "") where TEntity : Entity, new()
		{
			using (IDbConnection conn = NewConnection())
			{
				conn.Open();
				return ExecuteSearchCommand<TEntity>(GetSearchCommand<TOwner, TEntity>(conn, owner, exp, order));
			}
		}

		public IDbCommand GetSearchCommand<T>(IDbConnection conn, string condition = "", string order = "", int start = -1, int count = -1) where T : Entity, new()
		{
			IDbCommand command = conn.CreateCommand();
			var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T));

            string sql = string.Empty;

			bool useRowNumber = start > 0 || count > 0;
			string rowNumberOrder = string.IsNullOrEmpty(order) ? dataInfo.GetKeys(Comma, Dot, LeftSquare, RightSquare) : order;
            string rowNumberWhere = string.Empty;
			
            string rowNumberCondition = string.Empty;
            if (useRowNumber)
            {
                int startNumber = start > 0 ? start : 1;
                if (count > 0)
                {
                    rowNumberCondition += string.Format(BetweenSql, "RN", startNumber, startNumber + count - 1);
                }
                else
                {
                    rowNumberCondition += string.Format("(RN > {0})", startNumber);
                }

                rowNumberWhere = string.IsNullOrEmpty(condition) ? string.Empty : " WHERE " + condition;                

                sql = string.Format(RowNumSql, rowNumberOrder,
                    dataInfo.GetAllSelectColumns(Comma, true, Dot, LeftSquare, RightSquare),
                    dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, false), rowNumberWhere);
                
                if (!string.IsNullOrEmpty(rowNumberCondition))
                {
                    sql = string.Format(WhereSql, sql, rowNumberCondition);
                }
            }
            else
            {
                sql = string.Format(FindSql, dataInfo.GetAllSelectColumns(Comma, true, Dot, LeftSquare, RightSquare), dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, false));
                if (!string.IsNullOrEmpty(condition))
                {
                    sql = string.Format(WhereSql, sql, condition);
                }
                if (!string.IsNullOrEmpty(order))
                {
                    sql = string.Format(OrderSql, sql, order);
                }
            }

			command.CommandText = sql;
			return command;
		}

		public IDbCommand GetSearchCommand<TOwner, TEntity>(IDbConnection conn, TOwner owner, Expression<Func<TOwner, IEnumerable>> exp, string order = "")
			where TEntity : Entity, new()
		{
			var entityDataInfo = ColumnCollectionInfo.GetInfo(typeof(TEntity));
			var ownerDataInfo = ColumnCollectionInfo.GetInfo(owner.GetType());

			MemberExpression body = exp.Body as MemberExpression;
			if (body == null)
			{
				throw new ArgumentException();
			}

			var collectionName = body.Member.Name;

			IDbCommand command = conn.CreateCommand();
			string sql = string.Format(
				FindSql,
				entityDataInfo.GetAllSelectColumns(Comma, true, Dot, LeftSquare, RightSquare),
				entityDataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, false));

			CollectionInfo collectionInfo = ownerDataInfo.GetCollectionInfo(collectionName);
			if (collectionInfo != null)
			{
				// for one to many relationship
				if (collectionInfo.CollectionType == CollectionType.OneToMany)
				{
					ColumnInfo keyColumn = ownerDataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();
					string parameterName = string.Format(KeySqlValueParameter, 0);
					string whereSql = LeftSquare + collectionInfo.MapSourceKey + RightSquare + Equal + parameterName;
					sql = string.Format(WhereSql, sql, whereSql);
					AddParameter(keyColumn, parameterName, keyColumn.GetValue(owner), command);
				}
				// for many to many relationship
				else
				{
					string intermediateTableName = LeftSquare + collectionInfo.IntermediateTableName + RightSquare;
					string sourceKey = LeftSquare + ownerDataInfo.Name + RightSquare + Dot + ownerDataInfo.PrimaryKeyNames.OfType<string>().Single();
					string targetKey = LeftSquare + entityDataInfo.Name + RightSquare + Dot + entityDataInfo.PrimaryKeyNames.OfType<string>().Single();
					string toSourceKey = intermediateTableName + Dot + collectionInfo.MapSourceKey;
					string toTargetKey = intermediateTableName + Dot + collectionInfo.MapTargetKey;
					string joinSql =
						Join + intermediateTableName + On + targetKey + Equal + toTargetKey +
						Join + LeftSquare + ownerDataInfo.Name + RightSquare + On + toSourceKey + Equal + sourceKey;

					string parameterName = string.Format(KeySqlValueParameter, 0);
					string whereSql = sourceKey + Equal + parameterName;
					ColumnInfo keyColumnInfo = ownerDataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();
					AddParameter(keyColumnInfo, parameterName, keyColumnInfo.GetValue(owner), command);
					sql = string.Format(WhereSql, sql + joinSql, whereSql);
				}

				if (!string.IsNullOrEmpty(order))
				{
					sql = string.Format(OrderSql, sql, order);
				}
			}
			command.CommandText = sql;
			return command;
		}
		public IDbCommand GetSearchCommand<T>(IDbConnection conn, string field, Guid ownerId, string order = "") where T : Entity, new()
		{
			IDbCommand command = conn.CreateCommand();
			var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T));
			string sql = string.Empty;
			sql = string.Format(FindSql, dataInfo.GetAllSelectColumns(Comma, true, Dot, LeftSquare, RightSquare), dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, false));

			if (!string.IsNullOrEmpty(field))
			{
				ColumnInfo column = dataInfo[field];
				if (column != null)
				{
					string parameterName = string.Format(KeySqlValueParameter, 0);
					string whereSql = LeftSquare + field + RightSquare + Equal + parameterName;
					sql = string.Format(WhereSql, sql, whereSql);
					AddParameter(column, parameterName, ownerId, command);
				}
			}
			if (!string.IsNullOrEmpty(order))
			{
				sql = string.Format(OrderSql, sql, order);
			}
			command.CommandText = sql;
			return command;
		}

		private List<T> ExecuteSearchCommand<T>(IDbCommand command) where T : Entity, new()
		{
			List<T> result = new List<T>();
			using (command)
			{
				var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T));
				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						T obj = new T();
						for (int i = 0; i < reader.FieldCount; i++)
						{
							string name = reader.GetName(i);
							ColumnInfo column = dataInfo[name];
							if (column != null)
							{
								column.SetValue(obj, reader.GetValue(i));
							}
						}
						obj.Loaded = true;
						result.Add(obj);
					}
				}
			}
			return result;
		}

		#endregion

		#region Protected Methods

		private string GetKeyCondition(Object obj, ColumnCollectionInfo dataInfo, IDbCommand command)
		{
			int index = 0;
			List<string> conditions = new List<string>();
			foreach (ColumnInfo primaryKey in dataInfo.PrimaryKeys.Values)
			{
				string parameterName = string.Format(KeySqlValueParameter, index++);
				string keyName = LeftSquare + dataInfo.Name + RightSquare + Dot + primaryKey.Name;
				conditions.Add(string.Format(ConditionSql, keyName, parameterName));
				AddParameter(primaryKey, parameterName, primaryKey.GetValue(obj), command);
			}

			return string.Join(And, conditions.ToArray());
		}

		private void AddParameter(ColumnInfo columnInfo, string parameterName, Object parameterValue, IDbCommand command)
		{
			IDataParameter parameter = command.CreateParameter();
			parameter.ParameterName = parameterName;
			SetParameter(parameter, columnInfo, parameterValue);
			command.Parameters.Add(parameter);
		}

		private void SetParameter(IDataParameter parameter, ColumnInfo column, Object value)
		{
			parameter.DbType = column.Type;
			parameter.Value = value;
			parameter.Direction = ParameterDirection.Input;
		}

		#endregion

		#region Const Strings

		public abstract string Join { get; }
		public abstract string On { get; }
		public abstract string Equal { get; }
		public abstract string Dot { get; }
		public abstract string RowNumSql { get; }
		public abstract string CountSql
		{
			get;
		}

        public abstract string MaxSql
        {
            get;
        }

		public abstract string FindSql
		{
			get;
		}

		public abstract string FindAttributeSql
		{
			get;
		}

		public abstract string WhereSql
		{
			get;
		}

		public abstract string OrderSql
		{
			get;
		}

		public abstract string ConditionSql
		{
			get;
		}

		public abstract string UpdateSql
		{
			get;
		}

		public abstract string DeleteSql
		{
			get;
		}

		public abstract string InsertSql
		{
			get;
		}

		public abstract string SetSql
		{
			get;
		}

		public abstract string Comma
		{
			get;
		}

		public abstract string SemiColon
		{
			get;
		}

		public abstract string And { get; }

		public abstract string UpdateSqlValueParameterName
		{
			get;
		}

		public abstract string UpdateSqlValueParameter
		{
			get;
		}

		public abstract string KeySqlValueParameterName
		{
			get;
		}

		public abstract string KeySqlValueParameter
		{
			get;
		}
		public abstract string LeftSquare { get; }
		public abstract string RightSquare { get; }
		public abstract string BetweenSql { get; }

		#endregion

        #region Stroe Procedure

        public int ExecuteSPNonQuery(string spName, List<SPParameter> args)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                return GetStoreProcedureCommand(conn, spName, args).ExecuteNonQuery();
            }
        }

        public object ExecuteSPScalar(string spName, List<SPParameter> args)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                return GetStoreProcedureCommand(conn, spName, args).ExecuteScalar();
            }
        }

        public int ExecuteSPReturn(string spName, List<SPParameter> args)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                var cmd = GetStoreProcedureCommand(conn, spName, args);
                var returnParameter = cmd.CreateParameter();
                returnParameter.DbType = DbType.Int32;
                returnParameter.Direction = ParameterDirection.ReturnValue;
                returnParameter.ParameterName = "ReturnVal";
                cmd.Parameters.Add(returnParameter);
                cmd.ExecuteNonQuery();
                return (int)returnParameter.Value;
            }
        }

        

        public List<T> ExecuteSPReader<T>(string spName, List<SPParameter> args) where T : Entity, new()
        {
            List<T> result = new List<T>();
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = GetStoreProcedureCommand(conn, spName, args);
                var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T));
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T obj = new T();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string name = reader.GetName(i);
                            ColumnInfo column = dataInfo[name];
                            if (column != null)
                            {
                                column.SetValue(obj, reader.GetValue(i));
                            }
                        }
                        obj.Loaded = true;
                        result.Add(obj);
                    }
                }
            }
            return result;
        }

        public IDbCommand GetStoreProcedureCommand(IDbConnection conn,string spName, List<SPParameter> args)
        {
            IDbCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = spName;
            foreach (var arg in args)
            {
                var p = command.CreateParameter();
                p.DbType = arg.Type;
                p.ParameterName = arg.Name;
                p.Direction = arg.Direction;
                p.Value = arg.Value;
                command.Parameters.Add(p);
            }
            return command;
        }

        #endregion

        #region Direct Command Text

        public List<string> ExecuteCommandReader(string cmdText)
        {
            List<string> result = new List<string>();
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = conn.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = CommandType.Text;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.FieldCount > 0)
                        {
                            object r = reader.GetValue(0);
                            if (r != System.DBNull.Value)
                            {
                                result.Add((string)r);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public int ExecuteCommand(string cmdText)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = conn.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = CommandType.Text;
                return command.ExecuteNonQuery();
            }
        }

        public object ExecuteCommandScalar(string cmdText)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = conn.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = CommandType.Text;
                return command.ExecuteScalar();
            }
        }

        #endregion

        public abstract IDbConnection NewConnection();

		public abstract void Initialize(bool reset);

		public abstract void CreateDatabase();

		public abstract void DropDatabaseIfExist();

        
    }
}
