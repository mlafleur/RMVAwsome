using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Threading;

namespace RMV.Awesome.Droid
{
    [Activity(Label = "RMV Awesome!", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private RMV.Awesome.PCL.Model.MainViewModel viewModel = RMV.Awesome.PCL.Model.MainViewModel.Current;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Reference our ProgvessBar and display it
            var progressBar = (ProgressBar)FindViewById(Resource.Id.progressBar1);
            progressBar.Visibility = ViewStates.Visible;

            // Fetch the branch items from Azure and the RMV
            await viewModel.FetchXMLFeed();

            // Create a new BranchListAdapter to connect the ListView to our items
            var adapter = new Lib.BranchListAdapter(this, viewModel.Items);

            // Find the list view in the layout
            GridView gridView = (GridView)FindViewById(Resource.Id.gridview);

            // Add a click event
            gridView.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>((s, e) =>
            {
                // Create a branch detail activity
                var branchDetail = new Intent(this, typeof(BranchDetail));

                // Pass in the town
                branchDetail.PutExtra("Town", adapter[e.Position].Town);

                // Start the activity
                StartActivity(branchDetail);
            });

            // Assign the adapter
            gridView.Adapter = adapter;

            // Hides
            progressBar.Visibility = ViewStates.Gone;

            // This downloads the images in a background thread
            ThreadPool.QueueUserWorkItem(async o =>
            {
                foreach (var item in viewModel.Items)
                {
                    await Lib.Images.Download(item);
                }

                // This lets the adapter know to update itself
                // It needs to run back on the UI thread
                RunOnUiThread(() => { adapter.NotifyDataSetChanged(); });
            });
        }
    }
}