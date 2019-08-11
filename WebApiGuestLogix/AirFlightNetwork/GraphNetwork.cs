using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiGuestLogix.Data;
using WebApiGuestLogix.Exceptions;
using WebApiGuestLogix.Models;

namespace WebApiGuestLogix.AirFlightNetwork
{
    public class GraphNetwork : IGraphNetwork
    {
        private readonly ILoader _loader;

        public GraphNetwork(ILoader loader)
        {
            _loader = loader;

            foreach (var vertex in _loader.Airports)
                AddVertex(vertex);

            foreach (var edge in _loader.Routes)
                AddEdge(edge);
        }

        public Dictionary<AirportModel,HashSet<Tuple<AirportModel, AirlineModel>>> AdjacencyList { get; } = new Dictionary<AirportModel, HashSet<Tuple<AirportModel, AirlineModel>>>(
            new AirportModel.EqualityComparer());

        private void AddVertex(AirportModel vertex)
        {
            AdjacencyList[vertex] = new HashSet<Tuple<AirportModel, AirlineModel>>();
        }

        private void AddEdge(RouteModel edge)
        {
            var searchOrigin = AdjacencyList.FirstOrDefault(a => a.Key.IATA3 == edge.Origin.IATA3).Key;

            var searchDestination = AdjacencyList.FirstOrDefault(a => a.Key.IATA3 == edge.Destination.IATA3).Key;

            if (searchOrigin != null && searchDestination != null)
            {
                AdjacencyList[edge.Origin].Add(Tuple.Create(edge.Destination, edge.Airline));
            }
        }
    }
}