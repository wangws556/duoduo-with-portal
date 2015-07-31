




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
	public partial class LoginEntryViewModel
	{
	

		/// <summary>
		/// Field which backs the UserId property
		/// </summary>
		private HistoryableProperty<string> userId = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the UserId value
		/// </summary>
		[Browsable(false)]
		public  string UserId
		{
			get { return userId.GetValue(); }
			set { ChangeAndNotifyHistory<string>(userId, value, () => UserId ); }
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


		/// <summary>
		/// Field which backs the Remember property
		/// </summary>
		private HistoryableProperty<bool> remember = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the Remember value
		/// </summary>
		[Browsable(false)]
		public  bool Remember
		{
			get { return remember.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(remember, value, () => Remember ); }
		}
	}
}
	