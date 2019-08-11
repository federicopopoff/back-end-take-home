using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using WebApiGuestLogix.Models;

namespace WebApiGuestLogix.Data
{
    public class Loader : ILoader
    {
        private string AirlinesFile = "airlines.csv";
        private string AirportsFile = "airports.csv";
        private string RoutesFile = "routes.csv";
        private string Folder;

        public IEnumerable<AirlineModel> Airlines { get; set; }
        public IEnumerable<AirportModel> Airports { get; set; }
        public IEnumerable<Tuple<string, string>> RoutesTuple { get; set; }
        public IEnumerable<RouteModel> Routes { get; set; }
        public Loader(string folder = "")
        {
            this.Folder = folder;
            this.Airlines = this.GetAllAirlines();
            this.Airports = this.GetAllAirports();
            this.Routes = this.GetRoutes();
        }
        private IEnumerable<AirlineModel> GetAllAirlines()
        {
            var airlines = GetFileFromInternet(AirlinesFile)
                   .Select(fields => fields.Split(','))
                   .Select(fields => new AirlineModel
                   {
                       Name = fields[0].Trim(),
                       TwoDigitCode = fields[1].Trim(),
                       ThreeDigitCode = fields[2].Trim(),
                       Country = fields[3].Trim()
                   });

            return airlines.ToList();
        }
        private IEnumerable<AirportModel> GetAllAirports()
        {
            var airports = GetFileFromInternet(AirportsFile)
                   .Select(fields => fields.Split(','))
                   .Select(fields => new AirportModel
                   {
                       Name = fields[0].Trim(),
                       City = fields[1].Trim(),
                       Country = fields[2].Trim(),
                       IATA3 = fields[3].Trim()
                       //Latitude = double.Parse(fields[4].Trim()),
                       //Longitude = double.Parse(fields[5].Trim())
                   });

            return airports.ToList();
        }
        private IEnumerable<RouteModel> GetRoutes()
        {
            var routes = GetFileFromInternet(RoutesFile)
                  .Select(fields => fields.Split(','))
                  .Select(fields => new RouteModel() {
                      Airline = this.Airlines.FirstOrDefault(n => n.TwoDigitCode == fields[0].Trim()),
                      Origin = this.Airports.Where(n => n.IATA3 == fields[1].Trim()).DefaultIfEmpty(new AirportModel() { IATA3 = fields[1].Trim() }).FirstOrDefault(),
                      Destination = this.Airports.Where(n => n.IATA3 == fields[2].Trim()).DefaultIfEmpty(new AirportModel() { IATA3 = fields[2].Trim() }).FirstOrDefault()
                  });

            return routes.ToList();
        }
        private IEnumerable<Tuple<string, string>> GetAllRoutesTuplaFormat()
        {
            var routes = File.ReadAllLines(GetActualPath(RoutesFile))
                  .Skip(1)
                  .Select(fields => fields.Split(','))
                  .Select(fields => Tuple.Create(fields[1].Trim(), fields[2].Trim()));

            return routes.ToList();
        }
        private string GetActualPath(string filename)
        {
            return AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("WebApiGuestLogix") + 16) + "\\WebApiGuestLogix\\CSV\\" + Folder + "\\" + filename;
        }

        private string[] GetFileFromInternet(string filename)
        {
            List<string> values = new List<string>();

            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://raw.githubusercontent.com/Guestlogix/back-end-take-home/master/data/" + Folder +  "/" + filename);

            using (var reader = new StreamReader(stream, Encoding.Default, true, 1024))
            {
                string headerLine = reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    values.Add(line);
                }
            }

            return values.ToArray();

        }

    }
}