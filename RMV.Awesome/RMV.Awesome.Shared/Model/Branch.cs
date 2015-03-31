namespace RMV.Awesome.Model
{
    public class Branch : System.ComponentModel.INotifyPropertyChanged
    {
        // Set the default values for wait times to "Unknown" until we pull
        // live values from the RMV XML feed
        private string _licensingWait = "Unknown";

        private string _registrationWait = "Unknown";

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public string Address { get; set; }

        public string DisplayAddress
        {
            get { return Address + ", " + Town + ", MA"; }
        }

        public string Id { get; set; }

        public string ImagePath { get; set; }

        public double Latitude { get; set; }

        public string LicensingWait
        {
            get { return _licensingWait; }
            set
            {
                _licensingWait = value;

                /* We are firing off to notifications here.
                 * The first is for this property for obvious
                 * reasons. The second is for Subtitle which
                 * consumes this propery and we therefore
                 * want to tell the UI to update controls that
                 * use Subtitle as well */
                NotifyChanged("LicensingWait");
                NotifyChanged("Subtitle");
            }
        }

        public double Distance { get; set; }

        public double Longitude { get; set; }

        public string RegistrationWait
        {
            get { return _registrationWait; }
            set
            {
                _registrationWait = value;

                /* We are firing off to notifications here.
                * The first is for this property for obvious
                * reasons. The second is for Subtitle which
                * consumes this propery and we therefore
                * want to tell the UI to update controls that
                * use Subtitle as well */
                NotifyChanged("RegistrationWait");
                NotifyChanged("Subtitle");
            }
        }

        public string Subtitle
        {
            get
            {
                // Return a summary string for Licensing & Registration wait times
                return string.Format("Licensing Wait: {0}\nRegistration Wait: {1}",
                    LicensingWait, RegistrationWait);
            }
        }

        public string Title { get; set; }

        public string Town { get; set; }

        /// <summary>
        /// Notifies the UI that a given public property in this class has changed so that
        /// it can then update any controls bound to that property.
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        public void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        /* The following properties don't change while the app is running
         * so we don't need to fire NotifyChanged on these. We can use the
         * short-hand method of creating this properties */
        /* The following properties change based on the XML feed we're pulling
         * and therefore need to notify the UI when the values have changed. For
         * these properties we need to use the more formal method of property creation */
    }
}