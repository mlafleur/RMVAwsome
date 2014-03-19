using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Android.App;
using Android.Net;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;

namespace RMV.Awesome.Droid
{
    [Activity(Label = "RMV Awesome!", MainLauncher = true)]
    public class MainActivity : Activity
    {
        RMV.Awesome.PCL.Model.MainViewModel viewModel = RMV.Awesome.PCL.Model.MainViewModel.Current;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var progressBar = (ProgressBar)FindViewById(Resource.Id.progressBar1);
            progressBar.Visibility = ViewStates.Visible;

            await viewModel.FetchXMLFeed();

            // Create a new BranchListAdapter to connect the ListView to our items
            var adapter = new Lib.BranchListAdapter(this, viewModel.Items);

            // Find the list view in the layout
            ListView listView = (ListView)FindViewById(Resource.Id.branchList);

            

            // Add a click event
            listView.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>((s, e) => {
                var branchDetail = new Intent(this, typeof(BranchDetail));
                var branch = adapter[e.Position];
                branchDetail.PutExtra("Town", branch.Town);
                StartActivity(branchDetail);  

                this.StartActivity(typeof(BranchDetail));
            });

            // Assign the adapter
            listView.Adapter = adapter;
            progressBar.Visibility = ViewStates.Gone;

            ThreadPool.QueueUserWorkItem(async o => 
            {
                foreach (var item in viewModel.Items)
                {
                    await Lib.Images.Download(item);
                }

                RunOnUiThread(() => { adapter.NotifyDataSetChanged(); });
            });

        }

        

    }


}


