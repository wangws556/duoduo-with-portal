/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. YoYoStudio CONFIDENTIAL
/// </copyright>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using YoYoStudio.Common;

namespace YoYoStudio.Common.ORM
{
	public enum EntityAccesserType
	{
		SqlServer,
		Oracle,
		MySql,
		Xml,
	}

	public interface IEntityAccesser
	{
		void Load<T>(T obj) where T : Entity, new();
		void Insert<T>(T obj) where T : Entity, new();
		void Update<T>(T obj) where T : Entity, new();
		void Delete<T>(T obj) where T : Entity, new();
		List<T> Search<T>(string condition = "", string order = "", int start=-1, int count =-1) where T : Entity, new();
		List<TEntity> Search<TOwner, TEntity>(TOwner owner, Expression<Func<TOwner, IEnumerable>> exp, string order = "") where TEntity : Entity, new();
		void Initialize(bool reset);
		int GetCount<T>(string condition = "") where T : Entity, new();
        int GetMax<T>() where T : Entity, new();
        int ExecuteSPNonQuery(string spName, List<SPParameter> args);
        object ExecuteSPScalar(string spName, List<SPParameter> args);
        int ExecuteSPReturn(string spName, List<SPParameter> args);
        List<T> ExecuteSPReader<T>(string spName, List<SPParameter> args) where T : Entity, new();
        List<string> ExecuteCommandReader(string cmdText);
        int ExecuteCommand(string cmdText);
        object ExecuteCommandScalar(string cmdText);
	}
}
