using GuildMeetOrganizer.Helpers;
using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.Models.ApiHelper;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using static GuildMeetOrganizer.Models.ApiHelper.ApiUriBuilder;

namespace GuildMeetOrganizer.Controller
{
    internal class AuthenticateController
    {
        readonly HttpClient _client;
        readonly ApiControllers thisApiController = ApiControllers.Authenticate;
        public AuthenticateController()
        {
            _client = new HttpClient();
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
            Uri uri = BuildApiUri(thisApiController, LoginUserUrl);
            try
            {
                LoginRequest loginRequest = new LoginRequest()
                {
                    Username = username,
                    Password = password
                };
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                using (HttpResponseMessage responseMessage = await _client.PostAsync(uri, stringContent).ConfigureAwait(false))
                {
                    ApiResponseObject<LoginResult> response = new ApiResponseObject<LoginResult>(responseMessage);
                    GlobalVariables.ApiToken = response.Response.JwtToken;
                    GlobalVariables.LoggedInUser = response.Response.User;
                    responseObject = new ApiResponseObject<User>()
                    {
                        Response = response.Response.User,
                        ErrorMessage = response.ErrorMessage
                    };
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
