using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RMV.Awesome.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BranchPage : Page
    {
        public BranchPage()
        {
            this.InitializeComponent();

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {


            string town = e.Parameter as string;
            using (var cl = new UWPClient())
            {
                var results = await cl.Branch.GetBranchListAsync();
                this.DataContext = results.First(c => c.Town == town);
            }
            base.OnNavigatedTo(e);  

        }

        
    }
}
