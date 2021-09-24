using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiClient.ApiStandardResults;
using ApiClient.NopCommerce.ApiStandardResults;
using ApiClient.NopCommerce.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace ApiClient.NopCommerce
{
    public class NopApiClient  : INopAliClient
    {
        private static string _accessToken;
         private readonly RestClient _client;
        private readonly IConfiguration _configuration;
        private int _tryCount;
        private int _tryCountforToken;

        public NopApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var timeout = Convert.ToInt32(_configuration.GetSection("Nop:TimeoutInMilliseconds").Value);
            _client = new RestClient { Timeout = timeout };
            _tryCount = 0;
            _tryCountforToken = 0;

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

            var request = CreateRequest(url, payload, method, headers, null);

            var response = await _client.ExecuteAsync<ApiResult<T>>(request);

            Console.WriteLine($"{method} {response.StatusCode}");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _accessToken = null;
                if (_tryCount < 3)
                {
                    _tryCount++;
                    return await Execute<T>(url, payload, method);
                }
            }
            ThrowExceptionIfErrorOccured<T>(url, payload, response, headers);

            return response.Data;
        }

        private RestRequest CreateRequest(
            string url,
            object payload,
            Method method,
            Dictionary<string, string> headers,
            ICollection<KeyValuePair<string, string>> queryParams
        )
        {
            var request = new RestRequest(url, method, DataFormat.Json);
            if (payload != null)
            {
                request.AddJsonBody(payload);
            }
            _client.AddDefaultHeaders(headers);
            _client.BaseUrl = new Uri(_configuration[$"Nop:BaseUrl"]);
            return request;
        }

        private string GetAccessTokenAsync()
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_configuration["Nop:TokenProviderUrl"], Method.POST);

                var payload = new
                {
                    ClientId = _configuration[$"Nop:ClientId"],
                    ClientSecret = _configuration[$"Nop:ClientSecret"],
                    ServerUrl = _configuration[$"Nop:BaseUrl"]
                };
                client.RemoteCertificateValidationCallback= (sender, cert, chain, sslPolicyErrors) => { return true; };
                //_client.FollowRedirects = false;
                request.AddJsonBody(payload);
                var response = client.Execute<ApiResult<TokenModel>>(request);
                
                if (response.StatusCode != HttpStatusCode.OK)
                { 
                    if (_tryCountforToken < 3)
                    {
                        _tryCount++;
                        return GetAccessTokenAsync();
                    }
                }
                var token = response.Data.Result.Token;
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        private Dictionary<string, string> GetHeaders()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                _accessToken = GetAccessTokenAsync();
            }

            var headers = new Dictionary<string, string> { { "authorization", $"Bearer {_accessToken}" } };
            return headers;
        }

        private void ThrowExceptionIfErrorOccured<T>(string url, object payload, IRestResponse<ApiResult<T>> response, Dictionary<string, string> headers)
        {
            if (response.IsSuccessful == false)
            {
                throw new NopApiCallException.Builder()
                    .WithApiUrlAddress(url)
                       
                    .WithInputData(response.Content)

                    .WithInnerException(response.ErrorException)
                    .Build();
            }
        }

        //private void ThrowExceptionIfErrorOccured<T>(string url, object payload, IRestResponse<ApiResult<T>> response, Dictionary<string, string> headers)
        //{
        //    if (response.IsSuccessful == false)
        //    {
        //        throw new NopApiCallException.Builder()
        //            .WithApiUrlAddress(url) 
        //            .WithInputData(response.Content)
                 
        //            .WithInnerException(response.ErrorException)
        //            .Build();
        //    }
        //}

      
    }


}
