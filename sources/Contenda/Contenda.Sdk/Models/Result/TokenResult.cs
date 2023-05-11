// ReSharper disable InconsistentNaming

using JetBrains.Annotations;

namespace Contenda.Sdk.Models.Result
{
    internal class TokenResult
    {
        [UsedImplicitly] public string? valid_until { get; set; }

        public string? access_token { get; [UsedImplicitly] set; }
    }
}
