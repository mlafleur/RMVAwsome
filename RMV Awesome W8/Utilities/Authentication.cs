using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;
using Windows.Security.Credentials;

namespace RMV.Awesome.W8.Utilities
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
            var pwdVault = new PasswordVault();

            // Set the unique key we'll use for this password
            string valutKey = provider + ":" + App.RMVAwesomeMobileServiceClient.ApplicationUri.DnsSafeHost;


            try // Fetch stored credentials (if they exist)            
            {
                // Will throw an exception if none found (I hate that design) (Editor: Jeff wrote that, I agree). 
                var creds = pwdVault.FindAllByResource(valutKey);
                if (creds != null)
                {
                    // We're only storing 1 cred here
                    var userCred = creds[0];

                    // Create a User to use with the Mobile Service (instead of doing the login sequence)
                    App.RMVAwesomeMobileServiceClient.CurrentUser = new MobileServiceUser(userCred.UserName);

                    // Explicitly fetch the PWD into memory 
                    userCred.RetrievePassword();

                    // Set the JWT (Auth Token)
                    App.RMVAwesomeMobileServiceClient.CurrentUser.MobileServiceAuthenticationToken = userCred.Password;

                    // Remove the credential to eliminate potential duplicates (don't worry, we add it back in a second)
                    pwdVault.Remove(userCred);
                }

            }
            catch (Exception) {  /* Unable to find or retrieve credentials */ }


            // If we have a user now, validate the credentials are still valid
            if (App.RMVAwesomeMobileServiceClient.CurrentUser != null)
            {
                // Validate the credentials work by reading from a table that requires authentication
                try
                {
                    await App.RMVAwesomeMobileServiceClient.GetTable("TestTable").ReadAsync(string.Empty);
                }
                catch (Exception)
                {
                    // Failed for some reason, time to retry the login
                    App.RMVAwesomeMobileServiceClient.CurrentUser = null;
                }
            }

            // Make sure we have a user or we log in
            if (App.RMVAwesomeMobileServiceClient.CurrentUser == null)
            {
                try
                {
                    await App.RMVAwesomeMobileServiceClient.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);
                }
                catch (Exception)
                {
                    // Failed to log in / canceled so we close the app
                    return false;
                }
            }

            // Add to the vaule so we can use it next time
            var newCred = new PasswordCredential(valutKey,
                App.RMVAwesomeMobileServiceClient.CurrentUser.UserId,
                App.RMVAwesomeMobileServiceClient.CurrentUser.MobileServiceAuthenticationToken);
            pwdVault.Add(newCred);

            return true;
        }
    }
}
