




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
	public partial class SecurityConfigurationViewModel
	{
	

		/// <summary>
		/// Field which backs the NewPassword property
		/// </summary>
		private HistoryableProperty<string> newPassword = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the NewPassword value
		/// </summary>
		[Browsable(false)]
		public  string NewPassword
		{
			get { return newPassword.GetValue(); }
			set { ChangeAndNotifyHistory<string>(newPassword, value, () => NewPassword ); }
		}


		/// <summary>
		/// Field which backs the OldPassword property
		/// </summary>
		private HistoryableProperty<string> oldPassword = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the OldPassword value
		/// </summary>
		[Browsable(false)]
		public  string OldPassword
		{
			get { return oldPassword.GetValue(); }
			set { ChangeAndNotifyHistory<string>(oldPassword, value, () => OldPassword ); }
		}


		/// <summary>
		/// Field which backs the ConfirmPassword property
		/// </summary>
		private HistoryableProperty<string> confirmPassword = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the ConfirmPassword value
		/// </summary>
		[Browsable(false)]
		public  string ConfirmPassword
		{
			get { return confirmPassword.GetValue(); }
			set { ChangeAndNotifyHistory<string>(confirmPassword, value, () => ConfirmPassword ); }
		}


		/// <summary>
		/// Field which backs the AutoLogin property
		/// </summary>
		private HistoryableProperty<bool> autoLogin = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the AutoLogin value
		/// </summary>
		[Browsable(false)]
		public  bool AutoLogin
		{
			get { return autoLogin.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(autoLogin, value, () => AutoLogin ); }
		}
	}
}
	