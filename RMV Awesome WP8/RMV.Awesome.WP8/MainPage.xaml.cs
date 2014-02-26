using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RMV.Awesome.WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/BranchPage.xaml?branchindex=" + BranchList.SelectedIndex.ToString(), UriKind.Relative));
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            // Get our application settings
            var settings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;

            // Make sure the location is enabled
            if ((bool)settings["location"] == true)
            {
                try
                {
                    // Reference the locator
                    var locator = new Windows.Devices.Geolocation.Geolocator();

                    // Sample the current position
                    var position = await locator.GetGeopositionAsync();

                    // Pass current position to PCL
                    RMV.Awesome.PCL.Utilities.Location.DeviceLatitude = position.Coordinate.Latitude;
                    RMV.Awesome.PCL.Utilities.Location.DeviceLogitude = position.Coordinate.Longitude;
                }
                catch
                {
                    // We were unable to check location 
                }
            }

            PCL.Model.MainViewModel viewModel = PCL.Model.MainViewModel.Current;
            this.DataContext = viewModel;
            viewModel.FetchXMLFeed();
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This app accesses your phone's location. Is that ok?",
            "Location",
            MessageBoxButton.OKCancel);
           

            if (result == MessageBoxResult.OK)
            {
                System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["location"] = true;
            }
            else
            {
                System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["location"] = false;
            }

            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Save();

        }
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}