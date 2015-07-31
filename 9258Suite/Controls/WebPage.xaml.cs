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
using YoYoStudio.Common;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Controls
{
	/// <summary>
	/// Interaction logic for WebPage.xaml
	/// </summary>
	[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	[System.Runtime.InteropServices.ComVisibleAttribute(true)]
	public partial class WebPage : UserControl, IDisposable
	{
		public WebPage()
		{
			InitializeComponent();
			webBrowser.LoadCompleted += webBrowser_LoadCompleted;
		}

        protected bool loaded = false;

		#region Dependency Properties

        public string LocalHtmlFile
        {
            get { return (string)GetValue(LocalHtmlFileProperty); }
            set { SetValue(LocalHtmlFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LocalHtmlFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocalHtmlFileProperty =
            DependencyProperty.Register("LocalHtmlFile", typeof(string), typeof(WebPage), new PropertyMetadata(string.Empty));



		/// <summary>
		/// partial html file that contains the content within the body 
		/// </summary>
		public string Body
		{
			get { return (string)GetValue(BodyProperty); }
			set { SetValue(BodyProperty, value); }
		}

		public static readonly DependencyProperty BodyProperty =
			DependencyProperty.Register("Body", typeof(string), typeof(WebPage), new PropertyMetadata(string.Empty));
        	

		public string Css
		{
			get { return (string)GetValue(CssFileProperty); }
			set { SetValue(CssFileProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CssDirectory.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CssFileProperty =
			DependencyProperty.Register("Css", typeof(string), typeof(WebPage), new PropertyMetadata(string.Empty));


		public string JavaScript
		{
			get { return (string)GetValue(JavaScriptFileProperty); }
			set { SetValue(JavaScriptFileProperty, value); }
		}

		// Using a DependencyProperty as the backing store for JavaScriptDirectory.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty JavaScriptFileProperty =
			DependencyProperty.Register("JavaScript", typeof(string), typeof(WebPage), new PropertyMetadata(string.Empty));
				
		#endregion

		#region Event Handlers

        private void Broswer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InvokeJavaScript("Resize", (int)e.NewSize.Width, (int)e.NewSize.Height);
        }

		private void DataContextChangedHandler(object sender, DependencyPropertyChangedEventArgs e)
		{
			webBrowser.ObjectForScripting = e.NewValue;			
		}

		private void webBrowser_LoadCompleted(object sender, NavigationEventArgs e)
		{
            if (!loaded)
            {
                loaded = true;
                webBrowser.SizeChanged += Broswer_SizeChanged;
                Utility.MinimizeRelease();
                if (LoadCompleted != null)
                {
                    LoadCompleted();
                }
            }
		}

		#endregion

		public void InvokeJavaScript(string scriptName, params object[] args)
		{
			webBrowser.InvokeScript(scriptName, args);
            Utility.MinimizeRelease();
		}

        public void LoadHtmlFile()
        {
            if (!string.IsNullOrEmpty(LocalHtmlFile))
            {
                webBrowser.Source = new Uri(LocalHtmlFile);
            }
        }

		public event Action LoadCompleted;

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
                Utility.MinimizeRelease();
				webBrowser.Dispose();
				disposed = true;
			}
		}

		~WebPage()
		{
			Dispose(false);
		}

		#endregion

	}
}
