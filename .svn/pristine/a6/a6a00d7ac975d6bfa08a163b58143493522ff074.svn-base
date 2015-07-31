/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoYoStudio.Common.Notification
{
	/// <summary>
	///     <para>This class represent a message that is sent from ViewModel to the Windows and Controls, it can pass three kinds of information:</para>
	///     <para>1.The action of the message</para>
	///     <para>2.Message content</para>
	///     <para>3.Parameters along with the message</para>
	/// </summary>
	/// <typeparam name="T">type of the message content</typeparam>
	/// <typeparam name="ActionType">type of the action, normally will be enum value</typeparam>
	public class EnumNotificationMessage<T, ActionType> : NotificationMessage<T> where ActionType : struct
	{
		public ActionType Action { get; set; }


		public EnumNotificationMessage(ActionType action)
			: this(action, default(T))
		{
		}

		public EnumNotificationMessage(ActionType action, T content)
			: this(action, content, null)
		{
		}

		public EnumNotificationMessage(ActionType action, T content, object[] parameters)
			: base(content, parameters)
		{
			Action = action;
		}

	}
}
