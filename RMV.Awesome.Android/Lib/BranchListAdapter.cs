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

            view.FindViewById<TextView>(Resource.Id.branchitem_title).Text = items[position].Title;
            view.FindViewById<TextView>(Resource.Id.branchitem_subtitle).Text = items[position].Subtitle;



            var imageUri = Android.Net.Uri.Parse(items[position].ImagePath);

            view.FindViewById<ImageView>(Resource.Id.branchitem_icon).SetImageURI(imageUri);

            return view;
        }

    }
}