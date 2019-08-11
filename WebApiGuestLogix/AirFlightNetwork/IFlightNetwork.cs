using System.Collections.Generic;
using WebApiGuestLogix.Models;

namespace WebApiGuestLogix.AirFlightNetwork
{
    public interface IFlightNetwork
    {
        List<RouteModel> ShortestRoute(string origin, string destination);
    }
}