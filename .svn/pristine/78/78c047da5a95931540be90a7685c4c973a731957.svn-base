/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YoYoStudio.Common.Notification;

namespace YoYoStudio.Common.Wpf
{
	/// <summary>
	/// This is to be used as a base UserControl that is capable to receive messages from ViewModel
	/// </summary>
	/// <typeparam name="ActionType">the message type received from ViewModel, normally will be enum value</typeparam>
	public abstract class MessageSinkUserControl<ActionType> : UserControl, IDisposable where ActionType : struct
	{
		protected bool disposed = false;

		public MessageSinkUserControl()
		{
			Messenger.Default.Register<EnumNotificationMessage<object, ActionType>>(this, MessageReceived);
		}

		private void MessageReceived(EnumNotificationMessage<object, ActionType> message)
		{
			Dispatcher.BeginInvoke((Action)(() =>
			{
				ProcessMessage(message);
			}));
		}
		/// <summary>
		/// Implement this method to prcess the message received from ViewModel
		/// </summary>
		/// <param name="message">The message received from ViewModel</param>
		protected abstract void ProcessMessage(EnumNotificationMessage<object, ActionType> message);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			if (disposing)
			{
				Messenger.Default.Unregister<EnumNotificationMessage<object, ActionType>>(this);
			}
			disposed = true;
		}

		~MessageSinkUserControl()
		{
			Dispose(false);
		}
	}
}
