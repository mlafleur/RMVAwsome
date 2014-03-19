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

using System.Collections.ObjectModel;
using Android.Net;
using Android.Graphics;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace RMV.Awesome.Droid.Lib
{
    public class BranchListAdapter : BaseAdapter<RMV.Awesome.PCL.Model.Branch>
    {
        ObservableCollection<RMV.Awesome.PCL.Model.Branch> items;
        Activity context;

        

        public BranchListAdapter(Activity context, ObservableCollection<RMV.Awesome.PCL.Model.Branch> items)
            : base()
        {
            this.context = context;
            this.items = items;            
        }

        public override long GetItemId(int position)
        {            
            return position;
        }

        public override RMV.Awesome.PCL.Model.Branch this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.BranchItem, null);

            view.FindViewById<TextView>(Resource.Id.Title).Text = items[position].Title;
            view.FindViewById<TextView>(Resource.Id.Subtitle).Text = items[position].Subtitle;



            var imageUri = Android.Net.Uri.Parse(items[position].ImagePath);

            view.FindViewById<ImageView>(Resource.Id.Image).SetImageURI(imageUri);

            return view;
        }

        private void DownloadImage(RMV.Awesome.PCL.Model.Branch branch)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string localFilename = branch.Id;
            string localPath = System.IO.Path.Combine(documentsPath, localFilename);


            // File Exists
            if (File.Exists(localPath))
            {
                System.Diagnostics.Debug.WriteLine("Deleting " + localPath);
                File.Delete(localPath);
            }


            // File does not exist
            if (!File.Exists(localPath))
            {
                var webClient = new WebClient();

                webClient.DownloadDataCompleted += (s, e) =>
                {
                    var bytes = e.Result; // get the downloaded data          

                    if (bytes == null || bytes.Length == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("**** DOWNLOAD FAILED ****");
                    }

                    if (!File.Exists(localPath)) File.WriteAllBytes(localPath, bytes);
                    branch.ImagePath = localPath;
                    System.Diagnostics.Debug.WriteLine("Image saved as " + branch.ImagePath);

                    this.NotifyDataSetChanged();
                };

                System.Diagnostics.Debug.WriteLine("Downloading " + branch.ImagePath);
                var uri = new System.Uri(branch.ImagePath);
                webClient.DownloadDataAsync(uri);
            }


        }


    }
}