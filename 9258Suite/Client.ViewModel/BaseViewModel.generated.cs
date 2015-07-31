




/// <copyright>
/// Copyright ©  2012 Unisys Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using YoYoStudio.Common.ObjectHistory;
using YoYoStudio.Common.Extensions;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Chat;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace YoYoStudio.Client.ViewModel
{
	public partial class WindowViewModel
	{
	

		/// <summary>
		/// Field which backs the Busy property
		/// </summary>
		protected HistoryableProperty<bool> busy = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the Busy value
		/// </summary>
		[Browsable(false)]
		public  bool Busy
		{
			get { return busy.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(busy, value, () => Busy ); }
		}


		/// <summary>
		/// Field which backs the WebPageVM property
		/// </summary>
		private HistoryableProperty<WebPageViewModel> webPageVM = new HistoryableProperty<WebPageViewModel>(new WebPageViewModel());

		/// <summary>
		/// Gets / sets the WebPageVM value
		/// </summary>
		[Browsable(false)]
		public  WebPageViewModel WebPageVM
		{
			get { return webPageVM.GetValue(); }
			set { ChangeAndNotifyHistory<WebPageViewModel>(webPageVM, value, () => WebPageVM ); }
		}


		/// <summary>
		/// Field which backs the WelcomeMessage property
		/// </summary>
		protected HistoryableProperty<string> welcomeMessage = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the WelcomeMessage value
		/// </summary>
		[Browsable(false)]
		public  string WelcomeMessage
		{
			get { return welcomeMessage.GetValue(); }
			set { ChangeAndNotifyHistory<string>(welcomeMessage, value, () => WelcomeMessage ); }
		}


		/// <summary>
		/// Field which backs the BusyMessage property
		/// </summary>
		protected HistoryableProperty<string> busyMessage = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the BusyMessage value
		/// </summary>
		[Browsable(false)]
		public  string BusyMessage
		{
			get { return busyMessage.GetValue(); }
			set { ChangeAndNotifyHistory<string>(busyMessage, value, () => BusyMessage ); }
		}
	}
}
	