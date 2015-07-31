




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
	public partial class DatabaseViewModel
	{
	

		/// <summary>
		/// Field which backs the PersonalInfoConfigurationVM property
		/// </summary>
		private HistoryableProperty<PersonalInfoConfigurationViewModel> personalInfoConfigurationVM = new HistoryableProperty<PersonalInfoConfigurationViewModel>(null);

		/// <summary>
		/// Gets / sets the PersonalInfoConfigurationVM value
		/// </summary>
		[Browsable(false)]
		public  PersonalInfoConfigurationViewModel PersonalInfoConfigurationVM
		{
			get { return personalInfoConfigurationVM.GetValue(); }
			set { ChangeAndNotifyHistory<PersonalInfoConfigurationViewModel>(personalInfoConfigurationVM, value, () => PersonalInfoConfigurationVM ); }
		}


		/// <summary>
		/// Field which backs the PhotoSelectorVM property
		/// </summary>
		private HistoryableProperty<PhotoSelectorViewModel> photoSelectorVM = new HistoryableProperty<PhotoSelectorViewModel>(null);

		/// <summary>
		/// Gets / sets the PhotoSelectorVM value
		/// </summary>
		[Browsable(false)]
		public  PhotoSelectorViewModel PhotoSelectorVM
		{
			get { return photoSelectorVM.GetValue(); }
			set { ChangeAndNotifyHistory<PhotoSelectorViewModel>(photoSelectorVM, value, () => PhotoSelectorVM ); }
		}


		/// <summary>
		/// Field which backs the SecurityConfigurationVM property
		/// </summary>
		private HistoryableProperty<SecurityConfigurationViewModel> securityConfigurationVM = new HistoryableProperty<SecurityConfigurationViewModel>(null);

		/// <summary>
		/// Gets / sets the SecurityConfigurationVM value
		/// </summary>
		[Browsable(false)]
		public  SecurityConfigurationViewModel SecurityConfigurationVM
		{
			get { return securityConfigurationVM.GetValue(); }
			set { ChangeAndNotifyHistory<SecurityConfigurationViewModel>(securityConfigurationVM, value, () => SecurityConfigurationVM ); }
		}
	}
}
	