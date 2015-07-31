




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
	public partial class RoleCommandViewModel
	{
	

		/// <summary>
		/// Field which backs the SourceRoleVM property
		/// </summary>
		private HistoryableProperty<RoleViewModel> sourceRoleVM = new HistoryableProperty<RoleViewModel>(null);

		/// <summary>
		/// Gets / sets the SourceRoleVM value
		/// </summary>
		[Browsable(false)]
		public  RoleViewModel SourceRoleVM
		{
			get { return sourceRoleVM.GetValue(); }
			set { ChangeAndNotifyHistory<RoleViewModel>(sourceRoleVM, value, () => SourceRoleVM ); }
		}


		/// <summary>
		/// Field which backs the TargetRoleVM property
		/// </summary>
		private HistoryableProperty<RoleViewModel> targetRoleVM = new HistoryableProperty<RoleViewModel>(null);

		/// <summary>
		/// Gets / sets the TargetRoleVM value
		/// </summary>
		[Browsable(false)]
		public  RoleViewModel TargetRoleVM
		{
			get { return targetRoleVM.GetValue(); }
			set { ChangeAndNotifyHistory<RoleViewModel>(targetRoleVM, value, () => TargetRoleVM ); }
		}


		/// <summary>
		/// Field which backs the CommandVM property
		/// </summary>
		private HistoryableProperty<CommandViewModel> commandVM = new HistoryableProperty<CommandViewModel>(null);

		/// <summary>
		/// Gets / sets the CommandVM value
		/// </summary>
		[Browsable(false)]
		public  CommandViewModel CommandVM
		{
			get { return commandVM.GetValue(); }
			set { ChangeAndNotifyHistory<CommandViewModel>(commandVM, value, () => CommandVM ); }
		}


		/// <summary>
		/// Field which backs the IsManagerCommand property
		/// </summary>
		private HistoryableProperty<bool> isManagerCommand = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the IsManagerCommand value
		/// </summary>
		[Browsable(false)]
		public  bool IsManagerCommand
		{
			get { return isManagerCommand.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(isManagerCommand, value, () => IsManagerCommand ); }
		}
	}
}
	