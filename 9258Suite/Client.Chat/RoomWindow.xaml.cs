using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using YoYoStudio.Client.Chat.Controls;
using YoYoStudio.Client.Chat.WebPages;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Common;
using YoYoStudio.Common.Wpf;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;
using YoYoStudio.Model.Media;

namespace YoYoStudio.Client.Chat
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
    public partial class RoomWindow
    {
        private RoomWindowViewModel roomWindowVM;
        private HallWindow hallWindow;
        private PlayMusicWindow playMusicWindow = null;
        public RoomWindow(RoomWindowViewModel roomWindowVM):this(roomWindowVM,null)
        {
        }

        public RoomWindow(RoomWindowViewModel roomWindowVM, HallWindow hwnd)
            :base(roomWindowVM)
        {
            roomWindowVM.Load(AllWebPages.RoomPage);
            roomWindowVM.Initialize();
            this.roomWindowVM = roomWindowVM;
            InitializeComponent();
            Loaded += RoomWindow_Loaded;
            hallWindow = hwnd;
            if(hallWindow != null)
                hallWindow.StateChanged += hallWindow_StateChanged;
        }

        private void InitMusicWindow()
        {
            if (playMusicWindow == null)
            {
                PlayMusicWindowViewModel playMusicVM = new PlayMusicWindowViewModel();
                playMusicWindow = new PlayMusicWindow(playMusicVM);
                playMusicWindow.StateChanged += playMusicWindow_StateChanged;
                playMusicWindow.Closed += playMusicWindow_Closed;
            }
        }

        void hallWindow_StateChanged(object sender, EventArgs e)
        {
            HallWindow hwnd = sender as HallWindow;
            if (hwnd != null)
            {
                if (hwnd.WindowState != System.Windows.WindowState.Minimized)
                    WindowState = System.Windows.WindowState.Minimized;
            }
        }

        void RoomWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CreateVideoWindow(videoBorder1, roomWindowVM.FirstVideoWindowVM);
            CreateVideoWindow(videoBorder2, roomWindowVM.SecondVideoWindowVM);
            CreateVideoWindow(videoBorder3, roomWindowVM.ThirdVideoWindowVM);
            webWindow = CreateWebWindow() as WebWindow;
            Task.Factory.StartNew(() =>
                {
                    roomWindowVM.InitializeAudio();
                    
                });
            InitMusicWindow();
        }

        private Window CreateWebWindow()
        {
            Point p = PART_Web.TransformToAncestor(this).Transform(new Point(0, 0));
            double x = p.X;
            double y = p.Y;
            return CreateWindowInSeparateThread<WebWindowAction>(() =>
            {
                return new WebWindow(roomWindowVM);
            },
                x, y, false, true, PART_Web);
        }

        private Window CreateVideoWindow(ContentControl videoBorder,VideoWindowViewModel vm)
        {
            Point p = videoBorder.TransformToAncestor(this).Transform(new Point(0, 0));
            double x = p.X;
            double y = p.Y;
            return CreateWindowInSeparateThread<VideoWindowAction>(() =>
            {
                return new VideoWindow(vm,true);
            },
                x, y, false, true, videoBorder);
        }

		protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, RoomWindowAction> message)
        {
            RoomWindowViewModel roomVM = DataContext as RoomWindowViewModel;
            switch (message.Action)
            {
                case RoomWindowAction.ShowConfigWindow:
                    ConfigurationItemViewModel configItem = message.Content as ConfigurationItemViewModel;
                    roomVM.ApplicationVM.ConfigurationWindowVM = new ConfigurationWindowViewModel(configItem);
                    ShowWebWindow(webWindow,false);
                    ConfigurationWindow configurationWindow = new ConfigurationWindow(roomVM.ApplicationVM.ConfigurationWindowVM);
                    configurationWindow.Owner = this;
                    configurationWindow.ShowDialog();
                    ShowWebWindow(webWindow,true);
                    break;
                case RoomWindowAction.PlayMusic:
                    bool canPlay = (bool)message.Content;
                    if (canPlay)
                    {
                        if (playMusicWindow == null)
                            InitMusicWindow();
                        ShowWebWindow(webWindow, false);
                        playMusicWindow.WindowState = System.Windows.WindowState.Normal;
                        playMusicWindow.Show();
                        playMusicWindow.Topmost = true;
                    }
                    else
                    {
                        ShowWebWindow(webWindow, false);
                        if (System.Windows.MessageBox.Show("其它人正在播放音乐，请稍后再试", "提示", MessageBoxButton.OK) == MessageBoxResult.OK)
                            ShowWebWindow(webWindow, true);
                    }
                    break;
                
                case RoomWindowAction.ManageMusic:
                    ShowWebWindow(webWindow, false);
                    ManageMusicWindowViewModel manageMusicVM = new ManageMusicWindowViewModel();
                    ManageMusicWindow manageWnd = new ManageMusicWindow(manageMusicVM);
                    manageWnd.Owner = this;
                    manageWnd.Topmost = true;
                    ShowWebWindow(webWindow, false);
                    manageWnd.ShowDialog();
                    ShowWebWindow(webWindow, true);
                    break;

                case RoomWindowAction.RecordAudio:
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "音频文件 (*.wav)|*.wav";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        RoomWindowViewModel vm = DataContext as RoomWindowViewModel;
                        vm.StartAudioRecording(dialog.FileName);
                    }
                    break;
                default:
                    break;
            }
        }

        void playMusicWindow_Closed(object sender, EventArgs e)
        {
            playMusicWindow.StateChanged -= playMusicWindow_StateChanged;
            playMusicWindow.Closed -= playMusicWindow_Closed;
            playMusicWindow = null;
            ShowWebWindow(webWindow, true);
            InitMusicWindow();
        }

        void playMusicWindow_StateChanged(object sender, EventArgs e)
        {

            if (playMusicWindow.WindowState == System.Windows.WindowState.Minimized)
            {
                if (this.WindowState != System.Windows.WindowState.Minimized)
                    ShowWebWindow(webWindow, true);
            }
            else if (playMusicWindow.WindowState == System.Windows.WindowState.Normal)
            {
                if(this.WindowState != System.Windows.WindowState.Minimized)
                    ShowWebWindow(webWindow, false);
            }
        }
        

        public override void CallJavaScript(string functionName, params object[] args)
        {
            webWindow.CallJavaScript(functionName, args);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (hallWindow != null)
                hallWindow.StateChanged -= hallWindow_StateChanged;
            if(playMusicWindow != null)
            {
                playMusicWindow.StateChanged -= playMusicWindow_StateChanged;
                playMusicWindow.Closed -= playMusicWindow_Closed;
                playMusicWindow.Close();
            }
            base.OnClosed(e);
            //Dispatcher.Invoke((Action)(() => Helper.MainWindow.Show()));
        }

    }
}
