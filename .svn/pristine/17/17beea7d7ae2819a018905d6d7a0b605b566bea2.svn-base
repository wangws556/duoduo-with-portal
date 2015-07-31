using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace YoYoStudio.Common.Wpf
{
	/// <summary>
	/// The VisualWrapper simply integrates a raw Visual child into a tree of Framework Elements
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[ContentProperty("Child")]
	public class VisualWrapper<T> :FrameworkElement where T:Visual
	{
		protected T child = null;
		public T Child
		{
			get
			{
				return child;
			}
			set
			{
				if (child != null)
				{
					RemoveVisualChild(child);
				}
				child = value;
				if (child != null)
				{
					AddVisualChild(child);
				}
			}
		}

		protected override Visual GetVisualChild(int index)
		{
			return child;
		}

		protected override int VisualChildrenCount
		{
			get
			{
				return child == null ? 0 : 1;
			}
		}
	}

	public class VisualWrapper : VisualWrapper<Visual>
	{
	}
}
