using System;
using System.Collections.Generic;
using WebApiGuestLogix.Models;

namespace WebApiGuestLogix.AirFlightNetwork
{
    public interface IGraphNetwork
    {
        Dictionary<AirportModel, HashSet<Tuple<AirportModel, AirlineModel>>> AdjacencyList { get; }
    }
}