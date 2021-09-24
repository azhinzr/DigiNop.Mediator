using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiClient.ApiStandardResults;
using ApiClient.NopCommerce.ApiStandardResults;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace ApiClient.Digikala
{
    public class DigikalaApiClient : IDigikalaApiClient
    {
        private readonly RestClient _client;
        private readonly IConfiguration _configuration;
        private int _tryCount;
        private string _accessToken;

        public DigikalaApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var timeout = Convert.ToInt32(_configuration.GetSection("Digikala:TimeoutInMilliseconds").Value);
            _client = new RestClient { Timeout = timeout};
            _tryCount = 0;
            _client.BaseUrl = new Uri(_configuration["Digikala:BaseUrl"]);
        }

        public Task<ApiResult<T>> Get<T>(string url, object payload = null, ICollection<KeyValuePair<string, string>> queryParams = null, bool returnOriginalResponse = false)
        {
            return Execute<T>(url, payload, Method.GET);
        }

        public Task<ApiResult<T>> Post<T>(string url, object payload, bool returnOriginalResponse = false, ICollection<KeyValuePair<string, string>> extraHeaders = null)
        {
            return Execute<T>(url, payload, Method.POST);
        }

        public Task<ApiResult<T>> Put<T>(string url, object payload, bool returnOriginalResponse = false, ICollection<KeyValuePair<string, string>> extraHeaders = null)
        {
            return Execute<T>(url, payload, Method.PUT);
        }

        public Task<ApiResult<T>> Delete<T>(string url, object payload = null, bool returnOriginalResponse = false)
        {
            throw new NotImplementedException();
        }

        private async Task<ApiResult<T>> Execute<T>(string url, object payload, Method method)
        {
            var request = CreateRequest(url, payload, method, null);
            
            var response =await _client.ExecuteAsync<ApiResult<T>>(request);
            if (response.Data == null)
            {
                System.Threading.Thread.Sleep(40000);
                return await Execute<T>(url, payload, method);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (_tryCount < 3)
                {
                    System.Threading.Thread.Sleep(4000); 
                    _tryCount++;
                    return await Execute<T>(url, payload, method);
                }
                
            }
           
             return response.Data;
        }
      

        private RestRequest CreateRequest(
            string url,
            object payload,
            Method method,
             ICollection<KeyValuePair<string, string>> queryParams
        )
        {
            var headers = GetHeaders();
            var request = new RestRequest(url, method, DataFormat.Json);
            if (payload != null)
            {
                request.AddJsonBody(payload);
            }

            _client.AddDefaultHeaders(headers);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            return request;
        }


        private Dictionary<string, string> GetHeaders()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                _accessToken = GetAccessTokenAsync();
            }

            var headers = new Dictionary<string, string> { { "Authorization", _accessToken } };
            return headers;
        }

        private string GetAccessTokenAsync()
        {
            return _configuration["Digikala:AccesToken"];

        }
    }
}