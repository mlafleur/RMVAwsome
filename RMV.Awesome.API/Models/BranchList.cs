using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Humanizer;

namespace RMV.Awesome.API.Models
{
    public class BranchList
    {
        private static List<RMVBranch> branchList = new List<RMVBranch>();

        public async static Task<List<RMVBranch>> GetAll()
        {
            using (var db = new BranchEntities())
            {
                var x = from c in db.CurrentWaitTimes orderby c.DisplayName select c;
                PopulateBranchList(x);
                //await RefreshWaitTimes();
                return branchList.OrderBy(n => n.DisplayName).ToList();
            }
        }

        public async static Task<List<RMVBranch>> GetAll(double deviceLatitude, double deviceLogitude)
        {
            using (var db = new BranchEntities())
            {
                var x = from c in db.CurrentWaitTimes orderby c.DisplayName select c;
                PopulateBranchList(x);
                //await RefreshWaitTimes();
                CalculateDistance(deviceLatitude, deviceLogitude);
                return branchList.OrderBy(d => d.Distance).ToList();
            }
        }

        public static async Task<bool> RefreshSamples()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string response = await client.GetStringAsync("http://www.massdot.state.ma.us/feeds/qmaticxml/qmaticXML.aspx");

            // Convert the response into an XML document
            var xmlDoc = XDocument.Load((new System.IO.StringReader(response)));
            using (var db = new BranchEntities())
            {
                foreach (var item in xmlDoc.Descendants("branch"))
                {
                    string town = (string)item.Element("town");
                    string licensing = (string)item.Element("licensing");
                    string registration = (string)item.Element("registration");

                    var branchHistory = new BranchHistory();
                    branchHistory.ID = Guid.NewGuid();
                    branchHistory.Town = town;
                    branchHistory.SampleTime = DateTimeOffset.UtcNow;
                    branchHistory.Fetched = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

                    if (licensing.ToLower() != "closed")
                        branchHistory.LicensingWait = TimeSpan.Parse(licensing).TotalHours;
                    else
                        branchHistory.LicensingWait = -1;

                    if (registration.ToLower() != "closed")
                        branchHistory.RegistrationWait = TimeSpan.Parse(registration).TotalHours;
                    else
                        branchHistory.RegistrationWait = -1;

                    db.BranchHistories.Add(branchHistory);
                }
                db.SaveChanges();
            }

            return true;
        }

        private static void CalculateDistance(double deviceLatitude, double deviceLogitude)
        {
            foreach (var branch in branchList)
            {
                // Based on code by Gary Dryden on CodeProject - http://www.codeproject.com/Articles/12269/Distance-between-locations-using-latitude-and-long
                double dDistance = Double.MinValue;
                double dLat1InRad = deviceLatitude * (Math.PI / 180.0);
                double dLong1InRad = deviceLogitude * (Math.PI / 180.0);
                double dLat2InRad = branch.Latitude * (Math.PI / 180.0);
                double dLong2InRad = branch.Longitude * (Math.PI / 180.0);
                double dLongitude = dLong2InRad - dLong1InRad;
                double dLatitude = dLat2InRad - dLat1InRad;

                // Intermediate result a.
                double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                           Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                           Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

                // Intermediate result c (great circle distance in Radians).
                double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

                // Distance.
                const Double kEarthRadiusKms = 6376.5;
                dDistance = kEarthRadiusKms * c;

                branch.Distance = Math.Round(dDistance, 1);
            }
        }

        private static void PopulateBranchList(IEnumerable<CurrentWaitTime> waitTimes)
        {
            branchList = new List<RMVBranch>();
            foreach (var item in waitTimes)
            {
                var branchItem = new RMVBranch();
                branchItem.Town = item.Town;
                branchItem.Address = item.Address;
                branchItem.DisplayName = item.DisplayName;
                branchItem.ImagePath = item.ImagePath;
                branchItem.Latitude = item.Latitude;
                branchItem.Longitude = item.Longitude;

                if (item.LicensingWait == -1) branchItem.LicensingWait = "Closed";
                else branchItem.LicensingWait = TimeSpan.FromHours((double)item.LicensingWait).Humanize();

                if (item.RegistrationWait == -1) branchItem.RegistrationWait = "Closed";
                else branchItem.RegistrationWait = TimeSpan.FromHours((double)item.RegistrationWait).Humanize();
                branchList.Add(branchItem);
            }
        }

     
        public class RMVBranch
        {
            public string Address { get; set; }

