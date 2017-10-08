using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Models;
using V2EX.Service;

namespace V2EX.UI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private List<EmailModel> _emails = SampleDataService.GetEmails().ToList();
        public List<EmailModel> Emails
        {
            get { return _emails; }
            set { Set(ref _emails, value); }
        }
    }
}
