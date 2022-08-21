using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.Models.ApiHelper
{
    internal class ApiResponseObject<T>
    {
        public T ResponseObject;
        public bool HasError => !String.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage;
        public HttpStatusCode StatusCode { get; set; }

        public ApiResponseObject(HttpResponseMessage responseMessage)
        {
            StatusCode = responseMessage.StatusCode;
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                ResponseObject = JsonSerializer.Deserialize<T>(content, serializerOptions);
            }
            else
            {
                ErrorMessage = responseMessage.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
