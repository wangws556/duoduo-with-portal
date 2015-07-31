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
	///     <para>1.Message content</para>
	///     <para>2.Parameters along with the message</para>
	/// </summary>
	/// <typeparam name="T">type of the message content</typeparam>
	public class NotificationMessage<T>
	{
		public T Content { get; set; }

		public object[] Parameters { get; set; }

		public NotificationMessage()
			: this(default(T))
		{
		}

		public NotificationMessage(T content)
			: this(content, null)
		{
		}

		public NotificationMessage(T content, object[] parameters)
		{
			Content = content;
			Parameters = parameters;
		}
	}
}
