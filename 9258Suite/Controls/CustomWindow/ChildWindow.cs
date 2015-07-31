using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using YoYoStudio.Common.Wpf;

namespace YoYoStudio.Controls.CustomWindow
{
    public abstract class ChildWindow<ActionType> : MessageSinkWindow<ActionType> where ActionType : struct
    {
        #region Window styles
        

        #endregion

        public ChildWindow()
        {
            ShowInTaskbar = false;
            Loaded += ChildWindow_Loaded;
        }

        void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStyle = System.Windows.WindowStyle.None;
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);
            int extStyle = (int)WinApi.GetWindowLong(wndHelper.Handle, (int)WinApi.GetWindowLongFields.GWL_EXSTYLE);
            extStyle |= (int)WinApi.ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            WinApi.SetWindowLong(wndHelper.Handle, (int)WinApi.GetWindowLongFields.GWL_EXSTYLE, (IntPtr)extStyle);
        }

        private Window parentWindow;
        public void SetParent<ParentActionType>(ParentWindow<ParentActionType> parent, bool embedded) where ParentActionType : struct
        {
            if (parentWindow != null)
            {
                ParentWindow<ParentActionType> p = parentWindow as ParentWindow<ParentActionType>;
                if (embedded)
                {
                    p.PositionChanged -= ParentWindow_PositionChangedHandler;
                    p.StateChanged -= ParentWindow_StateChangedHandler;
                    p.SizeChanged -= ParentWindow_SizeChangedHandler;
                }
                
                p.Closed -= ParentWindow_ClosedHandler;
            }
            parentWindow = parent;
            if (parentWindow != null)
            {
                ParentWindow<ParentActionType> p = parentWindow as ParentWindow<ParentActionType>;
                if (embedded)
                {
                    p.PositionChanged += ParentWindow_PositionChangedHandler;
                    p.StateChanged += ParentWindow_StateChangedHandler;
                    p.SizeChanged += ParentWindow_SizeChangedHandler;
                    ResizeMode = System.Windows.ResizeMode.NoResize;
                    WindowStyle = System.Windows.WindowStyle.None;
                }
                else
                {
                    ResizeMode = System.Windows.ResizeMode.CanResize;
                    WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                }
                p.Closed += ParentWindow_ClosedHandler;
            }
        }

        private Control replicatedControl;
        public Control ReplicatedControl
        {
            get { return replicatedControl; }
            set
            {
                if (replicatedControl != null)
                {
                    replicatedControl.SizeChanged -= ReplicatedControl_SizeChangedHandler;
                }
                replicatedControl = value;
                if (replicatedControl != null)
                {
                    double w = 0.0;
                    double h = 0.0;
                    replicatedControl.Dispatcher.Invoke((Action)(() =>
                    {
                        w = replicatedControl.ActualWidth;
                        h = replicatedControl.ActualHeight;
                    }));                    
                    AdjustSize(w, h);
                    replicatedControl.SizeChanged += ReplicatedControl_SizeChangedHandler;
                }
            }
        }

        #region Handlers

        void ReplicatedControl_SizeChangedHandler(object sender, SizeChangedEventArgs e)
        {
            double w = 0.0;
            double h = 0.0;
            replicatedControl.Dispatcher.Invoke((Action)(() =>
            {
                w = e.NewSize.Width;
                h = e.NewSize.Height;
            }));
            AdjustSize(w, h);
        }

        void AdjustSize(double w, double h)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                if (WindowState != System.Windows.WindowState.Minimized)
                {
                    Width = w;
                    Height = h;
                }
            }));
        }

        void ParentWindow_ClosedHandler(object sender, EventArgs e)
        {
            Dispatcher.Invoke((Action)(()=>Close()));
        }

        void ParentWindow_SizeChangedHandler(object sender, SizeChangedEventArgs e)
        {
            //AdjustPosition();
        }

        void AdjustPosition()
        {
            WindowState state = System.Windows.WindowState.Normal;
            double top = 0.0;
            double left = 0.0;
            parentWindow.Dispatcher.Invoke((Action)(() =>
            {
                state = parentWindow.WindowState;
                top = parentWindow.Top;
                left = parentWindow.Left;
            }));

            Dispatcher.Invoke((Action)(() =>
            {
                switch (state)
                {
                    case System.Windows.WindowState.Minimized:
                        Hide();
                        break;
                    case System.Windows.WindowState.Maximized:
                        Top = OffsetY;
                        Left = OffsetX;
                        Show();
                        break;
                    case System.Windows.WindowState.Normal:
                        Top = top + OffsetY;
                        Left = left + OffsetX;
                        Show();
                        break;
                }
            }));
        }

        void ParentWindow_StateChangedHandler(object sender, EventArgs e)
        {
            AdjustPosition();
        }

        void ParentWindow_PositionChangedHandler(double arg1, double arg2)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                Left += arg1;
                Top += arg2;
            }));
        }

        #endregion

        #region Properties

        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register("OffsetY", typeof(double), typeof(ChildWindow<ActionType>), new PropertyMetadata(0.0, (o, e) =>
                {
                    ChildWindow<ActionType> child = o as ChildWindow<ActionType>;
                    if (child != null && child.parentWindow != null)
                    {
                        double oldOffset = (double)e.OldValue;
                        double newOffset = (double)e.NewValue;
                        if (Math.Abs(newOffset - oldOffset) > 0.1)
                        {
                            double top = 0.0;
                            child.parentWindow.Dispatcher.Invoke((Action)(() => top = child.parentWindow.Top));
                            child.Dispatcher.Invoke((Action)(() => child.Top = top + newOffset));
                        }
                    }
                }));

        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register("OffsetX", typeof(double), typeof(ChildWindow<ActionType>), new PropertyMetadata(0.0, (o, e) =>
                {
                    ChildWindow<ActionType> child = o as ChildWindow<ActionType>;
                    if (child != null && child.parentWindow != null)
                    {
                        double oldOffset = (double)e.OldValue;
                        double newOffset = (double)e.NewValue;
                        if (Math.Abs(newOffset - oldOffset) > 0.1)
                        {
                            double left = 0.0;
                            child.parentWindow.Dispatcher.Invoke((Action)(() => left = child.parentWindow.Left));
                            child.Dispatcher.Invoke((Action)(() => child.Left = left + newOffset));
                        }
                    }
                }));

        #endregion

    }
}
