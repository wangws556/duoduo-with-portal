/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace YoYoStudio.Common.Wpf
{
	public static class Helper
	{

		/// <summary>
		/// Finds a parent of a given item on the visual tree.
		/// </summary>
		/// <typeparam name="T">The type of the queried item.</typeparam>
		/// <param name="child">A direct or indirect child of the queried item.</param>
		/// <returns>The first parent item that matches the submitted type parameter. 
		/// If not matching item can be found, a null reference is being returned.</returns>
		public static T FindVisualParent<T>(DependencyObject child)
		  where T : DependencyObject
		{
			// get parent item
			DependencyObject parentObject = VisualTreeHelper.GetParent(child);

			// we’ve reached the end of the tree
			if (parentObject == null) return null;

			// check if the parent matches the type we’re looking for
			T parent = parentObject as T;
			if (parent != null)
			{
				return parent;
			}
			else
			{
				// use recursion to proceed with next level
				return FindVisualParent<T>(parentObject);
			}
		}

		/// <summary>
		/// Finds a parent of a given item on the logic tree.
		/// </summary>
		/// <typeparam name="T">The type of the queried item.</typeparam>
		/// <param name="child">A direct or indirect child of the queried item.</param>
		/// <returns>The first parent item that matches the submitted type parameter. 
		/// If not matching item can be found, a null reference is being returned.</returns>
		public static T FindLogicParent<T>(DependencyObject child)
		  where T : DependencyObject
		{
			// get parent item
			DependencyObject parentObject = LogicalTreeHelper.GetParent(child);

			// we’ve reached the end of the tree
			if (parentObject == null) return null;

			// check if the parent matches the type we’re looking for
			T parent = parentObject as T;
			if (parent != null)
			{
				return parent;
			}
			else
			{
				// use recursion to proceed with next level
				return FindLogicParent<T>(parentObject);
			}
		}

        public static T FindChild<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T found = default(T);
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                found = child as T;
                if (found == null)
                {
                    found = FindChild<T>(child, name);
                    if (found != null)
                    {
                        break;
                    }
                }
                else if (!string.IsNullOrEmpty(name))
                {
                    var frameworkElement = found as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == name)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return found;
        }

		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						yield return (T)child;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child))
					{
						yield return childOfChild;
					}
				}
			}
		}
		
		public static bool IsMultipleSelect()
		{
			return (Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None;
		}

		public static bool DoesItemExists(ItemsControl itemsControl, object item)
		{
			if (itemsControl.Items.Count > 0)
			{
				return itemsControl.Items.Contains(item);
			}
			return false;
		}

		public static void AddItem(ItemsControl itemsControl, object item, int insertIndex)
		{
			if (itemsControl.ItemsSource != null)
			{
				IList iList = itemsControl.ItemsSource as IList;
				if (iList != null)
				{
					iList.Insert(insertIndex, item);
				}
				else
				{
					Type type = itemsControl.ItemsSource.GetType();
					Type genericList = type.GetInterface("IList`1");
					if (genericList != null)
					{
						type.GetMethod("Insert").Invoke(itemsControl.ItemsSource, new object[] { insertIndex, item });
					}
				}
			}
			else
			{
				itemsControl.Items.Insert(insertIndex, item);
			}
		}

		public static void RemoveItem(ItemsControl itemsControl, object itemToRemove)
		{
			if (itemToRemove != null)
			{
				int index = itemsControl.Items.IndexOf(itemToRemove);
				if (index != -1)
				{
					RemoveItem(itemsControl, index);
				}
			}
		}

		public static void RemoveItem(ItemsControl itemsControl, int removeIndex)
		{
			if (removeIndex != -1 && removeIndex < itemsControl.Items.Count)
			{
				if (itemsControl.ItemsSource != null)
				{
					IList iList = itemsControl.ItemsSource as IList;
					if (iList != null)
					{
						iList.RemoveAt(removeIndex);
					}
					else
					{
						Type type = itemsControl.ItemsSource.GetType();
						Type genericList = type.GetInterface("IList`1");
						if (genericList != null)
						{
							type.GetMethod("RemoveAt").Invoke(itemsControl.ItemsSource, new object[] { removeIndex });
						}
					}
				}
				else
				{
					itemsControl.Items.RemoveAt(removeIndex);
				}
			}
		}

		public static object GetDataObjectFromItemsControl(ItemsControl itemsControl, Point p, Type targetType)
		{
			if (itemsControl.HasItems)
			{
				IInputElement element = itemsControl.InputHitTest(p);
				while (element != null)
				{
					if (element == itemsControl)
						return null;					

					if (element is FrameworkElement)
					{
						FrameworkElement fElement = element as FrameworkElement;
						if (fElement.GetType() == targetType)
						{
							return fElement.DataContext;
						}
						else
						{
							element = VisualTreeHelper.GetParent(element as DependencyObject) as FrameworkElement;
						}
					}
					else if (element is FrameworkContentElement)
					{
						element = (element as FrameworkContentElement).Parent as IInputElement;
					}
					else if (element is DependencyObject)
					{
						element = VisualTreeHelper.GetParent(element as DependencyObject) as FrameworkElement;
					}

					//object data = itemsControl.ItemContainerGenerator.ItemFromContainer(element);
					//if (data != DependencyProperty.UnsetValue)
					//{
					//	return data;
					//}
					//else
					//{
					//	element = VisualTreeHelper.GetParent(element) as UIElement;
					//}
				}
			}

			return null;
		}

		public static UIElement GetItemContainerFromPoint(ItemsControl itemsControl, Point p)
		{
			UIElement element = itemsControl.InputHitTest(p) as UIElement;
			while (element != null)
			{
				if (element == itemsControl)
					return null;

				object data = itemsControl.ItemContainerGenerator.ItemFromContainer(element);
				if (data != DependencyProperty.UnsetValue)
				{
					return element;
				}
				else
				{
					element = VisualTreeHelper.GetParent(element) as UIElement;
				}
			}
			return null;
		}

		public static UIElement GetItemContainerFromItemsControl(ItemsControl itemsControl)
		{
			UIElement container = null;
			if (itemsControl != null && itemsControl.Items.Count > 0)
			{
				container = itemsControl.ItemContainerGenerator.ContainerFromIndex(0) as UIElement;
			}
			else
			{
				container = itemsControl;
			}
			return container;
		}

		public static bool IsPointInTopHalf(ItemsControl itemsControl, DragEventArgs e)
		{
			UIElement selectedItemContainer = GetItemContainerFromPoint(itemsControl, e.GetPosition(itemsControl));
			Point relativePosition = e.GetPosition(selectedItemContainer);
			if (IsItemControlOrientationHorizontal(itemsControl))
			{
				return relativePosition.Y < ((FrameworkElement)selectedItemContainer).ActualHeight / 2;
			}
			return relativePosition.X < ((FrameworkElement)selectedItemContainer).ActualWidth / 2;
		}

		public static bool IsItemControlOrientationHorizontal(ItemsControl itemsControl)
		{
			if (itemsControl is TabControl)
				return false;
			return true;
		}

		public static bool? IsMousePointerAtTop(ItemsControl itemsControl, Point pt)
		{
			if (pt.Y > 0.0 && pt.Y < 25)
			{
				return true;
			}
			else if (pt.Y > itemsControl.ActualHeight - 25 && pt.Y < itemsControl.ActualHeight)
			{
				return false;
			}
			return null;
		}

		
		
	}
}
