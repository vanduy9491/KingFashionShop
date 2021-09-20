using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KingFashion.Helpers
{
    public static class ApiHelper
    {
        public static async Task<T> HttpGet<T>(string apiUrl, string method = "GET")
        {
            WebRequest request = WebRequest.Create(apiUrl);
            request.Method = method;
            WebResponse response = request.GetResponse();
            string responseFromServer = string.Empty;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(responseFromServer);
        }

        public static async Task<T> HttpPost<T>(string apiUrl, string method, object model)
        {
            WebRequest request = WebRequest.Create(apiUrl);
            request.Method = method;
            request.ContentType = "application/json";

            using (var streamWrite = new StreamWriter(request.GetRequestStream()))
            {
                var data = JsonConvert.SerializeObject(model);
                streamWrite.Write(data);
            }
            WebResponse response = request.GetResponse();
            string responseFromServer = string.Empty;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(responseFromServer);
        }
    }
}
