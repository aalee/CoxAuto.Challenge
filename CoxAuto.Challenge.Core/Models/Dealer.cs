using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoxAuto.Challenge.Core.Models
{
    public class Dealer
    {
        [JsonProperty("dealerId")] public int DealerId { get; set; }

        [JsonProperty("name")] public string? Name { get; set; }

        [JsonProperty("vehicles")] public List<Vehicle>? Vehicles { get; set; }
    }
}