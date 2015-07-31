/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.Resource;

namespace YoYoStudio.Common
{
	public class ExpressionHelper
	{
		public const string MemberExpressionParameterName = "memberExpression";

		public static string GetPropertyName<T>(Expression<Func<T>> memberExpression)
		{
			string propertyName = string.Empty;
			//Get the name of the property
			if (memberExpression == null)
			{
				throw new ArgumentNullException(MemberExpressionParameterName);
			}
			var experssionBody = memberExpression.Body as MemberExpression;
			if (experssionBody == null)
			{
				throw new ArgumentException("Expression Must Return");
			}
			propertyName = experssionBody.Member.Name;
			return propertyName;
		}
	}
}
