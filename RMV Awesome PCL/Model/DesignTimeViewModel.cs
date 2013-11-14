

namespace RMV.Awesome.PCL.Model
{
    public class DesignTimeViewModel
    {
        public System.Collections.ObjectModel.ObservableCollection<Branch> Items { get; set; }

        public DesignTimeViewModel()
        {
            Items = StaticBranchData.Data;
        }
    }
}
