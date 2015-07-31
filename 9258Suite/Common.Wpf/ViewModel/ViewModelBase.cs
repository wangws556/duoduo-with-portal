using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using YoYoStudio.Common.Extensions;
using System.Runtime.InteropServices;
using YoYoStudio.Common.ObjectHistory;

namespace YoYoStudio.Common.Wpf.ViewModel
{
    [ComVisible(true)]
    [Serializable]
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region INotifyPropertyChanged Implementation

		public void ChangeAndNotify<T>(ref T field, T value, Expression<Func<T>> memberExpression)
		{
			PropertyChanged.ChangeAndNotify<T>(this, ref field, value, memberExpression);            
			Dirty = true;
		}

		public void ChangeAndNotifyHistory<T>(HistoryableProperty<T> field, T value, Expression<Func<T>> memberExpression)
		{
			PropertyChanged.ChangeAndNotifyHistory<T>(this, field, value, memberExpression);
			Dirty = true;
		}

		public void Notify<T>(Expression<Func<T>> memberExpression)
		{
			PropertyChanged.Notify<T>(this, memberExpression);
		}

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

		private bool dirty = false;

		/// <summary>
		/// Gets / sets the Title value
		/// </summary>
		public virtual bool Dirty
		{
			get { return dirty; }
			set
			{
				if (dirty != value)
				{
					dirty = value;
					Notify<bool>(() => Dirty);
				}
			}
		}

        
        #region IDisposable Implemenation 

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    ReleaseManagedResource();
                }
                ReleaseUnManagedResource();
                disposed = true;
            }
        }

        protected virtual void ReleaseManagedResource()
        {
        }

        protected virtual void ReleaseUnManagedResource()
        {
            
        }

        //public void View_Hide()
        //{
        //    if (View != null)
        //    {
        //        DispatcherInvoker(() => View.Hide(), true);
        //    }
        //}

        //public void View_ShowDialog()
        //{
        //    if (View != null)
        //    {
        //        DispatcherInvoker(() => View.ShowDialog(), true);
        //    }
        //}

        //public void View_Show()
        //{
        //    if (View != null)
        //    {
        //        DispatcherInvoker(() => View.Show(), true);
        //    }
        //}

        //public void View_Close()
        //{
        //    if (View != null)
        //    {
        //        DispatcherInvoker(() => View.Close(), true);
        //    }
        //}

        ~ViewModelBase()
        {
            Dispose(false);
        }

        #endregion

        public void CallJavaScript(string functionName, params object[] args)
        {
            if (View != null)
            {
                View.CallJavaScript(functionName, args);
            }
        }

        public void DispatcherInvoker(Action act, bool async = false)
        {
            //var disp = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            var disp = System.Windows.Application.Current.Dispatcher;
            if (disp != null)
            {
                if (async)
                {
                    disp.BeginInvoke((Action)(() => act()));
                }
                else
                {
                    disp.Invoke((Action)(() => act()));
                }
            }
            else
            {
                act();
            }
        }

        public ViewModelBase()
        {
            
        }

        public virtual void Initialize()
        {
            InitializeResource();
            InitializeCommand();
        }

		protected virtual void InitializeResource()
		{
		}

        protected virtual void InitializeCommand()
        {
        }

        public virtual void Save()
        {
			Dirty = false;
        }

        public virtual void Reset()
        {
			Dirty = false;
        }

        public IView View { get; set; }
    }
}
