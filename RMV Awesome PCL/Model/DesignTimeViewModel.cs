namespace RMV.Awesome.PCL.Model
{
    public class DesignTimeViewModel
    {
        public DesignTimeViewModel()
        {
            Items = StaticBranchData.Data;
        }

        public System.Collections.ObjectModel.ObservableCollection<Branch> Items { get; set; }
    }
}