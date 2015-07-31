using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoYoStudio.Common.ObjectHistory
{
	public interface IUndoRedo
	{
		//if it is undoable
		bool CanUndo();
		//if it is redoable
		bool CanRedo();
		//Undo the last change, return the current value(the one before undo)
		void Undo();
		//Undo all the changes
		void UndoAll();
		//Redo the last undo change, return the current value(the one before redo)
		void Redo();
		//Redo all the changes
		void RedoAll();
	}

	public class HistoryData
	{
		public Action UndoAction;
		public Action RedoAction;
	}

	public interface IHistoryableContext : IUndoRedo
	{
		void AddHistory(Action undoAction, Action redoAction);
		void ClearHistory();
		void StartTracking();
		void EndTracking();
	}

	public class HistoryableContext : IHistoryableContext
	{
		private List<List<HistoryData>> history = new List<List<HistoryData>>();
		private List<HistoryData> tempHistory = new List<HistoryData>();

		protected int current = -1;
		private int depth = 0;

		public virtual bool CanUndo()
		{
			lock (this)
			{
				return history.Count > 0 && current >= 0;
			}
		}

		public virtual bool CanRedo()
		{
			lock (this)
			{
				return history.Count > 0 && current < history.Count - 1;
			}
		}

		public virtual void Undo()
		{
			lock (this)
			{
				if (CanUndo())
				{
					List<HistoryData> datas = history[current];
					if (datas != null && datas.Count > 0)
					{
						for (int i = datas.Count - 1; i >= 0; --i)
						{
							datas[i].UndoAction.Invoke();
						}
					}
					current--;
				}
			}
		}

		public virtual void Redo()
		{
			lock (this)
			{
				if (CanRedo())
				{
					current++;
					List<HistoryData> datas = history[current];
					if (datas != null && datas.Count > 0)
					{
						foreach (var data in datas)
						{
							data.RedoAction.Invoke();
						}
					}
				}
			}
		}

		public virtual void ClearHistory()
		{
			lock (this)
			{
				history.Clear();
				current = -1;
			}
		}

		public void UndoAll()
		{
			while (CanUndo())
			{
				Undo();
			}
		}

		public void RedoAll()
		{
			while (CanRedo())
			{
				Redo();
			}
		}

		public void AddHistory(Action undoAction, Action redoAction)
		{
			lock (this)
			{
				tempHistory.Add(new HistoryData() { UndoAction = undoAction, RedoAction = redoAction });
			}
		}

		public void CommitHistory()
		{
			while (history.Count > current + 1)
			{
				history.RemoveAt(current + 1);
			}
			history.Add(tempHistory);
			current = history.Count - 1;

			tempHistory = new List<HistoryData>();
		}

		public void StartTracking()
		{
			lock (this)
			{
				if (depth == 0)
				{
					Singleton<HistoryContextManager>.Instance.Context = this;
				}
				++depth;
			}
		}

		public void EndTracking()
		{
			lock (this)
			{
				--depth;
				if (depth == 0)
				{
					Singleton<HistoryContextManager>.Instance.Context = null;

					CommitHistory();
				}
			}
		}
	}

	public class HistoryContextManager
	{
		private HistoryContextManager()
		{
		}

		private IHistoryableContext context;
		public IHistoryableContext Context
		{
			get
			{
				return context;
			}
			set
			{
				if (context != null && value != null)
				{
					throw new Exception("Only one history context can be set at one time!");
				}
				context = value;
			}
		}
	}

	public class HistoryContextScope : IDisposable
	{
		private IHistoryableContext context;

		public HistoryContextScope(IHistoryableContext context)
		{
			this.context = context;
			this.context.StartTracking();
		}
		public void Dispose()
		{
			this.context.EndTracking();
		}
	}
}
