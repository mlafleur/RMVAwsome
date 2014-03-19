using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RMV.Awesome.Droid
{
    [Activity(Label = "Branch Details")]
    public class BranchDetail : Activity
    {
        RMV.Awesome.PCL.Model.MainViewModel viewModel = RMV.Awesome.PCL.Model.MainViewModel.Current;

        private RMV.Awesome.PCL.Model.Branch _branch;

        public RMV.Awesome.PCL.Model.Branch Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }
        


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.BranchDetail);

            string text = Intent.GetStringExtra("Town");
            Branch = viewModel.Items.FirstOrDefault(c => c.Town == text);

            FindViewById<TextView>(Resource.Id.BranchAddress).Text = Branch.Address;
            FindViewById<TextView>(Resource.Id.BranchTitle).Text = Branch.Title;
            FindViewById<TextView>(Resource.Id.LicensingWait).Text = Branch.LicensingWait;
            FindViewById<TextView>(Resource.Id.RegistrationWait).Text = Branch.RegistrationWait;
            FindViewById<ImageView>(Resource.Id.BranchImage).SetImageURI(Android.Net.Uri.Parse(Branch.ImagePath));
        }
    }
}