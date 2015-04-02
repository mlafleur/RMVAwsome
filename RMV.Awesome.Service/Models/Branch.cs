namespace RMV.Awesome.API.Models
{
    public class Branch
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
}