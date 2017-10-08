using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Models;
using V2EX.Service;

namespace V2EX.UI.ViewModels
{
    public class ExploreViewModel : ViewModelBase
    {
        private List<EmailModel> _emails = SampleDataService.GetEmails().ToList();
        public List<EmailModel> Emails
        {
            get { return _emails; }
            set { Set(ref _emails, value); }
        }

        private string _lastTabName = string.Empty;
        private RelayCommand<string> _tabItemClickCommand;
        public RelayCommand<string> TabItemClickCommand
        {
            get
            {
                return _tabItemClickCommand ?? (_tabItemClickCommand = new RelayCommand<string>(tabName =>
                {
                    if (tabName != _lastTabName)
                    {
                        Debug.WriteLine(tabName);
                        _lastTabName = tabName;
                    }
                }));
            }
        }
    }
}
