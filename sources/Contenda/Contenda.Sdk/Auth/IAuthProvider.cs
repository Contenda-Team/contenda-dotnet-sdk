using System.Net.Http;
using System.Threading.Tasks;

namespace Contenda.Sdk.Auth
{
    /// <summary>
    /// Authentication provider interface
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>
        /// Modify headers callback
        /// </summary>
        /// <param name="httpClient">the HttpClient with the headers to modify</param>
        void ModifyHeadersCallback(HttpClient httpClient);

        /// <summary>
        /// Modify query callback
        /// </summary>
        /// <param name="currentQuery">the query to modify</param>
        /// <returns>modified query</returns>
        string ModifyQueryCallback(string currentQuery);

        /// <summary>
        /// Try authenticate
        /// </summary>
        /// <param name="apiBaseUri">API Base URI</param>
        /// <returns>true if successfully authenticated, false otherwise</returns>
        Task<bool> TryAuthenticate(string apiBaseUri);
    }
}
