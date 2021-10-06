using Newtonsoft.Json;

namespace CoxAuto.Challenge.Core.Models.Response
{
    public class Answer
    {
        [JsonProperty("success")] public bool Success { get; set; }

        [JsonProperty("message")] public string? Message { get; set; }

        [JsonProperty("totalMilliseconds")] public int TotalMilliseconds { get; set; }
    }
}