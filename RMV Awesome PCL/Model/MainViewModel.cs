using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMV.Awesome.PCL.Model
{
    public class MainViewModel
    {
        private static MainViewModel _current;

        public static MainViewModel Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new MainViewModel();
                }
                return _current;
            }
           
        }

        public System.Collections.ObjectModel.ObservableCollection<Branch> Items { get; set; }

        private MainViewModel()
        {
            // Seed our Items with our static branch data
            if (Items == null) Items = StaticBranchData.Data;
        }

        public async void FetchXMLFeed()
        {
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

    }
}
