using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RMV.Awesome.WP8
{
    public partial class BranchPage : PhoneApplicationPage
    {
        public BranchPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string branchindex = string.Empty;
            NavigationContext.QueryString.TryGetValue("branchindex", out branchindex);

            PCL.Model.MainViewModel viewModel = PCL.Model.MainViewModel.Current;
            this.DataContext = viewModel.Items[int.Parse(branchindex)];
            viewModel.FetchXMLFeed();
        }       
    }
}