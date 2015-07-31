using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.Common;

namespace YoYoStudio.Common.ObjectHistory
{
	public class HistoryableListProperty<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		private ObservableCollection<T> data = null;

		public HistoryableListProperty() : this(null)
		{			
		}

		public HistoryableListProperty(IEnumerable<T> list)
		{
			if (list != null)
			{
				data = new ObservableCollection<T>(list);
			}
			else
			{
				data = new ObservableCollection<T>();
			}
			data.CollectionChanged += CollectionChangedHandler;
			(data as INotifyPropertyChanged).PropertyChanged += PropertyChangedhandler;
		}

		public void AddHistory(Action undoAction, Action redoAction)
		{
			var historyContext = Singleton<HistoryContextManager>.Instance.Context;
			if (historyContext != null)
			{
				historyContext.AddHistory(undoAction, redoAction);
			}
		}

		#region ObservableCollection Members

		public void Move(int oldIndex, int newIndex)
		{
			lock (this)
			{
				AddHistory(() => data.Move(newIndex, oldIndex), () => data.Move(oldIndex, newIndex));
				data.Move(oldIndex, newIndex);
			}
		}

		#endregion

		#region IList implementations

		public int IndexOf(T item)
		{
			lock (this)
			{
				return data.IndexOf(item);
			}
		}

		public void Insert(int index, T item)
		{
			lock (this)
			{
				AddHistory(() => data.RemoveAt(index), () => data.Insert(index, item));
				data.Insert(index, item);
			}
		}

		public void RemoveAt(int index)
		{
			lock (this)
			{
				T item = data[index];
				AddHistory(() => data.Insert(index, item), () => data.RemoveAt(index));
				data.RemoveAt(index);
			}
		}

		public T this[int index]
		{
			get
			{
				lock (this)
				{
					return data[index];
				}
			}
			set
			{
				lock (this)
				{
					data[index] = value;
				}
			}
		}

		public void Add(T item)
		{
			lock (this)
			{
				AddHistory(() => data.Remove(item), () => data.Add(item));
				data.Add(item);
			}
		}

		public bool Contains(T item)
		{
			lock (this)
			{
				return data.Contains(item);
			}
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			lock (this)
			{
				data.CopyTo(array, arrayIndex);
			}
		}

		public int Count
		{
			get { lock (this) { return data.Count; } }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(T item)
		{
			lock (this)
			{
				int index = data.IndexOf(item);
				if (index > -1)
				{
					AddHistory(() => data.Insert(index, item), () => data.Remove(item));
				}
				return data.Remove(item);
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			lock (this)
			{
				return data.GetEnumerator();
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			lock (this)
			{
				return data.GetEnumerator();
			}
		}

		public void Clear()
		{
			lock (this)
			{
				if (data.Count > 0)
				{
					List<T> copy = new List<T>(data);
					AddHistory(() => { data.Clear(); copy.ForEach(c => data.Add(c)); }, () => data.Clear());
					data.Clear();
				}
			}
		}

		#endregion

		#region Notify Implementations

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		void PropertyChangedhandler(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}

		void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (CollectionChanged != null)
			{
				CollectionChanged(this, e);
			}
		}

		#endregion
	}
	
}
