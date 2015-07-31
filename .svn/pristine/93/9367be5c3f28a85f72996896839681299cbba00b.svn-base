using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace YoYoStudio.Common.Wpf.MultipleUIThreading
{
	/// <summary>
	/// The VisualTargetPresentationSource represents the root of a visual subtree owned by
	/// a different thread that the visual tree in which it is displayed.
	/// </summary>
	/// <remarks>
	///	A HostVisual belongs to the same UI thread that owns the visual tree in which it resides
	///
	/// A HostVisual can reference a VisualTarget owned by another thread.
	/// 
	/// A VisualTarget has a root visual
	/// 
	/// VisualTargetPresentationSource wraps the VisualTarget and enables basic functionality
	/// like loaded, which depends on a PresentationSource begin available.
	/// </remarks>
	public class VisualTargetPresentationSource : PresentationSource
	{
		protected VisualTarget visualTarget = null;
		protected object dataContext = null;

		public VisualTargetPresentationSource(HostVisual hostVisual)
		{
			visualTarget = new VisualTarget(hostVisual);
		}

		public event SizeChangedEventHandler SizeChanged;		

		protected override CompositionTarget GetCompositionTargetCore()
		{
			return visualTarget;
		}

		public override bool IsDisposed
		{
			get { return false; }
		}

		public override System.Windows.Media.Visual RootVisual
		{
			get
			{
				return visualTarget.RootVisual;
			}
			set
			{
				Visual oldRoot = visualTarget.RootVisual;
				if (oldRoot != null)
				{
					FrameworkElement oldFrameworkElement = oldRoot as FrameworkElement;
					if (oldFrameworkElement != null)
					{
						oldFrameworkElement.DataContext = null;
						oldFrameworkElement.SizeChanged -= frameworkElement_SizeChanged;
					}
				}


				//set the root visual of the VisualTarget, this visual will now be used to visually compose the scene.
				visualTarget.RootVisual = value;
				
				//Hook the SizeChanged event on frameworkelement for all future changed to the layout size of our root,
				//and manually trigger a size change
				FrameworkElement frameworkElement = value as FrameworkElement;
				if (frameworkElement != null)
				{
					frameworkElement.DataContext = DataContext;
					frameworkElement.SizeChanged += frameworkElement_SizeChanged;
				}

				//Tell the PresentationSource that the root visual has changed.
				//This will kick off a bunch of stuff like the loaded event.
				RootChanged(oldRoot, value);

				//Kickoff layout ...
				UIElement uiElement = value as UIElement;
				if (uiElement != null)
				{
					uiElement.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
					uiElement.Arrange(new Rect(uiElement.DesiredSize));
				}
			}
		}

		void frameworkElement_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (SizeChanged != null)
			{
				SizeChanged(this, e);
			}
		}

		public object DataContext
		{
			get { return dataContext; }
			set
			{
				dataContext = value;
				var rootElement = visualTarget.RootVisual as FrameworkElement;
				if (rootElement != null)
				{
					rootElement.DataContext = value;
				}
			}
		}
	}
}
