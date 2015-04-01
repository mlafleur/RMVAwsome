using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System;

namespace RMV.Awesome.Model
{
    public class AzureBranchData
    {
        public async Task<IEnumerable<Branch>> GetBranchCollection()
        {
            // Create a client to fetch our data
            Microsoft.WindowsAzure.MobileServices.MobileServiceClient RMVAwesomeMobileServiceClient =
                new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(
                    "https://rmvawesomemobileservice.azure-mobile.net/", "UClTVJXEtAkzboYGjkTaWgDZbiXUFq92");

            try
            {
                var branchData = await RMVAwesomeMobileServiceClient.GetTable<Branch>().ReadAsync();

                // Return the Azure Branch table
                return branchData;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
        }
    }
}