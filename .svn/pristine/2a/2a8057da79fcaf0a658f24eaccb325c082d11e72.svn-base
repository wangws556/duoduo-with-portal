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
using System.Windows.Shapes;
using YoYoStudio.Model;

namespace TestWpf
{
    /// <summary>
    /// Interaction logic for AWindow.xaml
    /// </summary>
    public partial class AWindow : Window
    {
		private string root = string.Empty;
		private string flexFile = string.Empty;
        public AWindow(TempViewModel tempVM)
        {
            InitializeComponent();
            Loaded += AWindow_Loaded;
			root = Environment.CurrentDirectory;
			Topmost = true;
			flexFile = System.IO.Path.Combine(root, Const.FlexFile);
			DataContext = tempVM;
			tempVM.Dispatcher = Dispatcher;
			//tempVM.PropertyChanged += tempVM_PropertyChanged;
        }

		void tempVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			Dispatcher.BeginInvoke((Action)(() =>
				{
					var vm = DataContext as TempViewModel;
					Left = vm.Left;
					Top = vm.Top;
				}));
		}

        void AWindow_Loaded(object sender, RoutedEventArgs e)
        {
			//flex.MoviePath = flexFile;
			//flex.LoadFlex();
        }
    }
}
