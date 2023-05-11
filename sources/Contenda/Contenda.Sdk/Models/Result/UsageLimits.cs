using Newtonsoft.Json;

namespace Contenda.Sdk.Models.Result
{
    /// <summary>
    /// Usage Limits response model
    /// </summary>
    public sealed class UsageLimits
    {
        /// <summary>
        /// If the current account is unlimited
        /// </summary>
        [JsonProperty("is_unlimited")]
        public bool IsUnlimited { get; set; }

        /// <summary>
        /// Measurement period
        /// </summary>
        [JsonProperty("period")]
        public string? Period { get; set; }

        /// <summary>
        /// Maximum media minutes allowed to process for the current measurement period
        /// </summary>
        [JsonProperty("limit")]
        public double? Limit { get; set; }

        /// <summary>
        /// Media minutes used in the current measurement period
        /// </summary>
        [JsonProperty("current")]
        public double? Current { get; set; }

        /// <summary>
        /// Human friendly description of the current limits
        /// </summary>
        [JsonProperty("friendly_message")]
        public string? FriendlyMessage { get; set; }

    }
}
