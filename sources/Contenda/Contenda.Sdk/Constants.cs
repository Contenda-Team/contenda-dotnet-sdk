namespace Contenda.Sdk
{
    internal static class Constants
    {
        internal const string BaseApiUri = "https://prod.contenda.io/";

        internal const string Version2Prefix = "api/v2/";

        internal const string Health = "health";

        internal const string ClientUserAgent = "Contenda .NET SDK";

        internal const string JsonMimeType = "application/json";

        internal static class IdentityV2
        {
            internal const string Token = "identity/token";
        }

        internal static class JobsV2
        {
            internal const string Status = "jobs/status/";

            internal const string SubmitVideoToBlog = "jobs/video-to-blog";

            internal const string UsageLimits = "jobs/usage-limits";
        }

        internal static class ContentV2
        {
            internal const string BlogMarkdown = "content/blog/{0}/markdown";
        }
    }
}
