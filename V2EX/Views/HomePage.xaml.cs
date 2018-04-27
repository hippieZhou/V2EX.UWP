using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using V2EX.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace V2EX.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel => DataContext as HomeViewModel;
        public HomePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeAsync();
            base.OnNavigatedTo(e);
        }
        private void OnItemGridViewContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (sender.ContainerFromItem(sender.Items.LastOrDefault()) is GridViewItem container)
            {
                container.XYFocusDown = container;
            }
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
    }
}
