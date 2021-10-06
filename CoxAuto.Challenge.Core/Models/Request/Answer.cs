using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoxAuto.Challenge.Core.Models.Request
{
    public class Answer
    {
        [JsonProperty("dealers")] public List<Dealer>? Dealers { get; set; }
    }
}