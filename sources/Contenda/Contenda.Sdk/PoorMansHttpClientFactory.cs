using System.Net.Http;
using System.Reflection;

namespace Contenda.Sdk
{
    /// <summary>
    /// Poor man's HttpClient factory, to ensure we only use one in the lifetime of the API
    /// </summary>
    internal class PoorMansHttpClientFactory
    {
        private PoorMansHttpClientFactory()
        {
            Client = new HttpClient();

            Client.DefaultRequestHeaders.TryAddWithoutValidation(
                "User-Agent",
                $"{Constants.ClientUserAgent} {Assembly.GetExecutingAssembly().GetName().Version}");
        }

        public HttpClient Client { get; }

        private static PoorMansHttpClientFactory? _instance;

        public static PoorMansHttpClientFactory Instance => _instance ??= new PoorMansHttpClientFactory();
    }
}
