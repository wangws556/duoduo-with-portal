using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;

namespace YoYoStudio.Controls.CustomWindow
{
    public abstract class ParentWindow<ActionType> : EssentialWindow<ActionType> where ActionType :struct
    {
        public ParentWindow()
        {
        }

        AutoResetEvent reset = new AutoResetEvent(false);


        protected ChildWindow<ChildActionType> CreateWindowInSeparateThread<ChildActionType>(Func<ChildWindow<ChildActionType>> createFunc, double x, double y, bool isModal, bool isEmbedded, Control replicatedControl = null) where ChildActionType : struct
        {
            double top = Top;
            double left = Left;
            
            ChildWindow<ChildActionType> window = null;
            Thread newWindowThread = new Thread(new ThreadStart(()=>
            {
                window = createFunc();
                reset.Set();
                window.OffsetX = x;
                window.OffsetY = y;
                window.ReplicatedControl = replicatedControl;
                window.Top = top + window.OffsetY;
                window.Left = left + window.OffsetX;
                window.Topmost = true;
                window.SetParent<ActionType>(this,isEmbedded);
                if (isModal)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
                System.Windows.Threading.Dispatcher.Run();
            }));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
            reset.WaitOne();
            return window;
        }

        public event Action<double, double> PositionChanged;

        protected void RaisePositionChanged(double xDelta, double yDelta)
        {
            if (PositionChanged != null)
            {
                PositionChanged(xDelta, yDelta);
            }
        }
    }
}
