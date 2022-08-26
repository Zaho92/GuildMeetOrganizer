using GuildMeetOrganizer.Helpers;
using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.Models.ApiHelper;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using static GuildMeetOrganizer.Models.ApiHelper.ApiUriBuilder;

namespace GuildMeetOrganizer.Controller
{

    internal class UserDataController
    {
        readonly HttpClient _client;
        readonly ApiControllers thisApiController = ApiControllers.User;

        public UserDataController()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalVariables.ApiToken);
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
            Uri uri = BuildApiUri(thisApiController, GetUsersUrl);
            try
            {
                using HttpResponseMessage responseMessage = await _client.GetAsync(uri).ConfigureAwait(false);
                responseObject = new ApiResponseObject<List<User>>(responseMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return responseObject;
        }

        const string GetUserUrl = "GetUser";
        public ApiResponseObject<User> GetUser(int idUser)
        {
            return GetUserAsync(idUser).Result;
        }

        public async Task<ApiResponseObject<User>> GetUserAsync(int idUser)
        {
            if (!ConnectivityChecks.CheckIsInternetConnectedAndWarn()) return null;
            ApiResponseObject<User> responseObject = null;
            Uri uri = BuildApiUri(thisApiController, GetUserUrl, new Dictionary<string, string>()
            {
                {nameof(idUser), idUser.ToString() }
            });
            try
            {
                using HttpResponseMessage responseMessage = await _client.GetAsync(uri).ConfigureAwait(false);
                responseObject = new ApiResponseObject<User>(responseMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return responseObject;
        }      

        const string AddUserUrl = "AddUser";
        public ApiResponseObject<User> AddUser(User user)
        {
            return AddUserAsync(user).Result;
        }

        public async Task<ApiResponseObject<User>> AddUserAsync(User user)
        {
            if (!ConnectivityChecks.CheckIsInternetConnectedAndWarn()) return null;
            ApiResponseObject<User> responseObject = null;
            Uri uri = BuildApiUri(thisApiController, AddUserUrl, new Dictionary<string, string>()
            {
                { nameof(user.Password), user.Password }
            });
            try
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (HttpResponseMessage responseMessage = await _client.PostAsync(uri, stringContent).ConfigureAwait(false))
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

        const string EditUserUrl = "EditUser";
        public ApiResponseObject<User> EditUser(User user)
        {
            return EditUserAsync(user).Result;
        }

        public async Task<ApiResponseObject<User>> EditUserAsync(User user)
        {
            if (!ConnectivityChecks.CheckIsInternetConnectedAndWarn()) return null;
            ApiResponseObject<User> responseObject = null;
            Uri uri = BuildApiUri(thisApiController, EditUserUrl);
            try
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (HttpResponseMessage responseMessage = await _client.PutAsync(uri, stringContent).ConfigureAwait(false))
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

        const string DeleteUserUrl = "DeleteUser";
        public ApiResponseObject<bool> DeleteUser(int idUser)
        {
            return DeleteUserAsync(idUser).Result;
        }

        public async Task<ApiResponseObject<bool>> DeleteUserAsync(int idUser)
        {
            if (!ConnectivityChecks.CheckIsInternetConnectedAndWarn()) return null;
            ApiResponseObject<bool> responseObject = null;
            Uri uri = BuildApiUri(thisApiController, DeleteUserUrl, new Dictionary<string, string>()
            {
                { nameof(idUser), idUser.ToString() }
            });
            try
            {
                using (HttpResponseMessage responseMessage = await _client.DeleteAsync(uri).ConfigureAwait(false))
                {
                    responseObject = new ApiResponseObject<bool>(responseMessage);
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
