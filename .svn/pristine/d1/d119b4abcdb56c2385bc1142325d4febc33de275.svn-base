




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
	public partial class ProfileViewModel
	{
	

		/// <summary>
		/// Field which backs the LoginEntryVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<LoginEntryViewModel>> loginEntryVMs = new HistoryableProperty<ObservableCollection<LoginEntryViewModel>>(null);

		/// <summary>
		/// Gets / sets the LoginEntryVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<LoginEntryViewModel> LoginEntryVMs
		{
			get { return loginEntryVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<LoginEntryViewModel>>(loginEntryVMs, value, () => LoginEntryVMs ); }
		}


		/// <summary>
		/// Field which backs the LastLoginVM property
		/// </summary>
		private HistoryableProperty<LoginEntryViewModel> lastLoginVM = new HistoryableProperty<LoginEntryViewModel>(null);

		/// <summary>
		/// Gets / sets the LastLoginVM value
		/// </summary>
		[Browsable(false)]
		public  LoginEntryViewModel LastLoginVM
		{
			get { return lastLoginVM.GetValue(); }
			set { ChangeAndNotifyHistory<LoginEntryViewModel>(lastLoginVM, value, () => LastLoginVM ); }
		}


		/// <summary>
		/// Field which backs the VideoConfigurationVM property
		/// </summary>
		private HistoryableProperty<VideoConfigurationViewModel> videoConfigurationVM = new HistoryableProperty<VideoConfigurationViewModel>(null);

		/// <summary>
		/// Gets / sets the VideoConfigurationVM value
		/// </summary>
		[Browsable(false)]
		public  VideoConfigurationViewModel VideoConfigurationVM
		{
			get { return videoConfigurationVM.GetValue(); }
			set { ChangeAndNotifyHistory<VideoConfigurationViewModel>(videoConfigurationVM, value, () => VideoConfigurationVM ); }
		}


		/// <summary>
		/// Field which backs the AudioConfigurationVM property
		/// </summary>
		private HistoryableProperty<AudioConfigurationViewModel> audioConfigurationVM = new HistoryableProperty<AudioConfigurationViewModel>(null);

		/// <summary>
		/// Gets / sets the AudioConfigurationVM value
		/// </summary>
		[Browsable(false)]
		public  AudioConfigurationViewModel AudioConfigurationVM
		{
			get { return audioConfigurationVM.GetValue(); }
			set { ChangeAndNotifyHistory<AudioConfigurationViewModel>(audioConfigurationVM, value, () => AudioConfigurationVM ); }
		}


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
	