using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoYoStudio.Controls.Winform;

namespace YoYoStudio.Controls
{
    /// <summary>
    /// Interaction logic for WpfFlexControl.xaml
    /// </summary>
    public partial class WpfFlexControl : UserControl, IDisposable
    {
        public WpfFlexControl()
        {
            InitializeComponent();
			flex.FlashCallback += flex_FlashCallback;
        }

		#region Privates

		private void flex_FlashCallback(FlexCallbackCommand cmd, List<string> args)
		{
			if (FlashCallback != null)
			{
                FlashCallback(cmd, args);
			}
		}

		#endregion

		public event FlashCallbackEventHandler FlashCallback;

		public string MoviePath
		{
			get { return (string)GetValue(MoviePathProperty); }
			set { SetValue(MoviePathProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MoviePath.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MoviePathProperty =
			DependencyProperty.Register("MoviePath", typeof(string), typeof(WpfFlexControl), new PropertyMetadata(string.Empty));

		public string[] CallFlash(FlexCommand cmd, params string[] args)
		{
            return flex.CallFlash(cmd, args);
		}

		public void LoadFlex()
		{
			if (!string.IsNullOrEmpty(MoviePath) && File.Exists(MoviePath))
			{
				flex.LoadFlash(MoviePath);
			}
		}

        #region IDisposable Implementation

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }
                flex.Dispose();
                disposed = true;
            }
        }

        ~WpfFlexControl()
        {
            Dispose(false);
        }

        #endregion
    }
}
