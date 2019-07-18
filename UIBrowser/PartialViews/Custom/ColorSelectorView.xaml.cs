﻿using Panuon.UI.Silver;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using UIBrowser.Helpers;

namespace UIBrowser.PartialViews.Native
{
    /// <summary>
    /// ComboBoxView.xaml 的交互逻辑
    /// </summary>
    public partial class ColorSelectorView : UserControl
    {
        #region Identity
        private bool _isCodeViewing;

        private LinearGradientBrush _linearGradientBrush;
        #endregion

        public ColorSelectorView()
        {
            InitializeComponent();
            Loaded += ButtonView_Loaded;
            UpdateVisualEffect();
            _linearGradientBrush = FindResource("ColorSelectorBrush") as LinearGradientBrush;
        }

        #region Event

        private void ButtonView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTemplate();
            UpdateCode();
        }

        private void BtnViewCode_Click(object sender, RoutedEventArgs e)
        {
            if (_isCodeViewing)
            {
                if (Helper.Tier == 2)
                {
                    AnimationHelper.SetEasingFunction(GrpPalette, new CubicEase() { EasingMode = EasingMode.EaseOut });
                    AnimationHelper.SetMarginTo(GrpPalette, new Thickness(0, 0, 0, 0));
                    AnimationHelper.SetEasingFunction(GrpCode, new CubicEase() { EasingMode = EasingMode.EaseOut });
                    AnimationHelper.SetMarginTo(GrpCode, new Thickness(0, 0, 0, 0));
                }
                else
                {
                    GrpPalette.Margin = new Thickness(0, 0, 0, 0);
                    GrpCode.Margin = new Thickness(0, 0, 0, 0);
                }
                BtnViewCode.Content = Properties.Resource.ViewCode;
            }
            else
            {
                if (Helper.Tier == 2)
                {
                    AnimationHelper.SetEasingFunction(GrpPalette, new CubicEase() { EasingMode = EasingMode.EaseOut });
                    AnimationHelper.SetMarginTo(GrpPalette, new Thickness(0, -120, 0, 0));
                    AnimationHelper.SetEasingFunction(GrpCode, new CubicEase() { EasingMode = EasingMode.EaseOut });
                    AnimationHelper.SetMarginTo(GrpCode, new Thickness(0, 0, 0, -120));
                }
                else
                {
                    GrpPalette.Margin = new Thickness(0, -120, 0, 0);
                    GrpCode.Margin = new Thickness(0, 0, 0, -120);
                }
                BtnViewCode.Content = Properties.Resource.CloseCodeViewer;
            }
            _isCodeViewing = !_isCodeViewing;
        }

        private void MenuItem_CopyCode(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TbCode.Text);
        }

        private void ChbIsOpacityEnabled_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            CsCustom.IsOpacityEnabled = ChbIsOpacityEnabled.IsChecked == true;

            UpdateTemplate();
            UpdateCode();
        }

        private void ChbShowMeasuredValue_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            CsCustom.IsMeasuredValueVisible = ChbShowMeasuredValue.IsChecked == true;

            UpdateTemplate();
            UpdateCode();
        }

        private void ChbShowDefaultColorPanel_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            CsCustom.IsDefaultColorPanelVisible = ChbShowDefaultColorPanel.IsChecked == true;

            UpdateTemplate();
            UpdateCode();
        }

        #endregion

        #region Function
        private void UpdateVisualEffect()
        {
            switch (Helper.Tier)
            {
                case 1:
                case 2:
                    AnimationHelper.SetSlideInFromBottom(GrpPalette, true);
                    RectBackground.Fill = FindResource("GridBrush") as Brush;
                    GroupBoxHelper.SetEffect(GrpPalette, FindResource("DropShadow") as Effect);
                    GroupBoxHelper.SetEffect(GrpCode, FindResource("DropShadow") as Effect);
                    break;
            }
        }
        private void UpdateTemplate()
        {
            CsCustom.Height = 340 - (CsCustom.IsMeasuredValueVisible ? 0 : 50) - (CsCustom.IsDefaultColorPanelVisible ? 0 : 70);
        }

        private void UpdateCode()
        {
            var isOpacityEnabled = CsCustom.IsOpacityEnabled;
            var isShowValue = CsCustom.IsMeasuredValueVisible;
            var isDefaultPanelVisible = CsCustom.IsDefaultColorPanelVisible;

            TbCode.Text = $"<pu:ColorSelector  Width=\"{CsCustom.ActualWidth}\"" +
                        $"\nHeight=\"{CsCustom.ActualHeight}\"" +
                        (isOpacityEnabled ? "\nIsOpacityEnabled=\"True\"" : "") +
                        (isShowValue ? "\nIsMeasuredValueVisible=\"True\"" : "") +
                        (isDefaultPanelVisible ? "\nIsDefaultColorPanelVisible=\"True\"" : "") +
                        " />";
        }




        #endregion

    }
}