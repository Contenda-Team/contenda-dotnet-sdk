namespace Contenda.Sdk
{
    internal static class Constants
    {
        internal const string BaseApiUri = "https://prod.contenda.co/";

        internal const string MainVersion3Prefix = "api/v3/";

        internal const string AuthVersion1Prefix = "auth/v1/";

        internal const string Health = "health";

        internal const string ClientUserAgent = "Contenda .NET SDK";

        internal const string JsonMimeType = "application/json";

        internal static class AuthV1
        {
            internal const string Token = "flow/apilogin";
        }

        internal static class JobsV3
        {
            internal const string Status = "jobs/status/";

            internal const string SubmitVideoToBlog = "jobs/video-to-blog";

            internal const string UsageLimits = "jobs/usage-limits";
        }

        internal static class ContentV3
        {
            internal const string BlogMarkdown = "content/blog/{0}/markdown";
        }
    }
}
