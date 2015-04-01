using RMV.Awesome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMV.Awesome
{
    public class Feeds
    {
        public static async Task<IEnumerable<Branch>> GetBranchDetails()
        {
            // Fetch Azure Branch Data
            var azureBranchData = await new AzureBranchData().GetBranchCollection();
            var sortedBranchData = azureBranchData.OrderBy(c => c.Distance = Location.CalculateDistance(c.Latitude, c.Longitude));
            return sortedBranchData.AsEnumerable();
        }

        public static async Task<IEnumerable<Branch>> GetWaitTimeChanges(IEnumerable<Branch> branchCollection)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string response = await client.GetStringAsync("http://www.massdot.state.ma.us/feeds/qmaticxml/qmaticXML.aspx");

            // Convert the response into an XML document
            var xmlDoc = XDocument.Load((new System.IO.StringReader(response)));

            HashSet<Branch> updatedBranchSet = new HashSet<Branch>();

            foreach (var item in xmlDoc.Descendants("branch"))
            {
                string town = (string)item.Element("town");
                string licensing = (string)item.Element("licensing");
                string registration = (string)item.Element("registration");


                // Make sure our branch collection contains this town
                if (!branchCollection.Any(c => c.Town == town)) continue;

                // Get this existing branch record
                var existingBranch = branchCollection.First(c => c.Town == (string)item.Element("town"));

                // If wait times have changed, update
                if (existingBranch.RegistrationWait != registration || existingBranch.LicensingWait != licensing)
                {
                    existingBranch.LicensingWait = ((string)item.Element("licensing"));
                    existingBranch.RegistrationWait = ((string)item.Element("registration"));
                    updatedBranchSet.Add(existingBranch);
                }

            }

            return updatedBranchSet;
        }
    }
}
