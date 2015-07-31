using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace YoYoStudio.CaptureScreen
{

    public class MaskCanvas : Canvas
    {
        private readonly DockPanel m_dockPanel = new DockPanel();
        
        public MaskCanvas()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //make the render effect same as SnapsToDevicePixels
            //"SnapsToDevicePixels = true;" doesn't work on "OnRender"
            //however, this maybe make some offset form the render target's origin location
            //SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            //ini this
            Cursor = BitmapCursor.CreateCrossCursor();
            Background = Brushes.Transparent;



            //ini mask rect
            maskRectLeft.Fill = maskRectRight.Fill = maskRectTop.Fill = maskRectBottom.Fill = Config.MaskWindowBackground;

            //these propeties(x, y...) will not changed
            SetLeft(maskRectLeft, 0);
            SetTop(maskRectLeft, 0);
            SetRight(maskRectRight, 0);
            SetTop(maskRectRight, 0);
            SetTop(maskRectTop, 0);
            SetBottom(maskRectBottom, 0);
            maskRectLeft.Height = ActualHeight;


            Children.Add(maskRectLeft);
            Children.Add(maskRectRight);
            Children.Add(maskRectTop);
            Children.Add(maskRectBottom);

            //ini selection border
            selectionBorder.Stroke = Config.SelectionBorderBrush;
            selectionBorder.StrokeThickness = Config.SelectionBorderThickness.Left;
            selectionBorder.StrokeDashArray = new DoubleCollection(new Double[] { 5, 2 });

            Children.Add(selectionBorder);

            //ini indicator
            indicator = new IndicatorObject(this);
            Children.Add(indicator);

            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        private void UpdateSelectionBorderLayout()
        {
            if (!selectionRegion.IsEmpty)
            {
                SetLeft(selectionBorder, selectionRegion.Left);
                SetTop(selectionBorder, selectionRegion.Top);
                selectionBorder.Width = selectionRegion.Width;
                selectionBorder.Height = selectionRegion.Height;
            }
        }

        private void UpdateMaskRectanglesLayout()
        {
            var actualHeight = ActualHeight;
            var actualWidth = ActualWidth;

            if (selectionRegion.IsEmpty)
            {
                SetLeft(maskRectLeft, 0);
                SetTop(maskRectLeft, 0);
                maskRectLeft.Width = actualWidth;
                maskRectLeft.Height = actualHeight;

                maskRectRight.Width = maskRectRight.Height = maskRectTop.Width = maskRectTop.Height = maskRectBottom.Width = maskRectBottom.Height = 0;
            }
            else
            {
                maskRectLeft.StrokeDashArray = new DoubleCollection(new Double[] { 3, 5 });
                var temp = selectionRegion.Left;
                if (maskRectLeft.Width != temp)
                {
                    maskRectLeft.Width = temp < 0 ? 0 : temp; //Math.Max(0, selectionRegion.Left);
                }

                temp = ActualWidth - selectionRegion.Right;
                if (maskRectRight.Width != temp)
                {
                    maskRectRight.Width = temp < 0 ? 0 : temp; //Math.Max(0, ActualWidth - selectionRegion.Right);
                }

                if (maskRectRight.Height != actualHeight)
                {
                    maskRectRight.Height = actualHeight;
                }

                SetLeft(maskRectTop, maskRectLeft.Width);
                SetLeft(maskRectBottom, maskRectLeft.Width);

                temp = actualWidth - maskRectLeft.Width - maskRectRight.Width;
                if (maskRectTop.Width != temp)
                {
                    maskRectTop.Width = temp < 0 ? 0 : temp; //Math.Max(0, ActualWidth - maskRectLeft.Width - maskRectRight.Width);
                }

                temp = selectionRegion.Top;
                if (maskRectTop.Height != temp)
                {
                    maskRectTop.Height = temp < 0 ? 0 : temp; //Math.Max(0, selectionRegion.Top);
                }

                maskRectBottom.Width = maskRectTop.Width;

                temp = actualHeight - selectionRegion.Bottom;
                if (maskRectBottom.Height != temp)
                {
                    maskRectBottom.Height = temp < 0 ? 0 : temp; //Math.Max(0, ActualHeight - selectionRegion.Bottom);
                }
            }
        }


        #region Fileds & Props

        private IndicatorObject indicator;
        private Point? selectionStartPoint;
        private Point? selectionEndPoint;
        private Rect selectionRegion = Rect.Empty;
        private bool isMaskDraging;

        private readonly System.Windows.Shapes.Rectangle selectionBorder = new System.Windows.Shapes.Rectangle();

        private readonly System.Windows.Shapes.Rectangle maskRectLeft = new System.Windows.Shapes.Rectangle();
        private readonly System.Windows.Shapes.Rectangle maskRectRight = new System.Windows.Shapes.Rectangle();
        private readonly System.Windows.Shapes.Rectangle maskRectTop = new System.Windows.Shapes.Rectangle();
        private readonly System.Windows.Shapes.Rectangle maskRectBottom = new System.Windows.Shapes.Rectangle();


        public Size? DefaultSize
        {
            get;
            set;
        }


        public MaskWindow MaskWindowOwner
        {
            get;
            set;
        }

        #endregion

        #region Mouse Managment

        private bool IsMouseOnThis(RoutedEventArgs e)
        {
            return e.Source.Equals(this) || e.Source.Equals(maskRectLeft) || e.Source.Equals(maskRectRight) || e.Source.Equals(maskRectTop) || e.Source.Equals(maskRectBottom);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            //mouse down on this self
            if (IsMouseOnThis(e))
            {
                PrepareShowMask(Mouse.GetPosition(this));
            }
            //mouse down on indicator
            else if (e.Source.Equals(indicator))
            {
                HandleIndicatorMouseDown(e);
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseOnThis(e))
            {
                UpdateSelectionRegion(e, UpdateMaskType.ForMouseMoving);

                e.Handled = true;
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsMouseOnThis(e))
            {
                UpdateSelectionRegion(e, UpdateMaskType.ForMouseLeftButtonUp);
                FinishShowMask();
            }
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            indicator.Visibility = Visibility.Collapsed;
            selectionRegion = Rect.Empty;
            selectionBorder.Width = selectionBorder.Height = 0;
            ClearSelectionData();
            UpdateMaskRectanglesLayout();

            base.OnMouseRightButtonUp(e);
        }


        internal void HandleIndicatorMouseDown(MouseButtonEventArgs e)
        {
            if(e.ClickCount>=2)
            {
                if(MaskWindowOwner != null)
                {
                    MaskWindowOwner.ClipSnapshot(GetIndicatorRegion());
                    ClearSelectionData();
                }
            }
        }

        private void PrepareShowMask(Point mouseLoc)
        {
            indicator.Visibility = Visibility.Collapsed;
            selectionBorder.Visibility = Visibility.Visible;
            selectionStartPoint = new Point?(mouseLoc);

            if(!IsMouseCaptured)
            {
                CaptureMouse();
            }
        }


        private void UpdateSelectionRegion(MouseEventArgs e, UpdateMaskType updateType)
        {
            if (updateType == UpdateMaskType.ForMouseMoving && e.LeftButton != MouseButtonState.Pressed)
            {
                selectionStartPoint = null;
            }

            if (selectionStartPoint.HasValue )
            {
                selectionEndPoint = e.GetPosition(this);

                var startPoint = (Point) selectionEndPoint;
                var endPoint = (Point) selectionStartPoint;
                var sX = startPoint.X;
                var sY = startPoint.Y;
                var eX = endPoint.X;
                var eY = endPoint.Y;

                var deltaX = eX - sX;
                var deltaY = eY - sY;

                if (Math.Abs(deltaX) >= SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(deltaX) >= SystemParameters.MinimumVerticalDragDistance)
                {
                    isMaskDraging = true;

                    double x = sX < eX ? sX : eX;//Math.Min(sX, eX);
                    double y = sY < eY ? sY : eY;//Math.Min(sY, eY);
                    double w = deltaX < 0 ? -deltaX : deltaX;//Math.Abs(deltaX);
                    double h = deltaY < 0 ? -deltaY : deltaY;//Math.Abs(deltaY);

                    selectionRegion = new Rect(x, y, w, h);
                }
                else
                {
                    if (DefaultSize.HasValue && updateType == UpdateMaskType.ForMouseLeftButtonUp)
                    {
                        isMaskDraging = true;
                        
                        selectionRegion = new Rect(startPoint.X, startPoint.Y, DefaultSize.Value.Width, DefaultSize.Value.Height);
                    }
                    else
                    {
                        isMaskDraging = false;
                    }
                }
            }
        }

        internal void UpdateSelectionRegion(Rect region)
        {
            selectionRegion = region;
        }


        private void FinishShowMask()
        {
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }

            if (isMaskDraging)
            {
                if (MaskWindowOwner != null)
                {
                    MaskWindowOwner.OnShowMaskFinished(selectionRegion);
                }

                UpdateIndicator(selectionRegion);

                CreateDockPanel();

                ClearSelectionData();
            }
        }

        private void CreateDockPanel()
        {
            Button okBtn = new Button();
            okBtn.Content = "确定";
            Button cancelBtn = new Button();
            cancelBtn.Content = "取消";

            okBtn.SetValue(DockPanel.DockProperty, Dock.Right);
            okBtn.Height = 30;
            okBtn.Width = 40;
            okBtn.Padding = new Thickness(2);
            okBtn.Background = Brushes.LightGreen;
            okBtn.Click += new RoutedEventHandler(okBtn_Click);
            m_dockPanel.Children.Add(okBtn);

            cancelBtn.SetValue(DockPanel.DockProperty, Dock.Left);
            cancelBtn.Height = 30;
            cancelBtn.Width = 50;
            cancelBtn.Padding = new Thickness(2);
            cancelBtn.Background = Brushes.LightGreen;
            cancelBtn.Click += new RoutedEventHandler(cancelBtn_Click);
            m_dockPanel.Children.Add(cancelBtn);

            m_dockPanel.Margin = new Thickness(5);

            m_dockPanel.SetValue(TopProperty, Mouse.GetPosition(this).Y);
            m_dockPanel.SetValue(LeftProperty, Mouse.GetPosition(this).X - 90);

            this.Children.Add(m_dockPanel);

            Cursor = Cursors.Arrow;
        }

        void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MaskWindowOwner != null)
            {
                MaskWindowOwner.CancelCaputre();
            }
        }

        void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MaskWindowOwner != null)
            {
                MaskWindowOwner.ClipSnapshot(GetIndicatorRegion());
                ClearSelectionData();
            }
        }

        private void ClearSelectionData()
        {
            isMaskDraging = false;
            selectionStartPoint = null;
            selectionEndPoint = null;
        }

        private void UpdateIndicator(Rect region)
        {
            if(region.Width<indicator.MinWidth || region.Height<indicator.MinHeight)
            {
                return;
            }

            indicator.Width = region.Width;
            indicator.Height = region.Height;
            SetLeft(indicator, region.Left);
            SetTop(indicator, region.Top);

            indicator.Visibility = Visibility.Visible;
        }

        private Rect GetIndicatorRegion()
        {
            return new Rect(GetLeft(indicator), GetTop(indicator), indicator.ActualWidth, indicator.ActualHeight);
        }

        #endregion

        #region Render

        private void OnCompositionTargetRendering(object sender, EventArgs e)
        {
            UpdateSelectionBorderLayout();
            UpdateMaskRectanglesLayout();
        }

        #endregion

        #region inner types

        private enum UpdateMaskType
        {
            ForMouseMoving,
            ForMouseLeftButtonUp
        }

        #endregion

    }
}
