using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApiGuestLogix.Models
{
    [DataContract]
    public class AirlineModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string TwoDigitCode { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string ThreeDigitCode { get; set; }
    }
}