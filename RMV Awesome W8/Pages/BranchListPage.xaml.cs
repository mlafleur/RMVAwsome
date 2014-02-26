using RMV.Awesome.W8.Common;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace RMV.Awesome.W8.Pages
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class BranchListPage : Page
    {
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private NavigationHelper navigationHelper;
        public BranchListPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        private void Branch_Click(object sender, ItemClickEventArgs e)
        {
            PCL.Model.Branch branch = e.ClickedItem as PCL.Model.Branch;
            this.Frame.Navigate(typeof(Pages.BranchPage), branch.Town);
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

            try
            {
                // Reference the locator
                var locator = new Windows.Devices.Geolocation.Geolocator();

                // Sample the current position
                var position = await locator.GetGeopositionAsync();

                // Pass current position to PCL
                RMV.Awesome.PCL.Utilities.Location.DeviceLatitude = position.Coordinate.Point.Position.Latitude;
                RMV.Awesome.PCL.Utilities.Location.DeviceLogitude = position.Coordinate.Point.Position.Longitude;
            }
            catch
            {
                // We were unable to check location (most common reason is the user disabled geolocation
            }
            
            this.DefaultViewModel["Items"] = PCL.Model.MainViewModel.Current.Items;
            PCL.Model.MainViewModel.Current.FetchXMLFeed();
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        ///
        /// Page specific logic should be placed in event handlers for the
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }
        #endregion NavigationHelper registration
    }
}