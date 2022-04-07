using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TeamConnect.Helpers;

namespace TeamConnect.Services.RestService
{
    public class RestService : IRestService
    {
        #region -- IRestService implementation

        public Task<ServerResponse<TSuccess, TError>> GetAsync<TSuccess, TError>(string resource, Dictionary<string, string> additionalHeaders = null)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(resource),
            };

            return SendRequest<TSuccess, TError>(request, additionalHeaders);
        }

        #endregion

        #region -- Private helpers --

        private async Task<ServerResponse<TSuccess, TError>> SendRequest<TSuccess, TError>(HttpRequestMessage request, Dictionary<string, string> additionalHeaders = null)
        {
            var result = new ServerResponse<TSuccess, TError>();

            using (var client = CreateHttpClient(DefaultHeaders(additionalHeaders)))
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                var responseString = await response.Content.ReadAsStringAsync();

                result.IsSuccess = response.IsSuccessStatusCode;
                result.Code = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    result.SuccessResult = JsonConvert.DeserializeObject<TSuccess>(responseString, converters: new JsonConverter[] { new IsoDateTimeConverter() });
                }
                else
                {
                    result.ErrorResult = JsonConvert.DeserializeObject<TError>(responseString, converters: new JsonConverter[] { new IsoDateTimeConverter() });
                }
            }

            return result;
        }

        private HttpClient CreateHttpClient(Dictionary<string, string> headerParams = null)
        {
            var httpClient = new HttpClient();

            if (headerParams != null)
            {
                foreach (var headerParam in headerParams)
                {
                    httpClient.DefaultRequestHeaders.Add(headerParam.Key, headerParam.Value);
                }
            }

            return httpClient;
        }

        private Dictionary<string, string> DefaultHeaders(Dictionary<string, string> additionalHeaders = null)
        {
            if (additionalHeaders == null)
            {
                additionalHeaders = new Dictionary<string, string>();
            }

            var defheaders = new Dictionary<string, string>();

            defheaders["User-Agent"] = "Mobile";
            defheaders["Accept"] = "*/*";

            foreach (var kv in additionalHeaders)
            {
                defheaders[kv.Key] = kv.Value;
            }

            return defheaders;
        }

        #endregion
    }
}
