using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using V2EX.UWP.Helpers;
using V2EX.UWP.Models;
using V2EX.UWP.ViewModels;
using V2EX.UWP.ViewModels.Home;
using V2EX.UWP.Views.Home;
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

namespace V2EX.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel
        {
            get { return DataContext as HomeViewModel; }
        }

        public HomePage()
        {
            this.InitializeComponent();
            this.Loaded += HomePage_Loaded;
        }

        private async void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync();

            #region GridView 初始化动画
            var compositor = Window.Current.Compositor;
            var animation = compositor.CreateScalarKeyFrameAnimation();
            ElementCompositionPreview.SetIsTranslationEnabled(rootGrid, true);

            var VisaulPropertySet = ElementCompositionPreview.GetElementVisual(rootGrid).Properties;
            VisaulPropertySet.InsertVector3("Translation", Vector3.Zero);

            animation.InsertExpressionKeyFrame(0, "this.StartingValue - 120");
            animation.InsertExpressionKeyFrame(1, "this.StartingValue");
            animation.StopBehavior = AnimationStopBehavior.SetToFinalValue;
            animation.IterationBehavior = AnimationIterationBehavior.Count;
            animation.IterationCount = 1;
            animation.Duration = TimeSpan.FromSeconds(1);

            var visual = ElementCompositionPreview.GetElementVisual(rootGrid);

            visual.StopAnimation("Translation.Y");
            visual.StartAnimation("Translation.Y", animation);
            #endregion
        }
        private async void OnitemGridView_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void OnItemGridViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gridView = (GridView)sender;
            if (gridView.ItemsPanelRoot is ItemsWrapGrid wrapGrid)
            {
                if (LayoutVisualStates.CurrentState == NarrowLayout)
                {
                    wrapGrid.ItemWidth = gridView.ActualWidth - gridView.Padding.Left - gridView.Padding.Right;
                }
                else
                {
                    wrapGrid.ItemWidth = double.NaN;
                }
            }
        }

        private void OnItemGridViewItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (TopicModel)e.ClickedItem;
            var nav = ServiceLocator.Current.GetInstance<ShellViewModel>().NavigationService;
            nav.Navigate(typeof(HomeDetailViewModel).FullName, item);
        }
    }
}
