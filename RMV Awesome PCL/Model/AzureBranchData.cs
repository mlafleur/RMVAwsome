using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace RMV.Awesome.PCL.Model
{
    public class AzureBranchData
    {
        public async Task<IEnumerable<Branch>> GetBranchCollection()
        {
            // Create a client to fetch our data
            Microsoft.WindowsAzure.MobileServices.MobileServiceClient RMVAwesomeMobileServiceClient =
                new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(
                    "https://rmvawesomemobileservice.azure-mobile.net/", "UClTVJXEtAkzboYGjkTaWgDZbiXUFq92");


            // Return the Azure Branch table
            return await RMVAwesomeMobileServiceClient.GetTable<Branch>().ReadAsync();
        }
    }
}