using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.UI.ViewModels.Dashboard;
using V2EX.UI.ViewModels.Explore;

namespace V2EX.UI.ViewModels
{
    class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<ExploreViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
        }

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();
        public ExploreViewModel ExploreViewModel => ServiceLocator.Current.GetInstance<ExploreViewModel>();
        public DashboardViewModel DashboardViewModel => ServiceLocator.Current.GetInstance<DashboardViewModel>();
    }
}
