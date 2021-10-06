﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoxAuto.Challenge.Core.Models.Response
{
    public class Vehicles
    {
        [JsonProperty("vehicleIds")] public List<int>? VehicleIds { get; set; }
    }
}