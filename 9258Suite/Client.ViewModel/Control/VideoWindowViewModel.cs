using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YoYoStudio.Common.ObjectHistory;

namespace YoYoStudio.Client.ViewModel
{
	public class VideoWindowViewModel : WindowViewModel
	{
        public VideoWindowViewModel():this(null)
        {
        }

		public VideoWindowViewModel(UserViewModel uvm)
		{
			UserVM = uvm;
		}

        /// <summary>
        /// Field which backs the SelectedUserVM property
        /// </summary>
        private HistoryableProperty<UserViewModel> userVM = new HistoryableProperty<UserViewModel>(null);

        /// <summary>
        /// Gets / sets the SelectedUserVM value
        /// </summary>
        [Browsable(false)]
        public UserViewModel UserVM
        {
            get { return userVM.GetValue(); }
            set { ChangeAndNotifyHistory<UserViewModel>(userVM, value, () => UserVM); }
        }
	}
}
