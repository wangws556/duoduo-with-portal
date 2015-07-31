




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
	public partial class PersonalInfoConfigurationViewModel
	{
	

		/// <summary>
		/// Field which backs the NickName property
		/// </summary>
		private HistoryableProperty<string> nickName = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the NickName value
		/// </summary>
		[Browsable(false)]
		public  string NickName
		{
			get { return nickName.GetValue(); }
			set { ChangeAndNotifyHistory<string>(nickName, value, () => NickName ); }
		}


		/// <summary>
		/// Field which backs the AccountId property
		/// </summary>
		private HistoryableProperty<int> accountId = new HistoryableProperty<int>(-1);

		/// <summary>
		/// Gets / sets the AccountId value
		/// </summary>
		[Browsable(false)]
		public  int AccountId
		{
			get { return accountId.GetValue(); }
			set { ChangeAndNotifyHistory<int>(accountId, value, () => AccountId ); }
		}


		/// <summary>
		/// Field which backs the Gender property
		/// </summary>
		private HistoryableProperty<bool> gender = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the Gender value
		/// </summary>
		[Browsable(false)]
		public  bool Gender
		{
			get { return gender.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(gender, value, () => Gender ); }
		}


		/// <summary>
		/// Field which backs the IsFemale property
		/// </summary>
		private HistoryableProperty<bool> isFemale = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the IsFemale value
		/// </summary>
		[Browsable(false)]
		public  bool IsFemale
		{
			get { return isFemale.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(isFemale, value, () => IsFemale ); }
		}


		/// <summary>
		/// Field which backs the Email property
		/// </summary>
		private HistoryableProperty<string> email = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Email value
		/// </summary>
		[Browsable(false)]
		public  string Email
		{
			get { return email.GetValue(); }
			set { ChangeAndNotifyHistory<string>(email, value, () => Email ); }
		}


		/// <summary>
		/// Field which backs the PasswordQuestion property
		/// </summary>
		private HistoryableProperty<string> passwordQuestion = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the PasswordQuestion value
		/// </summary>
		[Browsable(false)]
		public  string PasswordQuestion
		{
			get { return passwordQuestion.GetValue(); }
			set { ChangeAndNotifyHistory<string>(passwordQuestion, value, () => PasswordQuestion ); }
		}


		/// <summary>
		/// Field which backs the PasswordAnswer property
		/// </summary>
		private HistoryableProperty<string> passwordAnswer = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the PasswordAnswer value
		/// </summary>
		[Browsable(false)]
		public  string PasswordAnswer
		{
			get { return passwordAnswer.GetValue(); }
			set { ChangeAndNotifyHistory<string>(passwordAnswer, value, () => PasswordAnswer ); }
		}


		/// <summary>
		/// Field which backs the Age property
		/// </summary>
		private HistoryableProperty<int> age = new HistoryableProperty<int>(-1);

		/// <summary>
		/// Gets / sets the Age value
		/// </summary>
		[Browsable(false)]
		public  int Age
		{
			get { return age.GetValue(); }
			set { ChangeAndNotifyHistory<int>(age, value, () => Age ); }
		}


		/// <summary>
		/// Field which backs the LastLoginTime property
		/// </summary>
		private HistoryableProperty<DateTime> lastLoginTime = new HistoryableProperty<DateTime>(DateTime.Now);

		/// <summary>
		/// Gets / sets the LastLoginTime value
		/// </summary>
		[Browsable(false)]
		public  DateTime LastLoginTime
		{
			get { return lastLoginTime.GetValue(); }
			set { ChangeAndNotifyHistory<DateTime>(lastLoginTime, value, () => LastLoginTime ); }
		}


		/// <summary>
		/// Field which backs the ImageSource property
		/// </summary>
		private HistoryableProperty<ImageSource> imageSource = new HistoryableProperty<ImageSource>(null);

		/// <summary>
		/// Gets / sets the ImageSource value
		/// </summary>
		[Browsable(false)]
		public  ImageSource ImageSource
		{
			get { return imageSource.GetValue(); }
			set { ChangeAndNotifyHistory<ImageSource>(imageSource, value, () => ImageSource ); }
		}
	}
}
	