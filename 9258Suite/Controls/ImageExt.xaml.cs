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

namespace YoYoStudio.Controls
{

    enum ScaleDirection
    {
        Up,
        Down
    }
    /// <summary>
    /// Interaction logic for ImageExt.xaml
    /// </summary>
    public partial class ImageExt : UserControl,IDisposable
    {
        public ImageExt()
        {
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += new EventHandler(timer_Tick);
        }

        private System.Windows.Threading.DispatcherTimer timer;
        private ScaleDirection scaleDirection;

        public ImageSource ImageSource
        {
            get { return img.Source; }
            set { img.Source = value; }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            AdjustScale(scaleDirection, LogoScale);
        }

        void AdjustScale(ScaleDirection scaleDirection, ScaleTransform scale)
        {
            if (scaleDirection == ScaleDirection.Down)
            {
                if (scale.ScaleX < 1.1)
                {
                    scale.ScaleX += 0.05; scale.ScaleY += 0.05;
                }
                else
                    timer.Stop();
            }
            else
            {
                if (scale.ScaleX > 1.0)
                {
                    scale.ScaleX -= 0.05;
                    scale.ScaleY -= 0.05;
                }
                else
                    timer.Stop();
            }
        }

        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            scaleDirection = ScaleDirection.Down;
            timer.Start();
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            scaleDirection = ScaleDirection.Up;
            timer.Start();
        }

        public void Dispose()
        {
            if (timer != null)
                timer.Stop();
        }

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(ImageExt), new PropertyMetadata(null));

        private void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ClickCommand != null)
            {
                ClickCommand.Execute(null);
            }
        }
    }
}
