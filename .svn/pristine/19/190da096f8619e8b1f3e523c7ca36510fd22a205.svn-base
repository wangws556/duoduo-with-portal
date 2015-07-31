using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace YoYoStudio.Common.Wpf.ViewModel
{
    [ComVisible(true)]
    [Serializable]
    public partial class TitledViewModel : ViewModelBase
    {
        /// <summary>
        /// Field which backs the Title property
        /// </summary>
        protected string title = string.Empty;

        /// <summary>
        /// Gets / sets the Title value
        /// </summary>
        public string Title
        {
            get { return title; }
            set { ChangeAndNotify<string>(ref title, value, () => Title); }
        }
    }

}
