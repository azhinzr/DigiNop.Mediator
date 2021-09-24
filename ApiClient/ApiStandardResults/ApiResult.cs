using System.Collections.Generic;
using ApiClient.Digikala;
using ApiClient.NopCommerce.ApiStandardResults;
using Contracts.NopCommerce;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

 namespace ApiClient.ApiStandardResults

{
    public class ApiResult<TResult> : NopApiResult
    {
      
        public bool Succeeded { get; set; } 
        public TResult Result { get; set; }
         public ApiError Error { get; set; }
        public int Count { get; set; }
        public List<Product> products { get; set; }
        public Data Data { get; set; }
        public static ApiResult<TResult> StandardOk(TResult result)
        {
            return new ApiResult<TResult>
            {
                Succeeded = true,
                Result = result,
                Error = null
            };
        }
    }

   
}