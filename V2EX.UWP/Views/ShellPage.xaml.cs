using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using V2EX.UWP.Helpers;
using V2EX.UWP.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace V2EX.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel
        {
            get { return DataContext as ShellViewModel; }
        }
        public ShellPage()
        {
            this.InitializeComponent();
            ViewModel.Initialize(this.contentFrame);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            #region SetupTitleBar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonForegroundColor = Colors.Black;
                    titleBar.ButtonBackgroundColor = Colors.Transparent;
                    titleBar.BackgroundColor = Colors.Transparent;
                    titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

                    CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                }
            }
            #endregion

            #region 启动动画
            // Set a fade in animation when this page enters the scene
            var _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var fadeInAnimation = _compositor.CreateScalarKeyFrameAnimation();
            fadeInAnimation.Target = "Opacity";
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.3);
            fadeInAnimation.InsertKeyFrame(0, 0);
            fadeInAnimation.InsertKeyFrame(1, 1);

            ElementCompositionPreview.GetElementVisual(this);
            ElementCompositionPreview.SetImplicitShowAnimation(this, fadeInAnimation);

            // Set a fade out animation when this page exits the scene
            var fadeOutAnimation = _compositor.CreateScalarKeyFrameAnimation();
            fadeOutAnimation.Target = "Opacity";
            fadeOutAnimation.Duration = TimeSpan.FromSeconds(0.3);
            fadeOutAnimation.InsertKeyFrame(0, 1);
            fadeOutAnimation.InsertKeyFrame(1, 0);

            ElementCompositionPreview.SetImplicitHideAnimation(this, fadeOutAnimation);
            #endregion
        }
    }
}
