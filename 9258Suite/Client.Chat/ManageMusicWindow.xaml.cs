using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Common;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.Chat
{
    /// <summary>
    /// Interaction logic for ManageMusicWindow.xaml
    /// </summary>
    public partial class ManageMusicWindow
    {
        public ManageMusicWindow(ManageMusicWindowViewModel vm)
            : base(vm)
        {
            MinHeight = ActualHeight;//overide the MiniHeight set by window base
            MinimizeButtonState = YoYoStudio.Controls.CustomWindow.WindowButtonState.Disabled;
            MaximizeButtonState = YoYoStudio.Controls.CustomWindow.WindowButtonState.Disabled;
            Loaded += ManageMusicWindow_Loaded;
            DataContext = vm;
            if (vm != null)
                vm.Initialize();
            InitializeComponent();
        }

        void ManageMusicWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ManageMusicWindowViewModel vm = DataContext as ManageMusicWindowViewModel;
            if (vm != null)
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    vm.LoadMusicsFromServer();
                }));
            }
        }

        protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, ManageMusicWindowAction> message)
        {
            switch (message.Action)
            {
                case ManageMusicWindowAction.UploadMusic:
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = "MP3文件 (*.mp3)|*.mp3|FLV 文件 (*.flv)|*.flv";
                    dlg.Multiselect = false;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!String.IsNullOrEmpty(dlg.FileName))
                        {
                            try
                            {
                                string path = ConvertToFLV(dlg.FileName);
                                if (!System.IO.File.Exists(path))
                                {
                                    System.Windows.MessageBox.Show(this, "请确认歌曲名字和其所在的路径没有空格", Messages.UploadError);
                                    return;
                                }
                            
                                ManageMusicWindowViewModel viewModel = DataContext as ManageMusicWindowViewModel;
                                bool result = viewModel.UploadMusic(path);
                                if (result == true)
                                {
                                    System.Windows.MessageBox.Show(this, Messages.UploadSucceed);
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show(this, Messages.UploadError);
                                }
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(this, e.Message);
                            }
                            
                        }
                        else
                        {
                            System.Windows.MessageBox.Show(this, "请先选择歌曲。");
                        }
                    }
                    break;
                case ManageMusicWindowAction.DeleteMusic:
                    if(System.Windows.MessageBox.Show("确定要删除选择的歌曲吗？",Text.Warning,MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        ManageMusicWindowViewModel mvm = DataContext as ManageMusicWindowViewModel;    
                        mvm.DeleteMusic();
                    }
                    break;
                case ManageMusicWindowAction.LoadMusicComplete:
                    ManageMusicWindowViewModel vm = DataContext  as ManageMusicWindowViewModel;
                    PART_Loading.Visibility = System.Windows.Visibility.Collapsed;
                    PART_Content.Visibility = System.Windows.Visibility.Visible;
                    MusicDataGrid.ItemsSource = vm.Musics;
                    break;
                default:
                    break;
            }
        }

        private string ConvertToFLV(string song)
        {
            string result = "";
            string ext = System.IO.Path.GetExtension(song);
            if (ext == ".flv")
                result = song;
            else if (ext == ".mp3")
            {   
                //string outputDir = AppDomain.CurrentDomain.BaseDirectory + @"Flex\Songs";
                string outputDir = Utility.GetOSDisk() + @"9258\Songs";
                if(!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }
                string name = System.IO.Path.GetFileNameWithoutExtension(song);
                result = outputDir + "\\" + name + ".flv";
                Utility.MusicToFLV(song, result);
            }
            return result;
        }

    }
}
