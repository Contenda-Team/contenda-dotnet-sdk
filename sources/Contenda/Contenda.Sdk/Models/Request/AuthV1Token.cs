// ReSharper disable InconsistentNaming

using JetBrains.Annotations;

namespace Contenda.Sdk.Models.Request
{
    internal class AuthV1Token
    {
        public string? user_email { [UsedImplicitly] get; set; }

        public string? api_key { [UsedImplicitly] get; set; }
    }
}
