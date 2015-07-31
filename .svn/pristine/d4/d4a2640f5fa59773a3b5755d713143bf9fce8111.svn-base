




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
using YoYoStudio.Model.Media;

namespace YoYoStudio.Client.ViewModel
{
	public partial class HallWindowViewModel
	{
	

		/// <summary>
		/// Field which backs the OnlinieUserCount property
		/// </summary>
		private HistoryableProperty<int> onlinieUserCount = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the OnlinieUserCount value
		/// </summary>
		[Browsable(false)]
		public  int OnlinieUserCount
		{
			get { return onlinieUserCount.GetValue(); }
			set { ChangeAndNotifyHistory<int>(onlinieUserCount, value, () => OnlinieUserCount ); }
		}


		/// <summary>
		/// Field which backs the UserName property
		/// </summary>
		private HistoryableProperty<string> userName = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the UserName value
		/// </summary>
		[Browsable(false)]
		public  string UserName
		{
			get { return userName.GetValue(); }
			set { ChangeAndNotifyHistory<string>(userName, value, () => UserName ); }
		}


		/// <summary>
		/// Field which backs the CanCancel property
		/// </summary>
		private HistoryableProperty<bool> canCancel = new HistoryableProperty<bool>(true);

		/// <summary>
		/// Gets / sets the CanCancel value
		/// </summary>
		[Browsable(false)]
		public  bool CanCancel
		{
			get { return canCancel.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(canCancel, value, () => CanCancel ); }
		}


		/// <summary>
		/// Field which backs the Password property
		/// </summary>
		private HistoryableProperty<string> password = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Password value
		/// </summary>
		[Browsable(false)]
		public  string Password
		{
			get { return password.GetValue(); }
			set { ChangeAndNotifyHistory<string>(password, value, () => Password ); }
		}
	}
}
	