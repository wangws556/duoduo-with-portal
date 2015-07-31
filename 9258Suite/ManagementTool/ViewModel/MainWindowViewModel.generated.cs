




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


namespace YoYoStudio.ManagementTool.ViewModel
{
	public partial class MainWindowViewModel
	{
	

		/// <summary>
		/// Field which backs the Commands property
		/// </summary>
		private HistoryableProperty<ObservableCollection<SecureCommandViewModel>> commands = new HistoryableProperty<ObservableCollection<SecureCommandViewModel>>(new ObservableCollection<SecureCommandViewModel>());

		/// <summary>
		/// Gets / sets the Commands value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<SecureCommandViewModel> Commands
		{
			get { return commands.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<SecureCommandViewModel>>(commands, value, () => Commands ); }
		}
	}
}
	