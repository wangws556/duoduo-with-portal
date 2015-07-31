using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unisys.CPF.Databus.Common.ObjectHistory
{
	/// <summary>
	/// This interface define the operations that support 
	/// </summary>
	public interface IHistoryableObject 
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
		//Clear the change history
		void ClearHistory();
		//Record History
		void AddHistory(Action undoAction, Action redoAction);

	}

	public class HistoryData
	{
		public Action UndoAction;
		public Action RedoAction;
	}

	/// <summary>
	/// This class supports the history operations
	/// </summary>
	public class HistoryableObject : IHistoryableObject
	{
		protected List<HistoryData> history = new List<HistoryData>();
		protected int current = -1;

		public volatile bool IsChangeTrackingEnabled;

		public virtual bool CanUndo()
		{
			lock (this)
			{
				return IsChangeTrackingEnabled && history.Count > 0 && current >= 0;
			}
		}

		public virtual bool CanRedo()
		{
			lock (this)
			{
				return IsChangeTrackingEnabled && history.Count > 0 && current < history.Count - 1;
			}
		}

		public virtual void Undo()
		{
			lock (this)
			{
				if (CanUndo())
				{
					HistoryData data = history[current];
					if (data != null && data.UndoAction != null)
					{
						data.UndoAction.Invoke();
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
					HistoryData data = history[current];
					if (data != null && data.UndoAction != null)
					{
						data.RedoAction.Invoke();
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

		public void AddHistory(Action undoAction, Action redoAction)
		{
			lock (this)
			{
				while (history.Count > current + 1)
				{
					history.RemoveAt(current + 1);
				}
				history.Add(new HistoryData(){UndoAction = undoAction,RedoAction = redoAction});
				current = history.Count - 1;
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
	}
}