            public string DisplayAddress
            {
                get { return Address + ", " + Town + ", MA"; }
            }

            public string DisplayName { get; set; }

            public double Distance { get; set; }

            public string ImagePath { get; set; }

            public double Latitude { get; set; }

            public string LicensingWait { get; set; }

            public double Longitude { get; set; }

            public string RegistrationWait { get; set; }

            public string Town { get; set; }
        }

        //private static void ValidateList()
        //{
        //    branchList.Add(new Branch() { DisplayName = "Attleboro", Address = "75 Park Street", Latitude = 41.943778, Longitude = -71.279825, Town = "Attleboro", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/f/f1/Old_Attleboro%2C_MA%2C_post_office.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Boston", Address = "630 Washington Street", Latitude = 42.352181, Longitude = -71.062456, Town = "Washington", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/94/USA-Massachusetts_State_House0.jpg/640px-USA-Massachusetts_State_House0.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Braintree", Address = "10 Plain Street", Latitude = 42.196212, Longitude = -71.003572, Town = "Braintree", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Braintree_Old_Library.jpg/640px-Braintree_Old_Library.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Brockton", Address = "490 Forest Ave", Latitude = 42.070619, Longitude = -71.041389, Town = "Brockton", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/85/BrocktonCityHall.jpg/592px-BrocktonCityHall.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Chicopee", Address = "1011 Chicopee Street", Latitude = 42.191904, Longitude = -72.600594, Town = "Chicopee", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/9d/City_Hall%2C_Chicopee_MA.jpg/640px-City_Hall%2C_Chicopee_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Danvers", Address = "100 Independence Way", Latitude = 42.55290641370957, Longitude = -70.93985795974731, Town = "Danvers", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/6/63/North_end_of_MA_Route_35_entering_Danvers%2C_MA.jpg/512px-North_end_of_MA_Route_35_entering_Danvers%2C_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Easthampton", Address = "116 Pleasant St", Latitude = 42.272979, Longitude = -72.663482, Town = "East Hampton", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/b/bf/Mount_Tom_Easthampton_MA.JPG/320px-Mount_Tom_Easthampton_MA.JPG" });
        //    branchList.Add(new Branch() { DisplayName = "Fall River", Address = "203 Plymouth Avenue", Latitude = 41.697317, Longitude = -71.149106, Town = "Fall River", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/2/21/Downtown_Fall_River.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Greenfield", Address = "18 Miner Street", Latitude = 42.585972, Longitude = -72.619682, Town = "Greenfield", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/e/ed/Greenfield_Town_Hall.jpg/411px-Greenfield_Town_Hall.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Haverhill", Address = "4 Summer Street", Latitude = 42.777554, Longitude = -71.077176, Town = "Haverhill", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/1/17/Tattersall_Farm%2C_Haverhill_MA.jpg/640px-Tattersall_Farm%2C_Haverhill_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Lawrence", Address = "73 Winthrop Avenue", Latitude = 42.689929, Longitude = -71.149626, Town = "Lawrence", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/e/ee/Ayer_Mill_View.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Leominster", Address = "80 Erdman Way", Latitude = 42.547593, Longitude = -71.757933, Town = "Leominster", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/a/a0/Monument_Square_Leominster.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Lowell", Address = "  77 Middlesex Street", Latitude = 42.641479, Longitude = -71.310108, Town = "Lowell", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/4c/Front_of_boott_mill.jpg/320px-Front_of_boott_mill.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Martha's Vineyard", Address = "11 A Street", Latitude = 41.391582, Longitude = -70.603321, Town = "Marthas Vineyard", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Martha%27s_Vineyard_Cottages.jpg/640px-Martha%27s_Vineyard_Cottages.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Milford", Address = "14 Beach Street", Latitude = 42.142346, Longitude = -71.512389, Town = "Milford", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fe/Milford_Town_Hall%2C_MA.jpg/320px-Milford_Town_Hall%2C_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Nantucket", Address = "Broad Street", Latitude = 41.28554, Longitude = -70.097874, Town = "Nantucket", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Brant_Point_Light_-_Nantucket_MA.jpg/576px-Brant_Point_Light_-_Nantucket_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Natick", Address = "Massachusetts Turnpike Natick Eastbound API Center", Latitude = 42.311156, Longitude = -71.364892, Town = "Natick", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Fast_lane_sign.jpg/640px-Fast_lane_sign.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "New Bedford", Address = "278 Union Street", Latitude = 41.633748, Longitude = -70.929116, Town = "New Bedford", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Downtown_New_Bedford_MA.jpg/640px-Downtown_New_Bedford_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "North Adams", Address = "33 Main Street", Latitude = 42.69956, Longitude = -73.113883, Town = "N Adams", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/MassMoCa.jpg/640px-MassMoCa.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Pittsfield", Address = "333 East Street", Latitude = 42.447641, Longitude = -73.247607, Town = "Pittsfield", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Park_Square%2C_Pittsfield_MA.jpg/640px-Park_Square%2C_Pittsfield_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Plymouth", Address = "40 Industrial Park Road", Latitude = 41.960272, Longitude = -70.700532, Town = "Plymouth", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/4b/Mayflower_II.jpg/600px-Mayflower_II.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Revere", Address = "9 Everett Street", Latitude = 42.409733, Longitude = -71.00108, Town = "Revere", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Paul_Revere_Statue_by_Cyrus_E._Dallin%2C_North_End%2C_Boston%2C_MA.JPG/576px-Paul_Revere_Statue_by_Cyrus_E._Dallin%2C_North_End%2C_Boston%2C_MA.JPG" });
        //    branchList.Add(new Branch() { DisplayName = "Roslindale", Address = "8 Cummins Highway", Latitude = 42.286383, Longitude = -71.127759, Town = "Roslindale", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/SacredHeartRoslindale_PWFord2.jpg/566px-SacredHeartRoslindale_PWFord2.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "South Yarmouth", Address = "1084 Route 28", Latitude = 41.661432, Longitude = -70.202079, Town = "Yarmouth", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/Bass_River%2C_South_Yarmouth_MA.jpg/640px-Bass_River%2C_South_Yarmouth_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Southbridge", Address = "6 Larochelle Way", Latitude = 42.07704, Longitude = -72.033211, Town = "Southbridge", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Southbridge_Town_Hall.jpg/358px-Southbridge_Town_Hall.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Springfield", Address = "165 Liberty Street", Latitude = 42.108267, Longitude = -72.593402, Town = "Springfield", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/8b/Avenue_of_States%2C_The_Big_E%2C_West_Springfield_MA.jpg/320px-Avenue_of_States%2C_The_Big_E%2C_West_Springfield_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Taunton", Address = "1 Washington Street", Latitude = 41.902468, Longitude = -71.099749, Town = "Taunton", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/4/48/Taunton_Green_Statue.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Watertown", Address = "550 Arsenal Street", Latitude = 42.363053, Longitude = -71.158113, Town = "Watertown", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Main_Street_Watertown_MA_2.jpg/320px-Main_Street_Watertown_MA_2.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Wilmington", Address = "355 Middlesex Avenue", Latitude = 42.566769, Longitude = -71.159566, Town = "Wilmington", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Church_Street%2C_Wilmington_MA.jpg/320px-Church_Street%2C_Wilmington_MA.jpg" });
        //    branchList.Add(new Branch() { DisplayName = "Worcester", Address = "611 Main Street", Latitude = 42.260149, Longitude = -71.804329, Town = "Worcester", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/90/Downtown_Worcester%2C_Massachusetts.jpg/640px-Downtown_Worcester%2C_Massachusetts.jpg" });

        //}


        //private static async Task RefreshWaitTimes()
        //{
        //    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        //    string response = await client.GetStringAsync("http://www.massdot.state.ma.us/feeds/qmaticxml/qmaticXML.aspx");

        //    // Convert the response into an XML document
        //    var xmlDoc = XDocument.Load((new System.IO.StringReader(response)));

        //    List<RMVBranch> updatedBranchSet = new List<RMVBranch>();
        //    foreach (var item in xmlDoc.Descendants("branch"))
        //    {
        //        string town = (string)item.Element("town");
        //        string licensing = (string)item.Element("licensing");
        //        string registration = (string)item.Element("registration");

        //        // Make sure our branch collection contains this town
        //        if (!branchList.Any(c => c.Town == town)) continue;

        //        // Get this existing branch record
        //        var existingBranch = branchList.First(c => c.Town == (string)item.Element("town"));

        //        // If wait times have changed, update
        //        existingBranch.LicensingWait = ((string)item.Element("licensing"));
        //        existingBranch.RegistrationWait = ((string)item.Element("registration"));

        //        updatedBranchSet.Add(existingBranch);
        //    }

        //    branchList = updatedBranchSet;
        //}

    }
}