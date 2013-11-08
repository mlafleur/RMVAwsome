using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMV.Awesome.PCL.Model
{
    /* Please note: This class is a bad idea! :)
     * We're using this here to provide the data not populated by the RMV XML Feed for the 
     * sake of this demo. In a real-world app, this data should be coming from the cloud. 
     * It would be a great use of an Azure Table for example. This would allow you to update 
     * the Branch data without having to update the App itself. */
    public class StaticBranchData
    {

        public static System.Collections.ObjectModel.ObservableCollection<Branch> Data
        {
            get
            {
                var details = new System.Collections.ObjectModel.ObservableCollection<Branch>();

                details.Add(new Branch() { Title = "Attleboro", Address = "75 Park Street", Latitude = 41.943778, Longitude = -71.279825, Town = "Attleboro", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Attleboro_MA_Downtown.jpg/320px-Attleboro_MA_Downtown.jpg" });
                details.Add(new Branch() { Title = "Boston", Address = "630 Washington Street", Latitude = 42.352181, Longitude = -71.062456, Town = "Washington", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Faneuil_Hall_Boston_Massachusetts.JPG/332px-Faneuil_Hall_Boston_Massachusetts.JPG" });
                details.Add(new Branch() { Title = "Braintree", Address = "10 Plain Street", Latitude = 42.196212, Longitude = -71.003572, Town = "Braintree", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Braintree_Town_Hall%2C_MA.jpg/320px-Braintree_Town_Hall%2C_MA.jpg" });
                details.Add(new Branch() { Title = "Brockton", Address = "490 Forest Ave", Latitude = 42.070619, Longitude = -71.041389, Town = "Brockton", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/85/BrocktonCityHall.jpg/370px-BrocktonCityHall.jpg" });
                details.Add(new Branch() { Title = "Chicopee", Address = "1011 Chicopee Street", Latitude = 42.191904, Longitude = -72.600594, Town = "Chicopee", ImagePath = "http://upload.wikimedia.org/wikipedia/en/thumb/c/c6/ChicopeeRiver.jpg/320px-ChicopeeRiver.jpg" });
                details.Add(new Branch() { Title = "Danvers", Address = "100 Independence Way", Latitude = 42.55290641370957, Longitude = -70.93985795974731, Town = "Danvers", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/8e/Peabody_Institute_Library_of_Danvers.JPG/320px-Peabody_Institute_Library_of_Danvers.JPG" });
                details.Add(new Branch() { Title = "Easthampton", Address = "116 Pleasant St", Latitude = 42.272979, Longitude = -72.663482, Town = "East Hampton", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/b/bf/Mount_Tom_Easthampton_MA.JPG/320px-Mount_Tom_Easthampton_MA.JPG" });
                details.Add(new Branch() { Title = "Fall River", Address = "203 Plymouth Avenue", Latitude = 41.697317, Longitude = -71.149106, Town = "Fall River", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/2/21/Downtown_Fall_River.jpg" });
                details.Add(new Branch() { Title = "Greenfield", Address = "18 Miner Street", Latitude = 42.585972, Longitude = -72.619682, Town = "Greenfield", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/e/ed/Greenfield_Town_Hall.jpg/411px-Greenfield_Town_Hall.jpg" });
                details.Add(new Branch() { Title = "Haverhill", Address = "4 Summer Street", Latitude = 42.777554, Longitude = -71.077176, Town = "Haverhill", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/Haverhill%2C_MA.jpg/320px-Haverhill%2C_MA.jpg" });
                details.Add(new Branch() { Title = "Lawrence", Address = "73 Winthrop Avenue", Latitude = 42.689929, Longitude = -71.149626, Town = "Lawrence", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/e/ee/Ayer_Mill_View.jpg" });
                details.Add(new Branch() { Title = "Leominster", Address = "80 Erdman Way", Latitude = 42.547593, Longitude = -71.757933, Town = "Leominster", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/a/a0/Monument_Square_Leominster.jpg" });
                details.Add(new Branch() { Title = "Lowell", Address = "  77 Middlesex Street", Latitude = 42.641479, Longitude = -71.310108, Town = "Lowell", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/4c/Front_of_boott_mill.jpg/320px-Front_of_boott_mill.jpg" });
                details.Add(new Branch() { Title = "Martha's Vineyard", Address = "11 A Street", Latitude = 41.391582, Longitude = -70.603321, Town = "Marthas Vineyard", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Gay_Head_cliffs_MV.JPG/320px-Gay_Head_cliffs_MV.JPG" });
                details.Add(new Branch() { Title = "Milford", Address = "14 Beach Street", Latitude = 42.142346, Longitude = -71.512389, Town = "Milford", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fe/Milford_Town_Hall%2C_MA.jpg/320px-Milford_Town_Hall%2C_MA.jpg" });
                details.Add(new Branch() { Title = "Nantucket", Address = "Broad Street", Latitude = 41.28554, Longitude = -70.097874, Town = "Nantucket", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/5/5f/Nantucket-08-2004.jpg/320px-Nantucket-08-2004.jpg" });
                details.Add(new Branch() { Title = "Natick", Address = "Massachusetts Turnpike Natick Eastbound Service Center", Latitude = 42.311156, Longitude = -71.364892, Town = "Natick", ImagePath = "http://upload.wikimedia.org/wikipedia/en/thumb/1/11/Natick-common.jpg/450px-Natick-common.jpg" });
                details.Add(new Branch() { Title = "New Bedford", Address = "278 Union Street", Latitude = 41.633748, Longitude = -70.929116, Town = "New Bedford", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/f/fc/New_Bedford%2C_Massachusetts-view_from_harbor.jpeg" });
                details.Add(new Branch() { Title = "North Adams", Address = "33 Main Street", Latitude = 42.69956, Longitude = -73.113883, Town = "N Adams", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/7/7b/Main_Street_North_Adams.jpg/320px-Main_Street_North_Adams.jpg" });
                details.Add(new Branch() { Title = "Pittsfield", Address = "333 East Street", Latitude = 42.447641, Longitude = -73.247607, Town = "Pittsfield", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/2/28/Pittsfield_City_Hall.JPG/320px-Pittsfield_City_Hall.JPG" });
                details.Add(new Branch() { Title = "Plymouth", Address = "40 Industrial Park Road", Latitude = 41.960272, Longitude = -70.700532, Town = "Plymouth", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/c/ca/Monument_to_the_Forefathers_1.jpg/336px-Monument_to_the_Forefathers_1.jpg" });
                details.Add(new Branch() { Title = "Revere", Address = "9 Everett Street", Latitude = 42.409733, Longitude = -71.00108, Town = "Revere", ImagePath = "http://ImagePathupload.wikimedia.org/wikipedia/commons/thumb/c/c5/Revere_City_Hall.JPG/320px-Revere_City_Hall.JPG" });
                details.Add(new Branch() { Title = "Roslindale", Address = "8 Cummins Highway", Latitude = 42.286383, Longitude = -71.127759, Town = "Roslindale", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/2/2a/Annunciation-cathedral-night.png/320px-Annunciation-cathedral-night.png" });
                details.Add(new Branch() { Title = "South Yarmouth", Address = "1084 Route 28", Latitude = 41.661432, Longitude = -70.202079, Town = "Yarmouth", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/98/Yarmouth_MA_Town_Offices.jpg/320px-Yarmouth_MA_Town_Offices.jpg" });
                details.Add(new Branch() { Title = "Southbridge", Address = "6 Larochelle Way", Latitude = 42.07704, Longitude = -72.033211, Town = "Southbridge", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Southbridge_Town_Hall.jpg/358px-Southbridge_Town_Hall.jpg" });
                details.Add(new Branch() { Title = "Springfield", Address = "165 Liberty Street", Latitude = 42.108267, Longitude = -72.593402, Town = "Springfield", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/8b/Avenue_of_States%2C_The_Big_E%2C_West_Springfield_MA.jpg/320px-Avenue_of_States%2C_The_Big_E%2C_West_Springfield_MA.jpg" });
                details.Add(new Branch() { Title = "Taunton", Address = "1 Washington Street", Latitude = 41.902468, Longitude = -71.099749, Town = "Taunton", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/4/48/Taunton_Green_Statue.jpg" });
                details.Add(new Branch() { Title = "Watertown", Address = "550 Arsenal Street", Latitude = 42.363053, Longitude = -71.158113, Town = "Watertown", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Main_Street_Watertown_MA_2.jpg/320px-Main_Street_Watertown_MA_2.jpg" });
                details.Add(new Branch() { Title = "Wilmington", Address = "355 Middlesex Avenue", Latitude = 42.566769, Longitude = -71.159566, Town = "Wilmington", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Church_Street%2C_Wilmington_MA.jpg/320px-Church_Street%2C_Wilmington_MA.jpg" });
                details.Add(new Branch() { Title = "Worcester", Address = "611 Main Street", Latitude = 42.260149, Longitude = -71.804329, Town = "Worcester", ImagePath = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/90/Downtown_Worcester%2C_Massachusetts.jpg/640px-Downtown_Worcester%2C_Massachusetts.jpg" });

                return details;

            }
        }
    }
}
