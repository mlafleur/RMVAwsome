using System.Linq;

namespace RMV.Awesome.PCL.Model
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

        public async void FetchXMLFeed()
        {
            try
            {
                // Populate Items with AzureBranchData (if needed)
                if (Items.Count == 0)
                {
                    foreach (var item in await new AzureBranchData().GetBranchCollection())
                    {
                        Items.Add(item);
                    }
                }


                // Fetch the XML from the web
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                string response = await client.GetStringAsync("http://www.massdot.state.ma.us/feeds/qmaticxml/qmaticXML.aspx");

                // Convert the response into an XML document
                var xmlDoc = System.Xml.Linq.XDocument.Load((new System.IO.StringReader(response)));

                // Iterate through the branches in the document and update the times
                foreach (var item in xmlDoc.Descendants("branch"))
                {
                    if (this.Items.Any(c => c.Town == (string)item.Element("town")))
                    {
                        Branch location = this.Items.First(c => c.Town == (string)item.Element("town"));
                        location.LicensingWait = ((string)item.Element("licensing"));
                        location.RegistrationWait = ((string)item.Element("registration"));
                    }
                }
            }
            catch { }
        }
    }
}