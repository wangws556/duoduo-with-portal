/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. YoYoStudio CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using YoYoStudio.Persistent;

namespace YoYoStudio.Persistent
{
	public class SqlServerORMapper : ORMapper
	{
		#region Sql statments

		public override string ConditionSql
		{
			get { return "{0} = {1}"; }
		}
		public override string CountSql
		{
			get { return "SELECT COUNT(*) FROM {0}"; }
		}
        public override string MaxSql
        {
            get { return "SELECT MAX({0}) FROM {1}"; }
        }
		public override string DeleteSql
		{
			get { return "DELETE FROM [{0}]"; }
		}
		public override string FindAttributeSql
		{
			get { return "SELECT DISTINCT {0} FROM {1}"; }
		}
		public override string FindSql
		{
			get { return "SELECT {0} FROM {1}"; }
		}
		public override string InsertSql
		{
			get { return "INSERT INTO [{0}]({1}) VALUES( {2} )"; }
		}
		public override string OrderSql
		{
			get { return "{0} ORDER BY {1}"; }
		}
		public override string SetSql
		{
			get { return "[{0}] = {1}"; }
		}
		public override string UpdateSql
		{
			get { return "UPDATE [{0}] SET {1} "; }
		}
		public override string UpdateSqlValueParameterName
		{
			get { return "Value_{0}"; }
		}
		public override string UpdateSqlValueParameter
		{
			get { return "@" + UpdateSqlValueParameterName; }
		}
		public override string KeySqlValueParameterName
		{
			get { return "KeyValue_{0}"; }
		}
		public override string KeySqlValueParameter
		{
			get { return "@" + KeySqlValueParameterName; }
		}
		public override string WhereSql
		{
			get { return "{0} WHERE ( {1} )"; }
		}
		public override string And
		{
			get { return " AND "; }
		}
		public override string Comma
		{
			get { return " , "; }
		}
		public override string Join
		{
			get { return " JOIN "; }
		}

		public override string On
		{
			get { return " ON "; }
		}

		public override string Equal
		{
			get { return "="; }
		}

		public override string Dot
		{
			get { return "."; }
		}

		public override string SemiColon
		{
			get { return ";"; }
		}
		public override string LeftSquare
		{
			get { return "["; }
		}

		public override string RightSquare
		{
			get { return "]"; }
		}

		public override string RowNumSql
		{
			get { return "WITH ROWNUM AS (SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS RN, {1} FROM {2} {3}) SELECT {1} FROM ROWNUM"; }
		}

		public override string BetweenSql
		{
			get { return " ({0} BETWEEN {1} AND {2})"; }
		}

		#endregion

		private const string ScriptFile = "CreateScript.sql";
		private const string MasterDB = "master";

		private string connectionString;

		public SqlServerORMapper(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public override IDbConnection NewConnection()
		{
			return new SqlConnection(connectionString);
		}

		public override void Initialize(bool reset)
		{
			if (reset)
			{
				DropDatabaseIfExist();
			}

			if (!IsDatabaseExisted())
			{
				CreateDatabase();
			}
		}

		public override void CreateDatabase()
		{
			CreateDatabase(GetDatabaseName());

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				FileInfo file = new FileInfo(ScriptFile);
				string script = file.OpenText().ReadToEnd();
				script = script.Replace("\r\nGO\r\n", "\r\n");
				using (var command = new SqlCommand(script, connection as SqlConnection))
				{
					command.ExecuteNonQuery();
				}
			}
		}

		public override void DropDatabaseIfExist()
		{
			if (!IsDatabaseExisted())
			{
				return;
			}

			using (var connection = ConnectToMaster())
			{
				connection.Open();

				{
					using (var command = new SqlCommand("DROP DATABASE " + GetDatabaseName(), connection))
					{
						command.ExecuteNonQuery();
					}
				}
			}
		}

		private void CreateDatabase(string databaseName)
		{
			using (var connection = ConnectToMaster())
			{
				connection.Open();

				string cmdStringFormat = @"create database {0}";
				using (var command = new SqlCommand(string.Format(cmdStringFormat, databaseName), connection))
				{
					command.ExecuteNonQuery();
				}
			}
		}

		private bool IsDatabaseExisted()
		{
			using (var connection = ConnectToMaster())
			{
				connection.Open();

				string databaseName = GetDatabaseName();
				string cmdStringFormat = @"select name from sysdatabases where name = '{0}'";
				using (var command = new SqlCommand(string.Format(cmdStringFormat, databaseName), connection))
				{
					string name = command.ExecuteScalar() as string;
					return name == databaseName;
				}
			}
		}

		private SqlConnection ConnectToMaster()
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
			builder.InitialCatalog = MasterDB;
			return new SqlConnection(builder.ToString());
		}

		private string GetDatabaseName()
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
			return builder.InitialCatalog;
		}
	}
}
