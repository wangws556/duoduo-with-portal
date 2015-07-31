




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
	public partial class RegisterWindowViewModel
	{
	

		/// <summary>
		/// Field which backs the ErrorMessage property
		/// </summary>
		private HistoryableProperty<string> errorMessage = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the ErrorMessage value
		/// </summary>
		[Browsable(false)]
		public  string ErrorMessage
		{
			get { return errorMessage.GetValue(); }
			set { ChangeAndNotifyHistory<string>(errorMessage, value, () => ErrorMessage ); }
		}


		/// <summary>
		/// Field which backs the AccountDescription property
		/// </summary>
		private HistoryableProperty<string> accountDescription = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the AccountDescription value
		/// </summary>
		[Browsable(false)]
		public  string AccountDescription
		{
			get { return accountDescription.GetValue(); }
			set { ChangeAndNotifyHistory<string>(accountDescription, value, () => AccountDescription ); }
		}


		/// <summary>
		/// Field which backs the PasswordDescription property
		/// </summary>
		private HistoryableProperty<string> passwordDescription = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the PasswordDescription value
		/// </summary>
		[Browsable(false)]
		public  string PasswordDescription
		{
			get { return passwordDescription.GetValue(); }
			set { ChangeAndNotifyHistory<string>(passwordDescription, value, () => PasswordDescription ); }
		}


		/// <summary>
		/// Field which backs the CompleteReigster property
		/// </summary>
		private HistoryableProperty<string> completeReigster = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the CompleteReigster value
		/// </summary>
		[Browsable(false)]
		public  string CompleteReigster
		{
			get { return completeReigster.GetValue(); }
			set { ChangeAndNotifyHistory<string>(completeReigster, value, () => CompleteReigster ); }
		}


		/// <summary>
		/// Field which backs the RegisterAgreement property
		/// </summary>
		private HistoryableProperty<string> registerAgreement = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the RegisterAgreement value
		/// </summary>
		[Browsable(false)]
		public  string RegisterAgreement
		{
			get { return registerAgreement.GetValue(); }
			set { ChangeAndNotifyHistory<string>(registerAgreement, value, () => RegisterAgreement ); }
		}


		/// <summary>
		/// Field which backs the View property
		/// </summary>
		private HistoryableProperty<string> view = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the View value
		/// </summary>
		[Browsable(false)]
		public  string View
		{
			get { return view.GetValue(); }
			set { ChangeAndNotifyHistory<string>(view, value, () => View ); }
		}


		/// <summary>
		/// Field which backs the Account property
		/// </summary>
		private HistoryableProperty<string> account = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Account value
		/// </summary>
		[Browsable(false)]
		public  string Account
		{
			get { return account.GetValue(); }
			set { ChangeAndNotifyHistory<string>(account, value, () => Account ); }
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
		/// Field which backs the UserId property
		/// </summary>
		private HistoryableProperty<int> userId = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the UserId value
		/// </summary>
		[Browsable(false)]
		public  int UserId
		{
			get { return userId.GetValue(); }
			set { ChangeAndNotifyHistory<int>(userId, value, () => UserId ); }
		}


		/// <summary>
		/// Field which backs the Sex property
		/// </summary>
		private HistoryableProperty<int> sex = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the Sex value
		/// </summary>
		[Browsable(false)]
		public  int Sex
		{
			get { return sex.GetValue(); }
			set { ChangeAndNotifyHistory<int>(sex, value, () => Sex ); }
		}


		/// <summary>
		/// Field which backs the AccountIdDescription property
		/// </summary>
		private HistoryableProperty<string> accountIdDescription = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the AccountIdDescription value
		/// </summary>
		[Browsable(false)]
		public  string AccountIdDescription
		{
			get { return accountIdDescription.GetValue(); }
			set { ChangeAndNotifyHistory<string>(accountIdDescription, value, () => AccountIdDescription ); }
		}
	}
}
	