/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Xceed.Wpf.Toolkit
{
  [TemplatePart( Name = PART_ActionButton, Type = typeof( Button ) )]
  public class SplitButton : DropDownButton
  {
    private const string PART_ActionButton = "PART_ActionButton";
    private const string PART_ToggleButton = "PART_ToggleButton";
    private ToggleButton _ToggleButton = null;

    #region Constructors

    static SplitButton()
    {
      DefaultStyleKeyProperty.OverrideMetadata( typeof( SplitButton ), new FrameworkPropertyMetadata( typeof( SplitButton ) ) );
    }

    #endregion //Constructors

    #region Base Class Overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      Button = GetTemplateChild( PART_ActionButton ) as Button;
      _ToggleButton = GetTemplateChild(PART_ToggleButton) as ToggleButton;
    }

    #endregion //Base Class Overrides

    public bool PopupEnabled
    {
        get { return (bool)GetValue(PopupEnabledProperty); }
        set { SetValue(PopupEnabledProperty, value); }
    }

    // Using a DependencyProperty as the backing store for PopupEnabled.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PopupEnabledProperty =
        DependencyProperty.Register("PopupEnabled", typeof(bool), typeof(DropDownButton), new PropertyMetadata(true,
            (o, e) =>
            {
                SplitButton btn = o as SplitButton;
                if (btn != null)
                {
                    if (btn._ToggleButton != null)
                    {
                        btn._ToggleButton.IsEnabled = (bool)e.NewValue;
                    }
                }
            }));
  }
}
