




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
	public partial class ExchangeRateViewModel
	{
	

		/// <summary>
		/// Field which backs the ApplicationId property
		/// </summary>
		private HistoryableProperty<int> applicationId = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the ApplicationId value
		/// </summary>
		[Browsable(false)]
		public  int ApplicationId
		{
			get { return applicationId.GetValue(); }
			set { ChangeAndNotifyHistory<int>(applicationId, value, () => ApplicationId ); }
		}


		/// <summary>
		/// Field which backs the ValidTime property
		/// </summary>
		private HistoryableProperty<string> validTime = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the ValidTime value
		/// </summary>
		[Browsable(false)]
		public  string ValidTime
		{
			get { return validTime.GetValue(); }
			set { ChangeAndNotifyHistory<string>(validTime, value, () => ValidTime ); }
		}


		/// <summary>
		/// Field which backs the ScoreToMoney property
		/// </summary>
		private HistoryableProperty<int> scoreToMoney = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the ScoreToMoney value
		/// </summary>
		[Browsable(false)]
		public  int ScoreToMoney
		{
			get { return scoreToMoney.GetValue(); }
			set { ChangeAndNotifyHistory<int>(scoreToMoney, value, () => ScoreToMoney ); }
		}


		/// <summary>
		/// Field which backs the MoneyToCache property
		/// </summary>
		private HistoryableProperty<int> moneyToCache = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the MoneyToCache value
		/// </summary>
		[Browsable(false)]
		public  int MoneyToCache
		{
			get { return moneyToCache.GetValue(); }
			set { ChangeAndNotifyHistory<int>(moneyToCache, value, () => MoneyToCache ); }
		}


		/// <summary>
		/// Field which backs the ScoreToCache property
		/// </summary>
		private HistoryableProperty<int> scoreToCache = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the ScoreToCache value
		/// </summary>
		[Browsable(false)]
		public  int ScoreToCache
		{
			get { return scoreToCache.GetValue(); }
			set { ChangeAndNotifyHistory<int>(scoreToCache, value, () => ScoreToCache ); }
		}
	}
}
	