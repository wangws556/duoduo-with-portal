using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using YoYoStudio.Common.Wpf.ViewModel;

namespace TestWpf
{
	public class DragThumb : Thumb
	{
		public DragThumb()
		{
			base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);

		}

		void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			
		}
	}

	public class MainViewModel : ViewModelBase
	{
		private double top;
		public double Top
		{
			get { return top; }
			set { ChangeAndNotify(ref top, value, () => Top); }
		}
		private double left;
		public double Left
		{
			get { return left; }
			set { ChangeAndNotify(ref left, value, () => Left); }
		}
		private string output;
		public string Output
		{
			get { return output; }
			set { ChangeAndNotify(ref output, value, () => Output); }
		}

		public TempViewModel TempVM { get; set; }

		public MainViewModel()
		{
			TempVM = new TempViewModel(this);
		}


	}

	public class TempViewModel : ViewModelBase
	{

		public Dispatcher Dispatcher { get; set; }
		private double top;
		public double Top
		{
			get { return top; }
			set { ChangeAndNotify(ref top, value, () => Top); }
		}
		private double left;
		public double Left
		{
			get { return left; }
			set { ChangeAndNotify(ref left, value, () => Left); }
		}

		private MainViewModel owner;

		public TempViewModel(MainViewModel main)
		{
			owner = main;
			Top = owner.Top + 100;
			Left = owner.Left + 100;
			//owner.PropertyChanged += owner_PropertyChanged;
		}

		void owner_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Top" || e.PropertyName == "Left")
			{
				Top = owner.Top + 100;
				Left = owner.Left + 10;
			}
		}

		public void SetPos(double x, double y)
		{
			Dispatcher.BeginInvoke((Action)(() =>
				{
					Left += x;
					Top += y;
				}));
		}
	}
}
