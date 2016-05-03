using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RMV.Awesome.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            this.Loading += MainPage_Loading;
        }

        private void MainPage_Loading(FrameworkElement sender, object args)
        {

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            using (var cl = new UWPClient())
            {
                double lat;
                double lng;

                try
                {
                    // Reference the locator
                    var locator = new Windows.Devices.Geolocation.Geolocator();

                    // Sample the current position
                    var position = await locator.GetGeopositionAsync();

                    // Store the current POS
                    lat = position.Coordinate.Point.Position.Latitude;
                    lng = position.Coordinate.Point.Position.Longitude;
                }
                catch
                {
                    lat = 0;
                    lng = 0;
                }

                if (lat == 0 && lng == 0)
                    this.DataContext = await cl.Branch.GetBranchListAsync();                
                else
                    this.DataContext = await cl.Branch.GetBranchListDistanceAsync(lat, lng);


            }
            base.OnNavigatedTo(e);
        }

        private void itemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var branch = e.ClickedItem as UWP.Models.RMVBranch;
            this.Frame.Navigate(typeof(BranchPage), branch.Town );
        }

        private void itemGridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var itemsControl = sender as ListViewBase;
            ItemsWrapGrid itemsPanel = itemsControl.ItemsPanelRoot as ItemsWrapGrid;

            if (itemsPanel != null)
            {
                //Get total size (leave room for scrolling.)
                var total = e.NewSize.Width - 10;


                //Minimum item size.
                var itemMinSize = 400;


                //How many items can be fit whole.
                var canBeFit = Math.Floor(total / itemMinSize);


                //logic that if the total items
                //are less then the number of items that
                //would fit then devide the total size by
                //the number of items rather than the number
                //of items that would actually fit.
                if (itemsControl.Items.Count > 0 &&
                    itemsControl.Items.Count < canBeFit)
                {
                    canBeFit = itemsControl.Items.Count;
                }


                //Set the items Panel item width appropriately.
                //Note you will need your container to stretch
                //along with the items panel or it will look
                //strange.
                //  <GridView.ItemContainerStyle>
                //< Style TargetType = "GridViewItem" >
                //< Setter Property = "HorizontalContentAlignment" Value = "Stretch" />
                // < Setter Property = "HorizontalAlignment" Value = "Stretch" />
                //</ Style >
                // </ GridView.ItemContainerStyle >
                itemsPanel.ItemWidth = total / canBeFit;
            }

        }



        private void ResetGridSize(double width)
        {
            ItemsWrapGrid MyItemsPanel = (ItemsWrapGrid)itemGridView.ItemsPanelRoot;
            double margin = 10.0;
            int ItemsNumber = itemGridView.Items.Count;
            if (ItemsNumber > 0)
                MyItemsPanel.ItemWidth = (width - margin) / (double)ItemsNumber;

        }
    }
}
