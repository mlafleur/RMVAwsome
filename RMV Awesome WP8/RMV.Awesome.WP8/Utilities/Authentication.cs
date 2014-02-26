using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMV.Awesome.WP8.Utilities
{
    /// <summary>
    /// All praise to Jeff Sanders; from whom I took this idea (and quite a bit of code) from.
    /// http://blogs.msdn.com/b/jpsanders/archive/2013/06/24/use-passwordvault-to-store-your-tokens-windows-azure-mobile-services.aspx
    /// If you're doing Azure Mobile Services you should check out his blog, it covers all of the WTF bits. 
    /// </summary>
    public class Authentication
    {
        public static async Task<bool> AuthenticateAsync(MobileServiceAuthenticationProvider provider)
        {

            // Instance the password vault
            var pwdVault = IsolatedStorageSettings.ApplicationSettings;

            // Set the unique key we'll use for this password
            string valutKey = provider + ":" + App.MobileService.ApplicationUri.DnsSafeHost;


            // Fetch stored credentials (if they exist)            
            if (pwdVault.Contains(valutKey))
            {
                // Will throw an exception if none found (I hate that design) (Editor: Jeff wrote that, I agree). 
                string userID = pwdVault[valutKey] as string;
                string jwt = pwdVault[valutKey + "jtw"] as string;

                // Create a User to use with the Mobile Service (instead of doing the login sequence)
                App.MobileService.CurrentUser = new MobileServiceUser(userID);

                // Set the JWT (Auth Token)
                App.MobileService.CurrentUser.MobileServiceAuthenticationToken = jwt;

                // Remove the credential to eliminate potential duplicates (don't worry, we add it back in a second)
                pwdVault.Remove(valutKey);
                pwdVault.Remove(valutKey + "jtw");
            }


            // If we have a user now, validate the credentials are still valid
            if (App.MobileService.CurrentUser != null)
            {
                // Validate the credentials work by reading from a table that requires authentication
                try
                {
                    await App.MobileService.GetTable("TestTable").ReadAsync(string.Empty);
                }
                catch (Exception)
                {
                    // Failed for some reason, time to retry the login
                    App.MobileService.CurrentUser = null;
                }
            }

            // Make sure we have a user or we log in
            if (App.MobileService.CurrentUser == null)
            {
                try
                {
                    await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);
                }
                catch (Exception)
                {
                    // Failed to log in / canceled so we close the app
                    return false;
                }
            }

            // Add to the vaule so we can use it next time
            pwdVault[valutKey] = App.MobileService.CurrentUser.UserId;
            pwdVault[valutKey + "jtw"] = App.MobileService.CurrentUser.MobileServiceAuthenticationToken;
            pwdVault.Save();

            return true;
        }
    }
}
