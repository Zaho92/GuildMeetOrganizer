using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.Models.ApiHelper
{
    internal static class ApiUriBuilder
    {
        public const string ApiBaseUrl = @"https://www.altrogge.online:10010";
        public enum ApiControllers
        {
            User,
            RightsTemplates
        }

        public static string GetBaseApiPath(ApiControllers forController)
        {
            return ApiBaseUrl + "/" + forController.ToString();
        }

        public static Uri BuildApiUri(ApiControllers apiController, string apiCommandPath, Dictionary<string, string> PropertyList = null)
        {
            string uriString = GetBaseApiPath(apiController);
            uriString += (!ApiBaseUrl.EndsWith("/") ? "/" : "") + apiCommandPath;
            if (PropertyList != null && PropertyList.Count > 0)
            {
                string propertyString = "?";
                foreach (var nameValuePair in PropertyList)
                {
                    if (!propertyString.EndsWith("?")) 
                    { 
                        propertyString += "&"; 
                    }
                    propertyString += nameValuePair.Key + "=" + nameValuePair.Value;
                }
                uriString += propertyString;
            }
            return new Uri(uriString);
        }
    }
}
