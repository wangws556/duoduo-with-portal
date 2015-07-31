/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. YoYoStudio CONFIDENTIAL
/// </copyright>
using System;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Persistent
{
	/// <summary>
	/// The factory class that load the model accesser dlls
	/// </summary>
	public class EntityAccesserFactory
	{
		private EntityAccesserFactory()
		{
		}

		public IORMapper GetEntityAccesser(EntityAccesserType accesserType, string connectionString)
		{
			switch (accesserType)
			{
				case EntityAccesserType.SqlServer:
					return new SqlServerORMapper(connectionString);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
