﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace V2EX.Animation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationFlowDestinationPage : Page
    {
        private Compositor _compositor;

        public NavigationFlowDestinationPage()
        {
            InitializeComponent();

            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            // Add a translation animation that will play when this element is shown
            var topBorderOffsetAnimation = _compositor.CreateScalarKeyFrameAnimation();
            topBorderOffsetAnimation.Duration = TimeSpan.FromSeconds(0.45);
            topBorderOffsetAnimation.Target = "Translation.Y";
            topBorderOffsetAnimation.InsertKeyFrame(0, -450.0f);
            topBorderOffsetAnimation.InsertKeyFrame(1, 0);

            ElementCompositionPreview.SetIsTranslationEnabled(TopBorder, true);
            // Call GetElementVisual() to work around a bug in Insider Build 15025
            ElementCompositionPreview.GetElementVisual(TopBorder);
            ElementCompositionPreview.SetImplicitShowAnimation(TopBorder, topBorderOffsetAnimation);

            // Add an opacity and translation animation that will play when this element is shown
            var mainContentTranslationAnimation = _compositor.CreateScalarKeyFrameAnimation();
            mainContentTranslationAnimation.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
            mainContentTranslationAnimation.DelayTime = TimeSpan.FromSeconds(0.2);
            mainContentTranslationAnimation.Duration = TimeSpan.FromSeconds(0.45);
            mainContentTranslationAnimation.Target = "Translation.Y";
            mainContentTranslationAnimation.InsertKeyFrame(0, 50.0f);
            mainContentTranslationAnimation.InsertKeyFrame(1, 0);

            var mainContentOpacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            mainContentOpacityAnimation.Duration = TimeSpan.FromSeconds(0.4);
            mainContentOpacityAnimation.Target = "Opacity";
            mainContentOpacityAnimation.InsertKeyFrame(0, 0);
            mainContentOpacityAnimation.InsertKeyFrame(0.25f, 0);
            mainContentOpacityAnimation.InsertKeyFrame(1, 1);

            var mainContentShowAnimations = _compositor.CreateAnimationGroup();
            mainContentShowAnimations.Add(mainContentTranslationAnimation);
            mainContentShowAnimations.Add(mainContentOpacityAnimation);

            ElementCompositionPreview.SetIsTranslationEnabled(MainContent, true);
            ElementCompositionPreview.GetElementVisual(MainContent);
            ElementCompositionPreview.SetImplicitShowAnimation(MainContent, mainContentShowAnimations);

            // Add a translation animation that will play when this element exits the scene
            var mainContentExitAnimation = _compositor.CreateScalarKeyFrameAnimation();
            mainContentExitAnimation.Target = "Translation.Y";
            mainContentExitAnimation.InsertKeyFrame(1, 30);
            mainContentExitAnimation.Duration = TimeSpan.FromSeconds(0.4);

            ElementCompositionPreview.SetIsTranslationEnabled(MainContent, true);
            ElementCompositionPreview.SetImplicitHideAnimation(MainContent, mainContentExitAnimation);

            // Add a translation animation that will play when this element exits the scene
            var topBorderExitAnimation = _compositor.CreateScalarKeyFrameAnimation();
            topBorderExitAnimation.Target = "Translation.Y";
            topBorderExitAnimation.InsertKeyFrame(1, -30);
            topBorderExitAnimation.Duration = TimeSpan.FromSeconds(0.4);

            ElementCompositionPreview.SetIsTranslationEnabled(TopBorder, true);
            ElementCompositionPreview.GetElementVisual(TopBorder);
            ElementCompositionPreview.SetImplicitHideAnimation(TopBorder, topBorderExitAnimation);

            // Add an opacity animation that will play when the page exits the scene
            var fadeOut = _compositor.CreateScalarKeyFrameAnimation();
            fadeOut.Target = "Opacity";
            fadeOut.InsertKeyFrame(1, 0);
            fadeOut.Duration = TimeSpan.FromSeconds(0.4);

            // Set Z index to force this page to the top during the hide animation
            Canvas.SetZIndex(this, 1);
            ElementCompositionPreview.GetElementVisual(this);
            ElementCompositionPreview.SetImplicitHideAnimation(this, fadeOut);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ConnectedAnimationService
                .GetForCurrentView()
                .GetAnimation("BorderSource")
                .TryStart(BorderDest, new[] { DescriptionRoot });

            ItemTextBlock.Text = $"Item {(int)e.Parameter}";
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BorderDest", BorderDest);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
