




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
	public partial class ConfigurationWindowViewModel
	{
	

		/// <summary>
		/// Field which backs the ConfigurationItemVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<ConfigurationItemViewModel>> configurationItemVMs = new HistoryableProperty<ObservableCollection<ConfigurationItemViewModel>>(null);

		/// <summary>
		/// Gets / sets the ConfigurationItemVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<ConfigurationItemViewModel> ConfigurationItemVMs
		{
			get { return configurationItemVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<ConfigurationItemViewModel>>(configurationItemVMs, value, () => ConfigurationItemVMs ); }
		}


		/// <summary>
		/// Field which backs the CurrentConfigurationItemVM property
		/// </summary>
		private HistoryableProperty<ConfigurationItemViewModel> currentConfigurationItemVM = new HistoryableProperty<ConfigurationItemViewModel>(null);

		/// <summary>
		/// Gets / sets the CurrentConfigurationItemVM value
		/// </summary>
		[Browsable(false)]
		public  ConfigurationItemViewModel CurrentConfigurationItemVM
		{
			get { return currentConfigurationItemVM.GetValue(); }
			set { ChangeAndNotifyHistory<ConfigurationItemViewModel>(currentConfigurationItemVM, value, () => CurrentConfigurationItemVM ); }
		}


		/// <summary>
		/// Field which backs the Message property
		/// </summary>
		private HistoryableProperty<string> message = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Message value
		/// </summary>
		[Browsable(false)]
		public  string Message
		{
			get { return message.GetValue(); }
			set { ChangeAndNotifyHistory<string>(message, value, () => Message ); }
		}
	}
}
	