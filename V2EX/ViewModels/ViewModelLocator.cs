using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Commons;
using V2EX.Views;

namespace V2EX.ViewModels
{
    public class ViewModelLocator
    {
        public static string HomePageKey = typeof(HomeViewModel).FullName;
        public static string NodesPageKey = typeof(NodesViewModel).FullName;
        public static string SettingsPageKey = typeof(SettingsViewModel).FullName;

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register(() => new NavigationService());
            Register<HomeViewModel, HomePage>();
            Register<NodesViewModel, NodesPage>();
            Register<SettingsViewModel, SettingsPage>();
            Register<TopicViewModel, TopicPage>();
        }

        private static void Register<VM, V>() where VM : class
        {
            SimpleIoc.Default.Register<VM>();
            NavigationService nav = ServiceLocator.Current.GetInstance<NavigationService>();
            string fullName = typeof(VM).FullName;
            nav.Configure(fullName, typeof(V));
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public NodesViewModel Nodes => ServiceLocator.Current.GetInstance<NodesViewModel>();
        public SettingsViewModel Settings => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public TopicViewModel Topic => ServiceLocator.Current.GetInstance<TopicViewModel>();
    }
}
