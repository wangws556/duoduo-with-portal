using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using YoYoStudio.Client.Chat.Controls;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Common;
using YoYoStudio.Common.Wpf;
using YoYoStudio.Controls.CustomWindow;


namespace YoYoStudio.Client.Chat
{
	public abstract partial class WindowBase<ActionType> : ParentWindow<ActionType> where ActionType :struct
	{
		protected DockPanel topPanel;
		protected Border windowButtons;
		protected Grid windowGrid;
		protected Border windowBorder;
		protected Thumb dragThumb;
		protected WindowViewModel dataContext;
        protected WebWindow webWindow;
       

		public WindowBase(WindowViewModel dc)
            :this(dc,null)
		{
		}

        public WindowBase(WindowViewModel dc, WebWindow wWnd)
        {
            dataContext = dc;
            Template = FindResource("WindowTemplate") as ControlTemplate;
            ApplyTemplate();
            Initialize();
            DataContext = dc;
            Loaded += WindowBase_Loaded;

            //MinHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            MinHeight = SystemParameters.FullPrimaryScreenHeight;
            Border border = Template.FindName("RoomHeaderBorder",this) as Border;
            if (border != null )
            {
                if (dc is RoomWindowViewModel)
                    border.Visibility = System.Windows.Visibility.Visible;
            }
            webWindow = wWnd;
        }

       

		void WindowBase_Loaded(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(BorderStyleKeyName))
			{
				Style style = Application.Current.FindResource(BorderStyleKeyName) as Style;
				if (style != null)
				{
					windowBorder.Style = style;
				}
			}
		}

		private void Initialize()
		{
            windowBorder = Template.FindName("windowBorder", this) as Border;
            windowGrid = Template.FindName("windowGrid", this) as Grid;
            topPanel = Template.FindName("topPanel", this) as DockPanel;
			windowButtons = Template.FindName("windowButtons", this) as Border;
            dragThumb = Template.FindName("dragThumb",this) as Thumb;
			dragThumb.DragDelta += dragThumb_DragDelta;
			//topPanel.MouseMove += DockPanel_MouseMove;
		}

		void dragThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
            Top += e.VerticalChange;
            Left += e.HorizontalChange;
            RaisePositionChanged(e.HorizontalChange, e.VerticalChange);
		}

		protected void DockPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
				this.DragMove();
		}

		protected override Decorator GetWindowButtonsPlaceholder()
		{
			return windowButtons;
		}

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
           
            CallJavaScript("ReleaseMemory");
            base.OnClosing(e);
        }

        protected void CloseWebWindow(WebWindow wnd)
        {
            if (wnd != null)
            {
                wnd.Dispatcher.Invoke((Action)(() =>
                    {
                        wnd.Close();
                    }));
            }
        }

        protected void ShowWebWindow(WebWindow wnd, bool show)
        {
            if (wnd != null)
            {
                wnd.Dispatcher.Invoke((Action)(() =>
                {
                    if (show)
                    {
                        wnd.Show();
                    }
                    else
                    {
                        wnd.Hide();
                    }
                }));
            }
        }

		public string BorderStyleKeyName { get; set; }
    }
}
