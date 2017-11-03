using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace V2EX.UWP.Services
{
    //public class ExNavigationService
    //{
    //    public event EventHandler<object> CurrentPageKeyEventHanler;

    //    public readonly SystemNavigationManager NavigationManager = SystemNavigationManager.GetForCurrentView();
    //    private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();

    //    public ExNavigationService()
    //    {
    //        NavigationManager.BackRequested += NavigationManager_BackRequested;
    //    }

    //    private Frame _frame;
    //    public Frame Frame
    //    {
    //        get
    //        {
    //            if (_frame == null)
    //                _frame = Window.Current.Content as Frame;
    //            return _frame;
    //        }
    //        set { _frame = value; }
    //    }

    //    public bool CanGoBack => Frame.CanGoBack;

    //    public void GoBack()
    //    {
    //        Frame.GoBack();
    //        Trace.WriteLine(Frame.CurrentSourcePageType);

    //        var currentPageKey = _pages.FirstOrDefault(p => p.Value == Frame.CurrentSourcePageType).Key;
    //        this.CurrentPageKeyEventHanler?.Invoke(this, currentPageKey);

    //        NavigationManager.AppViewBackButtonVisibility = CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
    //    }

    //    public void Configure(string key, Type pageType)
    //    {
    //        lock (_pages)
    //        {
    //            if (_pages.ContainsKey(key))
    //            {
    //                throw new ArgumentException($"The key {key} is already configured in NavigationService");
    //            }
    //            if (_pages.Any(p => p.Value == pageType))
    //            {
    //                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == pageType).Key}");
    //            }
    //            _pages.Add(key, pageType);
    //        }
    //    }

    //    public bool Navigate(string pageKey, object parameter = null, NavigationTransitionInfo infoOverride = null)
    //    {
    //        lock (_pages)
    //        {
    //            if (!_pages.ContainsKey(pageKey))
    //            {
    //                throw new ArgumentException($"Page not found: {pageKey}. Did you forget to call NavigationService.Configure?", "pageKey");
    //            }
    //            var navigationResult = Frame.Navigate(_pages[pageKey], parameter, infoOverride);
    //            NavigationManager.AppViewBackButtonVisibility = CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
    //            return navigationResult;
    //        }
    //    }

    //    private void NavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
    //    {
    //        if (this.CanGoBack)
    //        {
    //            this.GoBack();
    //            e.Handled = true;
    //        }
    //    }
    //}

    public class ExNavigationService
    {
        public event NavigatedEventHandler Navigated;

        public event NavigationFailedEventHandler NavigationFailed;

        public readonly SystemNavigationManager NavigationManager = SystemNavigationManager.GetForCurrentView();

        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();

        public ExNavigationService()
        {
            NavigationManager.BackRequested += NavigationManager_BackRequested;
        }

        private Frame _frame;
        public Frame Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = Window.Current.Content as Frame;
                    RegisterFrameEvents();
                }
                return _frame;
            }
            set
            {
                UnregisterFrameEvents();
                _frame = value;
                RegisterFrameEvents();
            }
        }

        private void UnregisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated -= Frame_Navigated;
                _frame.NavigationFailed -= Frame_NavigationFailed;
            }
        }

        private void RegisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated += Frame_Navigated;
                _frame.NavigationFailed += Frame_NavigationFailed;
            }
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e) => NavigationFailed?.Invoke(sender, e);

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationManager.AppViewBackButtonVisibility = CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            Navigated?.Invoke(sender, e);
        }

        public string CurrentPageKey => _pages.FirstOrDefault(p => p.Value.FullName == Frame.CurrentSourcePageType.FullName).Key;

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pages)
            {
                if (_pages.ContainsKey(pageKey))
                {
                    throw new ArgumentException($"The key {pageKey} is already configured in NavigationService");
                }
                if (_pages.Any(p => p.Value == pageType))
                {
                    throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == pageType).Key}");
                }
                _pages.Add(pageKey, pageType);
            }
        }

        public bool CanGoBack => Frame.CanGoBack;

        public bool CanGoForward => Frame.CanGoForward;

        public void GoBack() => Frame.GoBack();

        public void GoForward() => Frame.GoForward();

        public bool Navigate(string pageKey, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            lock (_pages)
            {
                if (!_pages.ContainsKey(pageKey))
                {
                    throw new ArgumentException($"Page not found: {pageKey}. Did you forget to call NavigationService.Configure?", "pageKey");
                }
                var navigationResult = Frame.Navigate(_pages[pageKey], parameter, infoOverride);
                return navigationResult;
            }
        }

        private void NavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (CanGoBack)
            {
                GoBack();
                e.Handled = true;
            }
        }
    }
}