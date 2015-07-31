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
using YoYoStudio.Controls.Winform;

namespace TestMusicPlay
{
    /// <summary>
    /// Interaction logic for MusicFlexControl.xaml
    /// </summary>
    public partial class MusicFlexControl : UserControl
    {
       public string MoviePath
        {
            get { return (string)GetValue(MoviePathProperty); }
            set { SetValue(MoviePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MoviePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoviePathProperty =
            DependencyProperty.Register("MoviePath", typeof(string), typeof(MusicFlexControl), new PropertyMetadata(string.Empty, (o, e) =>
            {
                MusicFlexControl mc = o as MusicFlexControl;
                if (mc != null && e.NewValue != null)
                {
                    mc.flex.MoviePath = e.NewValue.ToString();
                    mc.flex.LoadFlex();
                }
            }));

        public event FlashCallbackEventHandler FlashCallback;

        public MusicFlexControl()
        {
            InitializeComponent();
            
        }



        private void flex_FlashCallback(YoYoStudio.Controls.Winform.FlexCallbackCommand cmd, List<string> args)
        {
            switch (cmd)
            {
                case FlexCallbackCommand.AudioStateChanged:
                    break;
                case FlexCallbackCommand.ExtendVideo:
                    break;
                case FlexCallbackCommand.LoadComplete:
                     CallFlash(FlexCommand.ConnectRTMP, "rtmp://129.223.253.33/oflaDemo");
                CallFlash(FlexCommand.LoadMusics);
                    break;
                case FlexCallbackCommand.None:
                    break;
                case FlexCallbackCommand.ReportStatus:
                    break;
                case FlexCallbackCommand.ScaleXDefault:
                    break;
                case FlexCallbackCommand.ScaleXMirror:
                    break;
                case FlexCallbackCommand.TakePicture:
                    break;
                case FlexCallbackCommand.VideoStateChanged:
                    break;
                case FlexCallbackCommand.ZoomIn:
                    break;
                case FlexCallbackCommand.ZoomOut:
                    break;
                default:
                    break;
            }
        }

        public string[] CallFlash(FlexCommand cmd, params string[] args)
        {
            return flex.CallFlash(cmd, args);
        }

        public void Dispose()
        {
            flex.Dispose();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
