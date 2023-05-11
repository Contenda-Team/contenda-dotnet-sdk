// ReSharper disable InconsistentNaming

using JetBrains.Annotations;

namespace Contenda.Sdk.Models.Request
{
    internal class TokenV2
    {
        public string? email { [UsedImplicitly] get; set; }

        public string? api_key { [UsedImplicitly] get; set; }
    }
}
