




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
	public partial class CommandViewModel
	{
	

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
		/// Field which backs the Description property
		/// </summary>
		private HistoryableProperty<string> description = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Description value
		/// </summary>
		[Browsable(false)]
		public  string Description
		{
			get { return description.GetValue(); }
			set { ChangeAndNotifyHistory<string>(description, value, () => Description ); }
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
		/// Field which backs the CommandType property
		/// </summary>
		private HistoryableProperty<int> commandType = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the CommandType value
		/// </summary>
		[Browsable(false)]
		public  int CommandType
		{
			get { return commandType.GetValue(); }
			set { ChangeAndNotifyHistory<int>(commandType, value, () => CommandType ); }
		}


		/// <summary>
		/// Field which backs the Disable property
		/// </summary>
		private HistoryableProperty<bool> disable = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the Disable value
		/// </summary>
		[Browsable(false)]
		public  bool Disable
		{
			get { return disable.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(disable, value, () => Disable ); }
		}
	}
}
	