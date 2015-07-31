/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Common.Wpf
{
	/// <summary>
	/// This Window is to be used as a base window that is capable to receive messages from ViewModel
	/// </summary>
	/// <typeparam name="ActionType">the message type received from ViewModel, normally will be enum value</typeparam>
    public abstract class MessageSinkWindow<ActionType> : Window, IView, IDisposable where ActionType : struct
	{
		public MessageSinkWindow()
		{
            DataContextChanged += MessageSinkWindow_DataContextChanged;
			Messenger.Default.Register<EnumNotificationMessage<object, ActionType>>(this, MessageReceived);
		}

        void MessageSinkWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.OldValue != null)
            {
                ViewModelBase vm = e.OldValue as ViewModelBase;
                if(vm != null)
                {
                    vm.View = null;
                }
            }
            if (e.NewValue != null)
            {
                ViewModelBase vm = e.NewValue as ViewModelBase;
                if (vm != null)
                {
                    //vm.View = this;
                }
            }
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

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
            base.OnClosing(e);
        }

        public virtual void Dispose()
        {
            Messenger.Default.Unregister<EnumNotificationMessage<object, ActionType>>(this);

            Dispatcher.BeginInvoke((Action)(() =>
            {
                ViewModel.ViewModelBase vm = DataContext as ViewModel.ViewModelBase;
                if (vm != null)
                {
                    vm.Dispose();
                }
            }));
        }

        public virtual void CallJavaScript(string functionName, params object[] args)
        {
            
        }

        ~MessageSinkWindow()
        {
            Dispose();
        }
	}
}
