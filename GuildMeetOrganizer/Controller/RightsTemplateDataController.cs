using GuildMeetOrganizer.Helpers;
using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.Models.ApiHelper;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using static GuildMeetOrganizer.Models.ApiHelper.ApiUriBuilder;

namespace GuildMeetOrganizer.Controller
{
    internal class RightsTemplateDataController
    {
        readonly HttpClient _client;
        readonly ApiControllers thisApiController = ApiControllers.RightsTemplates;
        public RightsTemplateDataController()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalVariables.ApiToken);
        }

        const string GetRightsTemplatesUrl = "GetRightsTemplates";
        public ApiResponseObject<List<RightsTemplate>> GetRightsTemplates()
        {
            return GetRightsTemplatesAsync().Result;
        }

        public async Task<ApiResponseObject<List<RightsTemplate>>> GetRightsTemplatesAsync()
        {
            if (!ConnectivityChecks.CheckIsInternetConnectedAndWarn()) return null;
            ApiResponseObject<List<RightsTemplate>> responseObject = null;
            Uri uri = BuildApiUri(thisApiController, GetRightsTemplatesUrl);
            try
            {
                using (HttpResponseMessage responseMessage = await _client.GetAsync(uri).ConfigureAwait(false))
                {
                    responseObject = new ApiResponseObject<List<RightsTemplate>>(responseMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return responseObject;
        }
    }
}
