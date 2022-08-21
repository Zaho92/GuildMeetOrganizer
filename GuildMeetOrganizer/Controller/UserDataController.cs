using GuildMeetOrganizer.Helpers;
using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.Models.ApiHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static GuildMeetOrganizer.Models.ApiHelper.ApiUriBuilder;

namespace GuildMeetOrganizer.Controller
{

    internal class UserDataController
    {
        readonly HttpClient _client;

        public List<User> Users { get; private set; }
        public List<RightsTemplate> RightsTemplates { get; private set; }

        public UserDataController()
        {
            _client = new HttpClient();
        }

        const string GetUsersUrl = "GetUsers";
        public ApiResponseObject<List<User>> GetUsers()
        {
            return GetUsersAsync().Result;
        }

        public async Task<ApiResponseObject<List<User>>> GetUsersAsync()
        {
            if (!ConnectivityChecks.CheckIsInternetConnectedAndWarn()) return null;
            ApiResponseObject<List<User>> responseObject = null;
            Uri uri = BuildApiUri(ApiControllers.User, GetUsersUrl);
            try
            {
                using (HttpResponseMessage responseMessage = await _client.GetAsync(uri).ConfigureAwait(false))
                    {
                    responseObject = new ApiResponseObject<List<User>>(responseMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return responseObject;
        }

        const string LoginUserUrl = "LoginUser";
        public ApiResponseObject<User> LoginUser(string username, string password)
        {
            return LoginUserAsync(username, password).Result;
        }

        public async Task<ApiResponseObject<User>> LoginUserAsync(string username, string password)
        {
            if (!ConnectivityChecks.CheckIsInternetConnectedAndWarn()) return null;
            ApiResponseObject<User> responseObject = null;
            Uri uri = BuildApiUri(ApiControllers.User, LoginUserUrl, new Dictionary<string, string>()
            {
                { nameof(username), username },
                { nameof(password), password }
            });
            try
            {
                using (HttpResponseMessage responseMessage = await _client.GetAsync(uri).ConfigureAwait(false))
                {
                    responseObject = new ApiResponseObject<User>(responseMessage);
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
