using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApiGuestLogix.Models
{
    [DataContract]
    public class AirportModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string IATA3 { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public class EqualityComparer : IEqualityComparer<AirportModel>
        {

            public bool Equals(AirportModel x, AirportModel y)
            {
                return x.IATA3 == y.IATA3;
            }

            public int GetHashCode(AirportModel obj)
            {
                return obj.GetHashCode() ^ obj.GetHashCode();
            }
        }
    }
}