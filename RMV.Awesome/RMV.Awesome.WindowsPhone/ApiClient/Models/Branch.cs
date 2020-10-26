﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace RMV.Awesome.Api.Models
{
    public partial class Branch
    {
        private string _address;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }
        
        private string _displayAddress;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public string DisplayAddress
        {
            get { return this._displayAddress; }
            set { this._displayAddress = value; }
        }
        
        private string _displayName;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public string DisplayName
        {
            get { return this._displayName; }
            set { this._displayName = value; }
        }
        
        private double? _distance;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public double? Distance
        {
            get { return this._distance; }
            set { this._distance = value; }
        }
        
        private string _imagePath;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public string ImagePath
        {
            get { return this._imagePath; }
            set { this._imagePath = value; }
        }
        
        private double? _latitude;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public double? Latitude
        {
            get { return this._latitude; }
            set { this._latitude = value; }
        }
        
        private string _licensingWait;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public string LicensingWait
        {
            get { return this._licensingWait; }
            set { this._licensingWait = value; }
        }
        
        private double? _longitude;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public double? Longitude
        {
            get { return this._longitude; }
            set { this._longitude = value; }
        }
        
        private string _registrationWait;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public string RegistrationWait
        {
            get { return this._registrationWait; }
            set { this._registrationWait = value; }
        }
        
        private string _town;
        
        /// <summary>
        /// Optional.
        /// </summary>
        public string Town
        {
            get { return this._town; }
            set { this._town = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the Branch class.
        /// </summary>
        public Branch()
        {
        }
        
        /// <summary>
        /// Deserialize the object
        /// </summary>
        public virtual void DeserializeJson(JToken inputObject)
        {
            if (inputObject != null && inputObject.Type != JTokenType.Null)
            {
                JToken addressValue = inputObject["Address"];
                if (addressValue != null && addressValue.Type != JTokenType.Null)
                {
                    this.Address = ((string)addressValue);
                }
                JToken displayAddressValue = inputObject["DisplayAddress"];
                if (displayAddressValue != null && displayAddressValue.Type != JTokenType.Null)
                {
                    this.DisplayAddress = ((string)displayAddressValue);
                }
                JToken displayNameValue = inputObject["DisplayName"];
                if (displayNameValue != null && displayNameValue.Type != JTokenType.Null)
                {
                    this.DisplayName = ((string)displayNameValue);
                }
                JToken distanceValue = inputObject["Distance"];
                if (distanceValue != null && distanceValue.Type != JTokenType.Null)
                {
                    this.Distance = ((double)distanceValue);
                }
                JToken imagePathValue = inputObject["ImagePath"];
                if (imagePathValue != null && imagePathValue.Type != JTokenType.Null)
                {
                    this.ImagePath = ((string)imagePathValue);
                }
                JToken latitudeValue = inputObject["Latitude"];
                if (latitudeValue != null && latitudeValue.Type != JTokenType.Null)
                {
                    this.Latitude = ((double)latitudeValue);
                }
                JToken licensingWaitValue = inputObject["LicensingWait"];
                if (licensingWaitValue != null && licensingWaitValue.Type != JTokenType.Null)
                {
                    this.LicensingWait = ((string)licensingWaitValue);
                }
                JToken longitudeValue = inputObject["Longitude"];
                if (longitudeValue != null && longitudeValue.Type != JTokenType.Null)
                {
                    this.Longitude = ((double)longitudeValue);
                }
                JToken registrationWaitValue = inputObject["RegistrationWait"];
                if (registrationWaitValue != null && registrationWaitValue.Type != JTokenType.Null)
                {
                    this.RegistrationWait = ((string)registrationWaitValue);
                }
                JToken townValue = inputObject["Town"];
                if (townValue != null && townValue.Type != JTokenType.Null)
                {
                    this.Town = ((string)townValue);
                }
            }
        }
    }
}