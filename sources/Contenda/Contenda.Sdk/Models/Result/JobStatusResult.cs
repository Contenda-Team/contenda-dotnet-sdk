using Newtonsoft.Json;

namespace Contenda.Sdk.Models.Result
{
    /// <summary>
    /// Job result
    /// </summary>
    public sealed class JobResult
    {
        /// <summary>
        /// Job result type
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Result document ID
        /// </summary>
        [JsonProperty("result_document_id")]
        public string? ResultDocumentId { get; set; }
    }

    /// <summary>
    /// Job status result response model
    /// </summary>
    public sealed class JobStatusResult
    {
        /// <summary>
        /// Job status message
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Job status
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Job ID
        /// </summary>
        [JsonProperty("job_id")]
        public string? JobId { get; set; }

        /// <summary>
        /// Job result, only exists if job state is successful
        /// </summary>
        [JsonProperty("result")]
        public JobResult? Result { get; set; }
    }
}
