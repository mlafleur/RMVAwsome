using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RMV.Awesome.Model
{
    public class MainViewModel
    {
        private static MainViewModel _current;

        private MainViewModel()
        {
            Items = new System.Collections.ObjectModel.ObservableCollection<Branch>();
            
        }

        public static MainViewModel Current
        {
            get
            {
                if (_current == null) _current = new MainViewModel();
                return _current;
            }
        }

        public System.Collections.ObjectModel.ObservableCollection<Branch> Items { get; set; }

        public async Task FetchXMLFeed()
        {
            try
            {
                // If our current branch list (items) is empty, populate it
                if (Items.Count == 0) 
                    Items = new ObservableCollection<Branch>(await Feeds.GetBranchDetails());

                var updatedWaitTimes = await Feeds.GetWaitTimeChanges(Items);

                foreach (var branch in updatedWaitTimes)
                {
                    var existing = Items.FirstOrDefault(c => c.Town == branch.Town);
                    existing.RegistrationWait = branch.RegistrationWait;
                    existing.LicensingWait = branch.LicensingWait;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}