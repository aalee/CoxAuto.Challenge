using System;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Interfaces.Services;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Response;
using Newtonsoft.Json;

namespace CoxAuto.Challenge.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IRestClient _restClient;

        public VehicleService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Vehicles GetVehiclesAsync(string datasetId)
        {
            var vehicles =
                JsonConvert.DeserializeObject<Vehicles>(_restClient.GetVehicles(datasetId).Result.Content
                    .ReadAsStringAsync().Result);
            if (vehicles != null) return vehicles;
            throw new Exception("vehicles not found");
        }

        public async Task<Vehicle> GetVehicleAsync(string datasetId, int vehicleId)
        {
            var vehicle =
                JsonConvert.DeserializeObject<Vehicle>(await _restClient.GetVehicle(datasetId, vehicleId).Content
                    .ReadAsStringAsync());
            if (vehicle != null) return vehicle;
            throw new Exception("vehicle not found");
        }
    }
}