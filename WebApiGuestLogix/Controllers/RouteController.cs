using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGuestLogix.Data;
using WebApiGuestLogix.Exceptions;
using WebApiGuestLogix.AirFlightNetwork;



namespace WebApiGuestLogix.Controllers
{
    public class RouteController : ApiController
    {
        private readonly IFlightNetwork _flightNetwork;

        public RouteController(IFlightNetwork flightNetwork)
        {
            _flightNetwork = flightNetwork;
        }

        [HttpGet]
        [Route("api/GetShortestRoute")]
        public IHttpActionResult GetShortestRoute(string origin, string destination)
        {
            //https://localhost:44376/api/GetShortestRoute?origin=YYZ&destination=JFK
            //WebConfigurationManager.AppSettings["DataFolderName"]

            try
            {
                var response = _flightNetwork.ShortestRoute(origin, destination);

                return Ok(response);
            }
            catch (CustomException ce)
            {
                return Content(HttpStatusCode.NotFound, ce.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
