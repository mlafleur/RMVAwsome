//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMV.Awesome.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CurrentWaitTime
    {
        public string Town { get; set; }
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double LicensingWait { get; set; }
        public double RegistrationWait { get; set; }
        public System.DateTimeOffset SampleTime { get; set; }
        public string ImagePath { get; set; }
    }
}
