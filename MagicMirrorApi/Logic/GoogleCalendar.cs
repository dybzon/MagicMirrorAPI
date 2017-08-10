using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Google.Apis.Calendar.v3;
using MagicMirrorApi.Models;
using Newtonsoft.Json;

namespace MagicMirrorApi.Logic
{
    public class GoogleCalendar
    {
        public static List<Event> GetUpcomingCalendarEvents(string serviceAccountEmail, string keyFileName,
            string calendarId, int maxResults)
        {
            //Sample values:
            //string serviceAccountEmail = @"magicmirroraccount@psychic-order-175711.iam.gserviceaccount.com";
            //string keyFileName = "My Project-5ebf1707235e.json";
            //string calendarId = @"rasmusdybkjaer@gmail.com"; //"primary"
            //int maxResults = 10;

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
            Google.Apis.Calendar.v3.Data.Events events = request.Execute();
            
            //Add the resulting events to a list (split this into a separate method)
            List<Event> eventList = new List<Event>();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    Event e = new Event();
                    string startDate = eventItem.Start.DateTime.ToString(); //Sample date format: 30-08-2017 20:35:00 (dd-mm-yyyy hh:mm:ss)
                    if (String.IsNullOrEmpty(startDate))
                    {
                        startDate = eventItem.Start.Date;
                    }
                    e.StartDate = DateTime.ParseExact(startDate, "dd-MM-yyyy HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);

                    string endDate = eventItem.End.DateTime.ToString();
                    if (String.IsNullOrEmpty(endDate))
                    {
                        endDate = eventItem.Start.Date;
                    }
                    e.EndDate = DateTime.ParseExact(endDate, "dd-MM-yyyy HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);

                    e.EventDescription = eventItem.Description;
                    e.EventSummary = eventItem.Summary;
                    eventList.Add(e);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            return eventList;
        }
    }
}