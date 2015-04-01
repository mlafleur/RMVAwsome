using System;

namespace RMV.Awesome
{
    public class Location
    {
        private static double _deviceLatitude = 0;
        private static double _deviceLogitude = 0;

        public static double DeviceLatitude
        {
            get { return _deviceLatitude; }
            set { _deviceLatitude = value; }
        }

        public static double DeviceLogitude
        {
            get { return _deviceLogitude; }
            set { _deviceLogitude = value; }
        }


        public static double CalculateDistance(double BranchLatitude, double BranchLongitude)
        {
            
            /*
             * Based on code by Gary Dryden on CodeProject
             * http://www.codeproject.com/Articles/12269/Distance-between-locations-using-latitude-and-long
             *
             * The Haversine formula according to Dr. Math.
             * http://mathforum.org/library/drmath/view/51879.html
             * dlon = lon2 - lon1
             * dlat = lat2 - lat1
             * a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
             * c = 2 * atan2(sqrt(a), sqrt(1-a))
             * d = R * c
             * Where dlon is the change in longitude
             *       dlat is the change in latitude
             *       c is the great circle distance in Radians.
             *       R is the radius of a spherical Earth.
             * The locations of the two points in spherical coordinates
             * (longitude and latitude) are lon1,lat1 and lon2, lat2.
            */

            double dDistance = Double.MinValue;
            double dLat1InRad = DeviceLatitude * (Math.PI / 180.0);
            double dLong1InRad = DeviceLogitude * (Math.PI / 180.0);
            double dLat2InRad = BranchLatitude * (Math.PI / 180.0);
            double dLong2InRad = BranchLongitude * (Math.PI / 180.0);
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

            return Math.Round(dDistance, 1);
        }
    }
}