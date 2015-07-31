using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace TestMusicPlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string flexPath;
        public string FlexPath 
        {
            get { return flexPath; }
            set { flexPath = value; OnPropertyChange("FlexPath"); }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //videoControl.FlashCallback += videoControl_FlashCallback;
        }

        void videoControl_FlashCallback(YoYoStudio.Controls.Winform.FlexCallbackCommand cmd, List<string> args)
        {
            //videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.ConnectRTMP, "");
        }

        private void OnPropertyChange(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            musicControl.MoviePath = @"D:\personal\YoYoGooglecode\9258Suite\TestMusicPlay\FlexMusic.swf";
            //musicControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.ConnectRTMP, "");

            //videoControl.MoviePath = @"D:\personal\YoYoGooglecode\9258Suite\TestMusicPlay\FlexMedia.swf";
            //videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.ConnectRTMP, "");
        }
    }
}
