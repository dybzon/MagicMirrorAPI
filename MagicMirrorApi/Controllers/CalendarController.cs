using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MagicMirrorApi.Logic;
using MagicMirrorApi.Models;

namespace MagicMirrorApi.Controllers
{
    public class CalendarController : ApiController
    {
        /// <summary>
        /// Get events for the specified calendar
        /// GET api/calendar/rasmusdybkjaer@gmail.com/
        /// </summary>
        /// <param name="calendarId"></param>
        /// <returns>A list of events</returns>
        [HttpGet]
        public IHttpActionResult GetEventsForCalendar([FromUri] string calendarId)
        {
            //string calendarId = @"rasmusdybkjaer@gmail.com"; //"primary"
            string serviceAccountEmail = @"magicmirroraccount@psychic-order-175711.iam.gserviceaccount.com";
            string keyFileName = @"C:\Users\rad\Downloads\My Project-5ebf1707235e.json";
            int maxResults = 10;
            IEnumerable<Event> events =
                GoogleCalendar.GetUpcomingCalendarEvents(serviceAccountEmail, keyFileName, calendarId, maxResults);

            return Ok(events);
        }

        /// <summary>
        /// Get events for all calendars available in this API
        /// GET api/calendar/
        /// </summary>
        /// <returns>A list of events</returns>
        [HttpGet]
        public IHttpActionResult GetEvents()
        {
            string calendarId = @"rasmusdybkjaer@gmail.com"; //"primary"
            string serviceAccountEmail = @"magicmirroraccount@psychic-order-175711.iam.gserviceaccount.com";
            string keyFileName = @"C:\Users\rad\Downloads\My Project-5ebf1707235e.json";
            int maxResults = 10;
            IEnumerable<Event> events =
                GoogleCalendar.GetUpcomingCalendarEvents(serviceAccountEmail, keyFileName, calendarId, maxResults);

            return Ok(events);
        }
    }
}
