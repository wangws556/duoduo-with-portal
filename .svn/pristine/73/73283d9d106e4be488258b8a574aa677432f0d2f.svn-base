using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio.Utils;
using NAudio.Wave;
using YoYoStudio.Media.NAudio;
using NAudio.Wave.Compression;
using YoYoStudio.Common.Net;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Interop;


namespace TestWpf
{
	internal enum WM
	{
		WINDOWPOSCHANGING = 0x0047
	}
	[StructLayout(LayoutKind.Sequential)]
	internal struct WINDOWPOS
	{
		public IntPtr hwnd;
		public IntPtr hwndInsertAfter;
		public int x;
		public int y;
		public int cx;
		public int cy;
		public int flags;
	}

	

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetCursorPos(ref Win32Point pt);

		[StructLayout(LayoutKind.Sequential)]
		internal struct Win32Point
		{
			public Int32 X;
			public Int32 Y;
		};
		public static Point GetMousePosition()
		{
			Win32Point w32Mouse = new Win32Point();
			GetCursorPos(ref w32Mouse);
			return new Point(w32Mouse.X, w32Mouse.Y);
		}

		MainViewModel mainVM = new MainViewModel();
		private bool isDragging = false;
		public MainWindow()
		{
			InitializeComponent();
			DataContext = mainVM;
			Loaded += MainWindow_Loaded;
			//this.MouseMove += MainWindow_MouseMove;
			//MouseLeftButtonUp += MainWindow_MouseLeftButtonUp;
			//MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
		}


		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("base");
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			//HwndSource hwndSource = (HwndSource)HwndSource.FromVisual((Window)this);
			//hwndSource.AddHook(DragHook);
		}

		private IntPtr DragHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handeled)
		{
			switch ((WM)msg)
			{
				case WM.WINDOWPOSCHANGING:
					{
						WINDOWPOS pos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
						mainVM.Output = string.Format("X : {0} -- Y : {1}", pos.x, pos.y) + " ----- "
							+ string.Format("CX : {0} -- CY : {1}", pos.cx, pos.cy);
						mainVM.TempVM.SetPos(pos.x, pos.y);
						//if ((pos.flags & (int)SWP.NOMOVE) != 0)
						//{
						//	return IntPtr.Zero;
						//}

						//Window wnd = (Window)HwndSource.FromHwnd(hwnd).RootVisual;
						//if (wnd == null)
						//{
						//	return IntPtr.Zero;
						//}

						// ** do whatever you need here **
						// the new window position is in the pos variable
						// just note that those are in Win32 "screen coordinates" not WPF device independent pixels

					}
					break;
			}

			return IntPtr.Zero;
		}

		void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			throw new NotImplementedException();
		}

		void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			isDragging = false;
		}

		void MainWindow_MouseMove(object sender, MouseEventArgs e)
		{
			var x = GetMousePosition().X;
			var y = GetMousePosition().Y;
			//mainVM.Output = string.Format("X : {0} -- Y : {1}", x, y);
		}


        private int count = 1;

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
            NewWindowHandler();
            count++;
		}
		private void Button_Click_4(object sender, RoutedEventArgs e)
		{
		}

        private void NewWindowHandler()
        {
            Thread newWindowThread = new Thread(new ThreadStart(ThreadStartingPoint));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
        }
        private void ThreadStartingPoint()
        {
            AWindow tempWindow = new AWindow(mainVM.TempVM);
            tempWindow.Show();
            System.Windows.Threading.Dispatcher.Run();            
        }

		private void DockPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				this.DragMove();
			}
		}

		private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
		{
			Left += e.HorizontalChange;
			Top += e.VerticalChange;
			mainVM.TempVM.SetPos(e.HorizontalChange, e.VerticalChange);
		}
				
		
	}
}
