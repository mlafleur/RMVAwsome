using System;
using System.Collections.Generic;
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
using RMV.Awesome.PCL.Model;

using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Security.Credentials;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ADSUtil
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Authenticate a user
            var result = await Utilities.Authentication.AuthenticateAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);
            if (result == false) // login failed
            {
                Application.Current.Exit(); 
            }


            // Get the Table
            var branchTable = App.RMVAwesomeMobileServiceClient.GetTable<Branch>();

            try
            {
                // Remove all existing entries
                foreach (var item in await branchTable.ReadAsync())
                {
                    await branchTable.DeleteAsync(item);
                }
            }
            catch { }

            // Get the data we want to insert
            var data = GetBranchList();

            // Insert the new rows
            foreach (var item in data)
            {
                await branchTable.InsertAsync(item);
            }


            // Close this utility
            Application.Current.Exit();
        }


        private List<Branch> GetBranchList()
        {
            var tmp = new DesignTimeViewModel();
            return tmp.Items.ToList<Branch>();
            //var details = new List<Branch>();
        }

    }
}
