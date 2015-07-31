




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
	public partial class UserViewModel
	{
	

		/// <summary>
		/// Field which backs the Hide property
		/// </summary>
		private HistoryableProperty<bool> hide = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the Hide value
		/// </summary>
		[Browsable(false)]
		public  bool Hide
		{
			get { return hide.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(hide, value, () => Hide ); }
		}


		/// <summary>
		/// Field which backs the MicIndex property
		/// </summary>
		private HistoryableProperty<int> micIndex = new HistoryableProperty<int>(-1);

		/// <summary>
		/// Gets / sets the MicIndex value
		/// </summary>
		[Browsable(false)]
		public  int MicIndex
		{
			get { return micIndex.GetValue(); }
			set { ChangeAndNotifyHistory<int>(micIndex, value, () => MicIndex ); }
		}


		/// <summary>
		/// Field which backs the MicAction property
		/// </summary>
		private HistoryableProperty<MicAction> micAction = new HistoryableProperty<MicAction>(MicAction.None);

		/// <summary>
		/// Gets / sets the MicAction value
		/// </summary>
		[Browsable(false)]
		public  MicAction MicAction
		{
			get { return micAction.GetValue(); }
			set { ChangeAndNotifyHistory<MicAction>(micAction, value, () => MicAction ); }
		}


		/// <summary>
		/// Field which backs the MicStatus property
		/// </summary>
		private HistoryableProperty<int> micStatus = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the MicStatus value
		/// </summary>
		[Browsable(false)]
		public  int MicStatus
		{
			get { return micStatus.GetValue(); }
			set { ChangeAndNotifyHistory<int>(micStatus, value, () => MicStatus ); }
		}


		/// <summary>
		/// Field which backs the MusicStatus property
		/// </summary>
		private HistoryableProperty<int> musicStatus = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the MusicStatus value
		/// </summary>
		[Browsable(false)]
		public  int MusicStatus
		{
			get { return musicStatus.GetValue(); }
			set { ChangeAndNotifyHistory<int>(musicStatus, value, () => MusicStatus ); }
		}


		/// <summary>
		/// Field which backs the MicType property
		/// </summary>
		private HistoryableProperty<MicType> micType = new HistoryableProperty<MicType>(MicType.None);

		/// <summary>
		/// Gets / sets the MicType value
		/// </summary>
		[Browsable(false)]
		public  MicType MicType
		{
			get { return micType.GetValue(); }
			set { ChangeAndNotifyHistory<MicType>(micType, value, () => MicType ); }
		}


		/// <summary>
		/// Field which backs the StreamGuid property
		/// </summary>
		private HistoryableProperty<string> streamGuid = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the StreamGuid value
		/// </summary>
		[Browsable(false)]
		public  string StreamGuid
		{
			get { return streamGuid.GetValue(); }
			set { ChangeAndNotifyHistory<string>(streamGuid, value, () => StreamGuid ); }
		}


		/// <summary>
		/// Field which backs the Name property
		/// </summary>
		private HistoryableProperty<string> name = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Name value
		/// </summary>
		[Browsable(false)]
		public  string Name
		{
			get { return name.GetValue(); }
			set { ChangeAndNotifyHistory<string>(name, value, () => Name ); }
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
		/// Field which backs the Money property
		/// </summary>
		private HistoryableProperty<int> money = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the Money value
		/// </summary>
		[Browsable(false)]
		public  int Money
		{
			get { return money.GetValue(); }
			set { ChangeAndNotifyHistory<int>(money, value, () => Money ); }
		}


		/// <summary>
		/// Field which backs the Score property
		/// </summary>
		private HistoryableProperty<int> score = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the Score value
		/// </summary>
		[Browsable(false)]
		public  int Score
		{
			get { return score.GetValue(); }
			set { ChangeAndNotifyHistory<int>(score, value, () => Score ); }
		}


		/// <summary>
		/// Field which backs the Age property
		/// </summary>
		private HistoryableProperty<int> age = new HistoryableProperty<int>(0);

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
		/// Field which backs the RoleVM property
		/// </summary>
		private HistoryableProperty<RoleViewModel> roleVM = new HistoryableProperty<RoleViewModel>(null);

		/// <summary>
		/// Gets / sets the RoleVM value
		/// </summary>
		[Browsable(false)]
		public  RoleViewModel RoleVM
		{
			get { return roleVM.GetValue(); }
			set { ChangeAndNotifyHistory<RoleViewModel>(roleVM, value, () => RoleVM ); }
		}


		/// <summary>
		/// Field which backs the RoomWindowVM property
		/// </summary>
		private HistoryableProperty<RoomWindowViewModel> roomWindowVM = new HistoryableProperty<RoomWindowViewModel>(null);

		/// <summary>
		/// Gets / sets the RoomWindowVM value
		/// </summary>
		[Browsable(false)]
		public  RoomWindowViewModel RoomWindowVM
		{
			get { return roomWindowVM.GetValue(); }
			set { ChangeAndNotifyHistory<RoomWindowViewModel>(roomWindowVM, value, () => RoomWindowVM ); }
		}


		/// <summary>
		/// Field which backs the RoomRoleVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<RoomRoleViewModel>> roomRoleVMs = new HistoryableProperty<ObservableCollection<RoomRoleViewModel>>(null);

		/// <summary>
		/// Gets / sets the RoomRoleVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<RoomRoleViewModel> RoomRoleVMs
		{
			get { return roomRoleVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<RoomRoleViewModel>>(roomRoleVMs, value, () => RoomRoleVMs ); }
		}
	}
}
	