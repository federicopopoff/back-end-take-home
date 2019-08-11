using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiGuestLogix.Data;
using WebApiGuestLogix.Exceptions;
using WebApiGuestLogix.Models;

namespace WebApiGuestLogix.AirFlightNetwork
{
    public class FlightNetwork : IFlightNetwork
    {
        private readonly IGraphNetwork _graphNetwork;

        public IEnumerable<AirlineModel> airlines { get; set; }
        public IEnumerable<AirportModel> airports { get; set; }
        public IEnumerable<RouteModel> routes { get; set; }

        public FlightNetwork(ILoader data, IGraphNetwork graph)
        {
            this.airlines = data.Airlines;
            this.airports = data.Airports;
            this.routes = data.Routes;

            this._graphNetwork = graph;
        }

        public List<RouteModel> ShortestRoute(string origin, string destination)
        {
            if (!airports.Any(a => a.IATA3 == origin))
                throw new OriginNotFoundException();
            if (!airports.Any(a => a.IATA3 == destination))
                throw new DestinationNotFoundException();

            AirportModel airportOrigin = airports.FirstOrDefault(a => a.IATA3 == origin);
            AirportModel airportDestination = airports.FirstOrDefault(a => a.IATA3 == destination);

            var shortestPath = this.ShortestPathFunction(_graphNetwork, airportOrigin);
            var AirportsPath = shortestPath(airportDestination);
            List<RouteModel> finalPath = this.SetAirlines(AirportsPath);

            return finalPath;
        }

        private List<RouteModel> SetAirlines(IEnumerable<AirportModel> airportsPath)
        {
            List<RouteModel> finalPaths = new List<RouteModel>();
            for (int i = 0; i < airportsPath.Count(); i++)
            {
                if (i == airportsPath.Count() - 1)
                    continue;

                var airline = routes.FirstOrDefault(r => r.Origin.IATA3 == airportsPath.ToList()[i].IATA3
                                && r.Destination.IATA3 == airportsPath.ToList()[i + 1].IATA3);

                finalPaths.Add(airline);
            }
            return finalPaths;
        }

        private Func<AirportModel, IEnumerable<AirportModel>> ShortestPathFunction(IGraphNetwork graph, AirportModel start)
        {
            var previous = new Dictionary<AirportModel, AirportModel>(new AirportModel.EqualityComparer());
            var queue = new Queue<AirportModel>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor.Item1))
                        continue;

                    previous[neighbor.Item1] = vertex;
                    queue.Enqueue(neighbor.Item1);
                }
            }

            Func<AirportModel, IEnumerable<AirportModel>> shortestPath = v =>
            {
                var path = new List<AirportModel> { };

                var current = v;
                while (!current.Equals(start) && previous.Count > 0)
                {
                    path.Add(current);

                    AirportModel previousValue;
                    bool exist = previous.TryGetValue(current, out previousValue);

                    if(!exist)
                        throw new NoRouteFoundException();
                    
                    if (previous[current] != null)
                        current = previous[current];
                };

                path.Add(start);
                path.Reverse();

                return path;
            };

            return shortestPath;
        }

    }

}