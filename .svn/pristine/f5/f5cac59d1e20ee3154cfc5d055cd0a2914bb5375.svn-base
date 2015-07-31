/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace YoYoStudio.Common
{
	/// <summary>
	/// This is the utility class for singleton parten, you need to have private constructors to prevent creating new instances of the class before using this utility.
	/// </summary>
	/// <typeparam name="T">the type which you want to use singleton</typeparam>
	public class Singleton<T> where T : class
	{
		static object SyncRoot = new object();
		static T instance;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					lock (SyncRoot)
					{
						if (instance == null)
						{
							ConstructorInfo constructorInfo = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
							if (constructorInfo == null)
							{
								throw new InvalidOperationException("Constructor must be private");
							}
							instance = constructorInfo.Invoke(null) as T;
						}
					}
				}
				return instance;
			}
		}
	}
}
