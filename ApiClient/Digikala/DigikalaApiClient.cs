using ApiClient.ApiStandardResults;
using ApiClient.ApiStandardResults.Exceptions;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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
            _client = new RestClient { Timeout = timeout };
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
            var headers = GetHeaders();

            var request = CreateRequest(url, payload, method,headers, null);

            var response = await _client.ExecuteAsync<ApiResult<T>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (_tryCount < 3)
                {
                    System.Threading.Thread.Sleep(4000);
                    _tryCount++;
                    return await Execute<T>(url, payload, method);
                }

            }
            ThrowExceptionIfErrorOccured(url, payload, response, headers);
            return response.Data;
        }


        private RestRequest CreateRequest(
            string url,
            object payload,
            Method method,
            Dictionary<string, string> headers,
            ICollection<KeyValuePair<string, string>> queryParams)
        {
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
        private void ThrowExceptionIfErrorOccured<T>(string url, object payload, IRestResponse<ApiResult<T>> response, Dictionary<string, string> headers)
        {
            if (response.IsSuccessful == false)
            {
                throw new ApiCallException.Builder()
                    .WithApiUrlAddress(url)

                    .WithInputData(response.Content)

                    .WithInnerException(response.ErrorException)
                    .Build();
            }
        }
    }
}