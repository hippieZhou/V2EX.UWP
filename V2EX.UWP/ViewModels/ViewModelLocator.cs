using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.UWP.Services;
using V2EX.UWP.Views;

namespace V2EX.UWP.ViewModels
{
    class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new ExNavigationService();
            nav.Configure(typeof(HomeViewModel).FullName, typeof(HomePage));
            nav.Configure(typeof(NodesViewModel).FullName, typeof(NodesPage));
            nav.Configure(typeof(MessageViewModel).FullName, typeof(MessagePage));
            nav.Configure(typeof(LibraryViewModel).FullName, typeof(LibraryPage));
            SimpleIoc.Default.Register(() => nav);

            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<NodesViewModel>();
            SimpleIoc.Default.Register<MessageViewModel>();
            SimpleIoc.Default.Register<LibraryViewModel>();
        }
        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();
        public HomeViewModel HomeViewModel => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public NodesViewModel NodesViewModel => ServiceLocator.Current.GetInstance<NodesViewModel>();
        public MessageViewModel MessageViewModel => ServiceLocator.Current.GetInstance<MessageViewModel>();
        public LibraryViewModel LibraryViewModel => ServiceLocator.Current.GetInstance<LibraryViewModel>();
    }
}
