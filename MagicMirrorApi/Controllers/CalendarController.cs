using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MagicMirrorApi.Logic;
using MagicMirrorApi.Models;

namespace MagicMirrorApi.Controllers
{
    [RoutePrefix("api/calendar")]
    public class CalendarController : ApiController
    {
        /// <summary>
        /// Get events for the specified calendar
        /// GET api/calendar/rasmusdybkjaer@gmail.com/
        /// </summary>
        /// <param name="calendarId"></param>
        /// <returns>A list of events</returns>
        [HttpGet]
        [Route("{calendarId}")]
        public async Task<IEnumerable<Event>> GetEventsForCalendar(string calendarId)
        {
            const string serviceAccountEmail = @"magicmirroraccount@psychic-order-175711.iam.gserviceaccount.com";
            var keyFileName = System.Web.Hosting.HostingEnvironment.MapPath(@"~/My Project-5ebf1707235e.json");
            const int maxResults = 10;
            return await GoogleCalendar.GetUpcomingCalendarEvents(serviceAccountEmail, keyFileName, calendarId, maxResults);
        }

        /// <summary>
        /// Get events for all calendars available in this API
        /// GET api/calendar/
        /// </summary>
        /// <returns>A list of events</returns>
        [HttpGet]
        [Route]
        public async Task<IEnumerable<Event>> GetEvents()
        {
            const string calendarId = @"rasmusdybkjaer@gmail.com"; //"primary"
            const string serviceAccountEmail = @"magicmirroraccount@psychic-order-175711.iam.gserviceaccount.com";
            var keyFileName = System.Web.Hosting.HostingEnvironment.MapPath(@"~/My Project-5ebf1707235e.json");
            const int maxResults = 10;
            return await GoogleCalendar.GetUpcomingCalendarEvents(serviceAccountEmail, keyFileName, calendarId, maxResults);
        }
    }
}
