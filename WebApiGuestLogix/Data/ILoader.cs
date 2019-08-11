using System.Collections.Generic;
using WebApiGuestLogix.Models;

namespace WebApiGuestLogix.Data
{
    public interface ILoader
    {
        IEnumerable<AirlineModel> Airlines { get; set; }
        IEnumerable<AirportModel> Airports { get; set; }
        IEnumerable<RouteModel> Routes { get; set; }
    }
}