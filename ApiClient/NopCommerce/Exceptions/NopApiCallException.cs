using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ApiClient.NopCommerce.Exceptions
{
    public class NopApiCallException : Exception
    {
        public NopApiCallException(string message, Exception innerException = null)
        : base(message, innerException)
        {

        }

        public class Builder
        {
            private string _apiUrlAddress;
            private object _headers;
            private object _inputData;
            private object _resultData;
            private Exception _innerException;

            public Builder WithApiUrlAddress(string apiUrlAddress)
            {
                _apiUrlAddress = apiUrlAddress;
                return this;
            }

            public Builder WithHeaders(Dictionary<string, string> headers)
            {
                _headers = headers;
                return this;
            }

            public Builder WithInputData(object inputData)
            {
                _inputData = inputData;
                return this;
            }

            public Builder WithResultData(object resultData)
            {
                _resultData = resultData;
                return this;
            }

            public Builder WithInnerException(Exception innerException)
            {
                _innerException = innerException;
                return this;
            }

            public NopApiCallException Build()
            {
                var message = GenerateExceptionMessage();
                var crmApiCallException = new NopApiCallException(message, _innerException);
                return crmApiCallException;
            }

            private string GenerateExceptionMessage()
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.Append($"ApiUrlAddress : {_apiUrlAddress}");
                stringBuilder.AppendLine();

                if (_headers != null)
                {
                    var serializedHeaders = JsonConvert.SerializeObject(_headers);
                    stringBuilder.Append($"Headers : {serializedHeaders}");
                    stringBuilder.AppendLine();
                }

                if (_inputData != null)
                {
                    var serializedInputData = JsonConvert.SerializeObject(_inputData);
                    stringBuilder.Append($"InputData : {serializedInputData}");
                    stringBuilder.AppendLine();
                }

                if (_resultData != null)
                {
                    var serializedResultData = JsonConvert.SerializeObject(_resultData);
                    stringBuilder.Append($"ResultData : {serializedResultData}");
                    stringBuilder.AppendLine();
                }

                if (_innerException != null)
                {
                    var serializedInnerException = JsonConvert.SerializeObject(_innerException);
                    stringBuilder.Append($"InnerException : {serializedInnerException}");
                    stringBuilder.AppendLine();
                }

                return stringBuilder.ToString();
            }
        }
    }
}