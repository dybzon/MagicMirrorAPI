using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using MagicMirrorApi.Models;
using Newtonsoft.Json;

namespace MagicMirrorApi.Logic
{
    public class GoogleCalendar
    {
        public static async Task<List<Event>> GetUpcomingCalendarEvents(string serviceAccountEmail, string keyFileName,
            string calendarId, int maxResults)
        {
            //The values of the Google Service Account will be read from the .json key file, which should be available to the project
            GoogleServiceAccount keyValues;

            using (StreamReader r = new StreamReader(keyFileName))
            {
                string json = r.ReadToEnd();
                keyValues = JsonConvert.DeserializeObject<GoogleServiceAccount>(json);
            }

            CalendarService service = GoogleCalendarAuth.AuthenticateServiceAccountFromKey(serviceAccountEmail, keyValues.private_key);

            // Define parameter values of the Google Calendar request
            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = maxResults;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // Execute the request
            var events = await request.ExecuteAsync();

            //Add the resulting events to a list (split this into a separate method)
            var eventList = new List<Event>();
            if (events.Items != null && events.Items.Count > 0)
            {
                eventList = events.Items.Select(i =>
                       new Event
                       {
                           StartDate = i.Start?.DateTime ?? DateTime.MinValue,
                           EndDate = i.End?.DateTime ?? DateTime.MinValue,
                           EventDescription = i.Description,
                           EventSummary = i.Summary
                       }
                ).ToList();
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            return eventList;
        }
    }
}