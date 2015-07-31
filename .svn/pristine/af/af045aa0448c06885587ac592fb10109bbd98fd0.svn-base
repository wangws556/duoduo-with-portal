using System;
using System.Collections.Generic;
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
using YoYoStudio.Common;
using YoYoStudio.ManagementTool.ViewModel;
using YoYoStudio.Common.Wpf;
using System.IO;

namespace YoYoStudio.ManagementTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            Singleton<MainWindowViewModel>.Instance.Initialize();
            InitializeComponent();
            DataContext = Singleton<MainWindowViewModel>.Instance;
        }

        private SmileManagement smileControl;
        private StampManagement stampControl;

        protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, Actions> message)
        {
            switch (message.Action)
            {
                case Actions.SmileManagement:
                    if (smileControl == null)
                    {
                        smileControl = new SmileManagement();
                    }
                    mainContent.Content = smileControl;
                    break;
                case Actions.StampManagement:
                    if (stampControl == null)
                    {
                        stampControl = new StampManagement();
                    }
                    mainContent.Content = stampControl;
                    break;
                case Actions.ImportGif:
                    ImportGif();
                    break;
                default:
                    break;

            }
        }

        private void ImportGif()
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDlg.Description = "选择图片所在的文件夹...";
            folderBrowserDlg.ShowNewFolderButton = true;
            folderBrowserDlg.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folerName = folderBrowserDlg.SelectedPath;

                if (!string.IsNullOrEmpty(folerName) && Directory.Exists(folerName))
                {
                    MessageBox.Show("请等待图片上传结束");
                    Singleton<MainWindowViewModel>.Instance.ImportGifs(folerName);
                }
            }
        }
    }
}
