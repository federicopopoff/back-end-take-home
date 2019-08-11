using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApiGuestLogix.Models
{
    [DataContract]
    public class RouteModel
    {
        [DataMember(Order = 0)]
        public AirlineModel Airline { get; set; }

        [DataMember(Order = 1)]
        public AirportModel Origin { get; set; }

        [DataMember(Order = 2)]
        public AirportModel Destination { get; set; }
    }
}