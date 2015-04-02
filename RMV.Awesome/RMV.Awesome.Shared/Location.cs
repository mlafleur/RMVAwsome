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
    }
}