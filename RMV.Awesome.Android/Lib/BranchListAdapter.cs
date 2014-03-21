using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.ObjectModel;

namespace RMV.Awesome.Droid.Lib
{
    public class BranchListAdapter : BaseAdapter<RMV.Awesome.PCL.Model.Branch>
    {
        private Activity context;
        private ObservableCollection<RMV.Awesome.PCL.Model.Branch> items;

        public BranchListAdapter(Activity context, ObservableCollection<RMV.Awesome.PCL.Model.Branch> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override RMV.Awesome.PCL.Model.Branch this[int position]
        {
            get { return items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.BranchItem, null);

            // Assign the data to the correct layout elements
            view.FindViewById<TextView>(Resource.Id.branchitem_title).Text = items[position].Title;
            view.FindViewById<TextView>(Resource.Id.branchitem_subtitle).Text = items[position].Subtitle;
            view.FindViewById<ImageView>(Resource.Id.branchitem_icon).SetImageURI(Android.Net.Uri.Parse(items[position].ImagePath));

            return view;
        }
    }
}