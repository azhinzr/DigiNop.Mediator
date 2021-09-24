using  ApiClient.ApiStandardResults;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient
{
    public interface IApiClient
    {
        Task<ApiResult<T>> Get<T>(string url, object payload = null, ICollection<KeyValuePair<string, string>> queryParams = null, bool returnOriginalResponse = false);
        Task<ApiResult<T>> Post<T>(string url, object payload, bool returnOriginalResponse = false, ICollection<KeyValuePair<string, string>> extraHeaders = null);
        Task<ApiResult<T>> Put<T>(string url, object payload, bool returnOriginalResponse = false, ICollection<KeyValuePair<string, string>> extraHeaders = null);
        Task<ApiResult<T>> Delete<T>(string url, object payload = null, bool returnOriginalResponse = false);
    }
}
