// ReSharper disable InconsistentNaming

using JetBrains.Annotations;

namespace Contenda.Sdk.Models.Request
{
    internal class SubmitJobV2
    {
        public string? source_id { [UsedImplicitly] get; set; }

        public string? status_update_webhook_url { [UsedImplicitly] get; set; }

        public string? type { [UsedImplicitly] get; set; }

        public string? status_update_email { [UsedImplicitly] get; set; }
    }
}
