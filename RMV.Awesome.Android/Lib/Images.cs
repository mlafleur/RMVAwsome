using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace RMV.Awesome.Droid.Lib
{
    public class Images
    {
        public static async Task Download(RMV.Awesome.PCL.Model.Branch branch)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string localFilename = branch.Id;
            string localPath = System.IO.Path.Combine(documentsPath, localFilename);


            // File Exists
            if (File.Exists(localPath))
            {
                var created = File.GetCreationTime(localPath);
                if(created.CompareTo(DateTime.Now.AddDays(-1)) < 0)
                {
                    System.Diagnostics.Debug.WriteLine("Keeping " + localPath);
                    return;
                }

                System.Diagnostics.Debug.WriteLine("Deleting " + localPath);
                File.Delete(localPath);
            }


            // File does not exist
            if (!File.Exists(localPath))
            {
                var webClient = new WebClient();

                System.Diagnostics.Debug.WriteLine("Downloading " + branch.ImagePath);
                var uri = new System.Uri(branch.ImagePath);

                var bytes = await webClient.DownloadDataTaskAsync(uri);

                if (bytes == null || bytes.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine("**** DOWNLOAD FAILED ****");
                }

                if (!File.Exists(localPath)) File.WriteAllBytes(localPath, bytes);
                branch.ImagePath = localPath;
                System.Diagnostics.Debug.WriteLine("Image saved as " + branch.ImagePath);
            }


        }

    }
}