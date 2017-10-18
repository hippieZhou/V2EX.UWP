using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.UWP.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace V2EX.UWP.Services
{
    public class ExNavigationService
    {
        public event EventHandler<object> CurrentPageKeyEventHanler;

        public readonly SystemNavigationManager NavigationManager = SystemNavigationManager.GetForCurrentView();
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();

        private Frame _frame;
        public Frame Frame
        {
            get
            {
                if (_frame == null)
                    _frame = Window.Current.Content as Frame;
                return _frame;
            }
            set { _frame = value; }
        }

        public string CurrentPageKey => _pages.FirstOrDefault(p => p.Value == Frame.CurrentSourcePageType).Key;

        public bool CanGoBack => Frame.CanGoBack;

        public void GoBack()
        {
            Frame.GoBack();
            this.CurrentPageKeyEventHanler?.Invoke(this, this.CurrentPageKey);
            NavigationManager.AppViewBackButtonVisibility = CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        public void Configure(string key, Type pageType)
        {
            lock (_pages)
            {
                if (_pages.ContainsKey(key))
                {
                    throw new ArgumentException($"The key {key} is already configured in NavigationService");
                }
                if (_pages.Any(p => p.Value == pageType))
                {
                    throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == pageType).Key}");
                }
                _pages.Add(key, pageType);
            }
        }

        public bool Navigate(string pageKey, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            lock (_pages)
            {
                if (!_pages.ContainsKey(pageKey))
                {
                    throw new ArgumentException($"Page not found: {pageKey}. Did you forget to call NavigationService.Configure?", "pageKey");
                }
                var navigationResult = Frame.Navigate(_pages[pageKey], parameter, infoOverride);
                NavigationManager.AppViewBackButtonVisibility = CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
                return navigationResult;
            }
        }
    }
}
