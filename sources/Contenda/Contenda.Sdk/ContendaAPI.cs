using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Contenda.Sdk.Auth;
using Contenda.Sdk.Models;
using Contenda.Sdk.Models.Request;
using Contenda.Sdk.Models.Result;
using Newtonsoft.Json;

namespace Contenda.Sdk
{
    /// <summary>
    /// The main Contenda API class
    /// </summary>
    public sealed class ContendaAPI
    {
        private readonly IAuthProvider _authProvider;
        private readonly string _apiBaseUri;

        /// <summary>
        /// Create a Contenda API class
        /// </summary>
        /// <param name="authProvider">the authentication provider</param>
        /// <param name="apiBaseUriOverride">API Base URI override, must contain scheme prefix and end in a /, e.g. "https://example.com/"</param>
        public ContendaAPI(IAuthProvider authProvider, string? apiBaseUriOverride = null)
        {
            _authProvider = authProvider;
            _apiBaseUri = apiBaseUriOverride ?? Constants.BaseApiUri;
        }

        /// <summary>
        /// Test service health
        /// </summary>
        /// <returns>true if healthy, false otherwise</returns>
        public async Task<bool> ServiceHealth()
        {
            var client = PoorMansHttpClientFactory.Instance.Client;
            var uri = $"{_apiBaseUri}{Constants.Health}";
            try
            {
                var result = await client.GetAsync(uri).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Authenticate using the auth provider
        /// </summary>
        /// <returns>true if successful, false otherwise</returns>
        public async Task<bool> Authenticate()
        {
            return await _authProvider.TryAuthenticate(_apiBaseUri).ConfigureAwait(false);
        }

        /// <summary>
        /// Get usage limits for the authenticated account
        /// </summary>
        /// <returns></returns>
        public async Task<UsageLimits?> GetUsageLimits()
        {
            var client = PoorMansHttpClientFactory.Instance.Client;
            var uri = $"{_apiBaseUri}{Constants.MainVersion3Prefix}{Constants.JobsV3.UsageLimits}";

            _authProvider.ModifyHeadersCallback(client);
            uri = _authProvider.ModifyQueryCallback(uri);

            var result = await client.GetAsync(uri).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<UsageLimits>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Get a job's status
        /// </summary>
        /// <param name="jobId">job ID, e.g. uncertainty-rk-family-am-reflections-jo</param>
        /// <returns>job status</returns>
        public async Task<JobStatusResult?> GetJobStatus(string jobId)
        {
            var client = PoorMansHttpClientFactory.Instance.Client;
            var uri = $"{_apiBaseUri}{Constants.MainVersion3Prefix}{Constants.JobsV3.Status}{HttpUtility.UrlEncode(jobId)}";

            _authProvider.ModifyHeadersCallback(client);
            uri = _authProvider.ModifyQueryCallback(uri);

            var result = await client.GetAsync(uri).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<JobStatusResult>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Submit a new video to blog job
        /// </summary>
        /// <param name="sourceId">source ID</param>
        /// <param name="subType">blog type</param>
        /// <param name="statusUpdateWebhookUrl">optional, add a status update webhook url</param>
        /// <param name="statusUpdateEmail">optional, override the default email to send a status updates to</param>
        /// <returns></returns>
        /// <remarks>
        /// These are the supported source_id types - substitute the $values for your media:
        /// <list type="bullet">
        /// <item>
        /// "youtube $id" YouTube videos, $id for a YouTube video ID, e.g. https://www.youtube.com/watch?v=dQw4w9WgXcQ becomes "youtube dQw4w9WgXcQ"
        /// </item>
        /// <item>
        /// "twitch $id" Twitch vods, $id for a Twitch vod ID, e.g. https://www.twitch.tv/videos/1079879708 becomes "twitch 1079879708"
        /// </item>
        /// <item>
        /// "facebook $channel $id" Facebook videos, $channel for a Facebook page ID and $id for a Facebook video ID on that page, e.g. https://www.facebook.com/PersonOfInterestTV/videos/1827475693951431 becomes "facebook PersonOfInterestTV 1827475693951431"
        /// </item>
        /// <item>
        /// "mux $id" Mux videos, $id for a Mux video ID, e.g. https://stream.mux.com/uNbxnGLKJ00yfbijDO8COxTOyVKT01xpxW.m3u8 becomes "mux uNbxnGLKJ00yfbijDO8COxTOyVKT01xpxW"
        /// </item>
        /// <item>
        /// "url $url" Raw URL links, $url for a fully qualified URL that would download a media, e.g. "url https://download.blender.org/demo/movies/BBB/bbb_sunflower_1080p_60fps_normal.mp4"
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<JobStatusResult?> SubmitVideoToBlogJob(string sourceId, VideoToBlogJobSubType subType, string? statusUpdateWebhookUrl = null, string? statusUpdateEmail = null)
        {
            var client = PoorMansHttpClientFactory.Instance.Client;
            var uri = $"{_apiBaseUri}{Constants.MainVersion3Prefix}{Constants.JobsV3.SubmitVideoToBlog}";

            _authProvider.ModifyHeadersCallback(client);
            uri = _authProvider.ModifyQueryCallback(uri);

            var content = new StringContent(JsonConvert.SerializeObject(new SubmitJobV3
            {
                source_id = sourceId,
                status_update_email = statusUpdateEmail,
                status_update_webhook_url = statusUpdateWebhookUrl,
                type = subType == VideoToBlogJobSubType.Presentation ? "presentation" : "tutorial"
            }), Encoding.UTF8, Constants.JsonMimeType);

            var result = await client.PostAsync(uri, content).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<JobStatusResult>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Get blog as markdown
        /// </summary>
        /// <param name="blogId">blog ID</param>
        /// <returns>markdown blog</returns>
        public async Task<string> GetBlogAsMarkdown(string blogId)
        {
            var client = PoorMansHttpClientFactory.Instance.Client;
            var uri = $"{_apiBaseUri}{Constants.MainVersion3Prefix}{Constants.ContentV3.BlogMarkdown}";

            uri = string.Format(uri, blogId);

            _authProvider.ModifyHeadersCallback(client);
            uri = _authProvider.ModifyQueryCallback(uri);

            var result = await client.GetAsync(uri).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
