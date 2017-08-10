using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Calendar.v3;


namespace MagicMirrorApi.Logic
{
    public class GoogleCalendarAuth
    {
        /// <summary>
        /// Authenticating to Google using a Service account
        /// Documentation: https://developers.google.com/accounts/docs/OAuth2#serviceaccount
        /// </summary>
        /// <param name = "serviceAccountEmail" > From Google Developer console https://console.developers.google.com</param>
        /// <param name = "key" > Key from with in json file
        /// </param>
        /// <returns>CalendarService used to make requests against the Google Calendar API</returns>
        public static CalendarService AuthenticateServiceAccountFromKey(string serviceAccountEmail, string key)
        {
            try
            {
                // Check that the required parameter values are supplied
                if (string.IsNullOrEmpty(key))
                    throw new Exception("Key is required.");
                if (string.IsNullOrEmpty(serviceAccountEmail))
                    throw new Exception("ServiceAccountEmail is required.");

                // We only need Read permission to Google Calendar. We will not be creating or altering events
                string[] scopes = new string[] { CalendarService.Scope.CalendarReadonly };

                // Create new credentials with the given serviceAccountEmail and private key
                var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = scopes
                }.FromPrivateKey(key));

                // Create and return a Calendar service using the credentials created above
                return new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Calendar Authentication Sample",
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create service account CalendarService failed" + ex.Message);
                throw new Exception("CreateServiceAccountCalendarServiceFailed", ex);
            }
        }
    }
}