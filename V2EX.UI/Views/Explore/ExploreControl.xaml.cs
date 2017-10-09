using Microsoft.Toolkit.Uwp.UI.Controls;
using V2EX.UI.Services;
using V2EX.UI.ViewModels;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace V2EX.UI.Views.Explore
{
    public sealed partial class ExploreControl : UserControl
    {
        private ExploreViewModel ViewModel
        {
            get { return DataContext as ExploreViewModel; }
        }
        public ExploreControl()
        {
            this.InitializeComponent();
        }

        private void MasterDetailsView_ViewStateChanged(object sender, MasterDetailsViewState e)
        {
            NavigationServiceEx.SetHamburgerMenuVisibility(e);
        }
    }
}
