/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using YoYoStudio.Common.ObjectHistory;

namespace YoYoStudio.Common.Extensions
{
	/// <summary>
	/// This is class is used to declare the extension methods for PropertyChangedEventHandler class
	/// </summary>
	public static class PropertyChangedEventHandlerExtension
	{
		public const string OwnerParameterName = "owner";

		/// <summary>
		///     <para>
		///         The normal way to implement INotifyPropertyChanged interface is to raise the PropertyChanged event
		///         and pass in the property name string to notify the change, this is not very robust, the drawbacks are:
		///     </para>
		///     <para>1.You cannot use Auto-Properties</para>
		///     <para>2.The property name is passed as string, which might introduce errors if you rename the property but forget to change to notify</para>
		///     <para>3.Every property nees extra code</para>
		///     <para>This extension method provide a more elegant way to implement the INotifyPropertyChanged interface which will not need to pass string, example:</para>
		/// <example>
		///     <para>public class Sample : INotifyPropertyChanged</para>
		///     <para>{</para>
		///     <para>private string name;</para>
		///     <para>public string Name{</para>
		///     <para>get { return name; }</para>
		///     <para>set { PropertyChanged.ChangeAndNotify(this,ref name, value, () => Name); }}</para>
		///     <para>}</para>
		/// </example>     
		/// </summary>
		/// <typeparam name="T">The type of the property being changed</typeparam>
		/// <param name="handler">The event that is going to notify the change of the property</param>
		/// <param name="owner">The owner of the property</param>
		/// <param name="field">The field that is being changed</param>
		/// <param name="value">The new value of the property</param>
		/// <param name="memberExpression">The lambda expression of the Property</param>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="ArgumentException" />

		public static void ChangeAndNotify<T>(this PropertyChangedEventHandler handler, object owner, ref T field, T value, Expression<Func<T>> memberExpression)
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
			{
				return;
			}

			if (owner == null)
			{
				throw new ArgumentNullException(OwnerParameterName);
			}

			field = value;

			Notify<T>(handler, owner, memberExpression);
		}

		public static void ChangeAndNotifyHistory<T>(this PropertyChangedEventHandler handler, object owner, HistoryableProperty<T> field, T value, Expression<Func<T>> memberExpression)
		{
			if (owner == null)
			{
				throw new ArgumentNullException(OwnerParameterName);
			}

			if (EqualityComparer<T>.Default.Equals(field.GetValue(), value))
			{
				return;
			}
			field.SetValue(value, () => Notify<T>(handler, owner, memberExpression));

			Notify<T>(handler, owner, memberExpression);
		}

		public static void Notify<T>(this PropertyChangedEventHandler handler, object owner,Expression<Func<T>> memberExpression)
		{
            if (owner == null)
            {
                throw new ArgumentNullException(OwnerParameterName);
            }
            if (handler != null)
            {
                string propertyName = ExpressionHelper.GetPropertyName<T>(memberExpression);

                //send the property change event
                if (!string.IsNullOrEmpty(propertyName))
                {
                    try
                    {
                        handler(owner, new PropertyChangedEventArgs(propertyName));
                    }
                    catch { }
                }
            }
		}
	}
}