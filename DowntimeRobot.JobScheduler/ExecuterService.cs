using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.JobScheduler
{
    public class ExecuterService
    {
        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _serviceUrl;
        private string Token { get; set; }

        public ExecuterService(string apiKey, string secretKey, string serviceUrl)
        {
            _apiKey = apiKey;
            _secretKey = secretKey;
            _serviceUrl = serviceUrl;
            Token = CreateToken();
        }

        public async Task<bool> ExecuteJobs()
        {
            using (var client = new HttpClient() { BaseAddress = new Uri(_serviceUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ApiKey", _apiKey);
                client.DefaultRequestHeaders.Add("SecretKey", _secretKey);
                client.DefaultRequestHeaders.Add("Authenticate-Bearer", Token);
                var response = client.GetAsync("/api/job-executer").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<Response>(result);
                    return res.IsSuccess;
                }
                //
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    int counter = 0;
                    while (counter < 5)
                    {
                        Token = CreateToken();
                        if (!string.IsNullOrEmpty(Token)) break;
                        //
                        counter++;
                    }
                    //
                    return await ExecuteJobs();
                }
            }

            return false;
        }

        private string CreateToken()
        {
            using (var client = new HttpClient() { BaseAddress = new Uri(_serviceUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("/api/create-token", new StringContent(JsonConvert.SerializeObject(new ReqCreateToken
                {
                    ApiKey = _apiKey,
                    SecretKey = _secretKey
                }), Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
                //
                if (response == null) return null;
                //
                var result = JsonConvert.DeserializeObject<Response>(response);
                if (result.IsSuccess)
                    return result.Data;
                else
                    return null;
            }
        }
        private class ReqCreateToken
        {
            public string ApiKey { get; set; }
            public string SecretKey { get; set; }
        }

        public class Response
        {
            public string Data { get; set; }
            public string Message { get; set; }
            public bool IsSuccess { get; set; }
        }
    }


}
