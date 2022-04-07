using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;

namespace TeamConnect.Services.RestService
{
    public interface IRestService
    {
        /// <summary>
        /// Get request.
        /// </summary>
        /// <typeparam name="TSuccess">The object that is returned if the request succeeds.</typeparam>
        /// <typeparam name="TError">The object that is returned if an exception is thrown.</typeparam>
        /// <param name="resource">Request URL</param>
        /// <param name="additioalHeaders">Additioal headers dictionary.</param>
        /// <returns>Returns the object that contains the data and results of the request.</returns>
        Task<ServerResponse<TSuccess, TError>> GetAsync<TSuccess, TError>(string resource, Dictionary<string, string> additioalHeaders = null);
    }
}
