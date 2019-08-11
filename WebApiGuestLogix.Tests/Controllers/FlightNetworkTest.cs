using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiGuestLogix.AirFlightNetwork;
using WebApiGuestLogix.Controllers;
using WebApiGuestLogix.Models;
using WebApiGuestLogix.Exceptions;
using WebApiGuestLogix.Data;

namespace WebApiGuestLogix.Tests.Controllers
{
    [TestClass]
    public class FlightNetworkTest
    {
        FlightNetwork flightNet;

        [TestInitialize]
        public void TestInitialize()
        {
            Loader data = new Loader("test");
            GraphNetwork graph = new GraphNetwork(data);
            flightNet = new FlightNetwork(data, graph);
        }

        [TestMethod()]
        public void GetShortestRouteValidRoute1()
        {
            var result = flightNet.ShortestRoute("YYZ", "LAX");

            List<RouteModel> routes = new List<RouteModel>();
            routes.Add(new RouteModel()
            {
                 Airline = new AirlineModel() { Country = "Canada", Name = "Air Canada", ThreeDigitCode = "ACA", TwoDigitCode = "AC" },
                 Origin = new AirportModel() { City = "Toronto", Country = "Canada", IATA3 = "YYZ", Latitude = 0, Longitude = 0, Name = "Lester B. Pearson International Airport" },
                 Destination = new AirportModel() { City = "New York", Country = "United States", IATA3 = "JFK", Latitude = 0, Longitude = 0, Name = "John F Kennedy International Airport" }
            });

            routes.Add(new RouteModel()
            {
                Airline = new AirlineModel() { Country = "United States", Name = "United Airlines", ThreeDigitCode = "UAL", TwoDigitCode = "UA" },
                Origin = new AirportModel() { City = "New York", Country = "United States", IATA3 = "JFK", Latitude = 0, Longitude = 0, Name = "John F Kennedy International Airport" },
                Destination = new AirportModel() { City = "Los Angeles", Country = "United States", IATA3 = "LAX", Latitude = 0, Longitude = 0, Name = "Los Angeles International Airport" }
            });

            Assert.AreEqual(routes[0].Origin.IATA3, result[0].Origin.IATA3);
            Assert.AreEqual(routes[0].Destination.IATA3, result[0].Destination.IATA3);

            Assert.AreEqual(routes[1].Origin.IATA3, result[1].Origin.IATA3);
            Assert.AreEqual(routes[1].Destination.IATA3, result[1].Destination.IATA3);

        }

        [TestMethod()]
        public void GetShortestRouteValidRoute2()
        {
            var result = flightNet.ShortestRoute("YYZ", "JFK");

            List<RouteModel> routes = new List<RouteModel>();
            routes.Add(new RouteModel()
            {
                Airline = new AirlineModel() { Country = "Canada", Name = "Air Canada", ThreeDigitCode = "ACA", TwoDigitCode = "AC" },
                Origin = new AirportModel() { City = "Toronto", Country = "Canada", IATA3 = "YYZ", Latitude = 0, Longitude = 0, Name = "Lester B. Pearson International Airport" },
                Destination = new AirportModel() { City = "New York", Country = "United States", IATA3 = "JFK", Latitude = 0, Longitude = 0, Name = "John F Kennedy International Airport" }
            });

            Assert.AreEqual(routes[0].Origin.IATA3, result[0].Origin.IATA3);
            Assert.AreEqual(routes[0].Destination.IATA3, result[0].Destination.IATA3);
        }

        [TestMethod]
        [ExpectedException(typeof(OriginNotFoundException))]
        public void GetShortestRoute_OriginNotFound_KnownDestination()
        {
            flightNet.ShortestRoute("XXX", "JFK");
        }

        [TestMethod]
        [ExpectedException(typeof(DestinationNotFoundException))]
        public void GetShortestRoute_KnownOrigin_DestinationNotFound()
        {
            flightNet.ShortestRoute("YYZ", "XXX");
        }

        [TestMethod]
        [ExpectedException(typeof(NoRouteFoundException))]
        public void GetShortestRoute_NoRouteFound()
        {
            flightNet.ShortestRoute("YYZ", "ORD");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            flightNet = null;
        }


    }
}
