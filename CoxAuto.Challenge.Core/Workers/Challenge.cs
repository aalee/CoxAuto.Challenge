using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Interfaces.Services;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Response;
using CoxAuto.Challenge.Core.Services;

namespace CoxAuto.Challenge.Core.Workers
{
    public static class Challenge
    {
#pragma warning disable 8618
        private static IVehicleService _vehicleService;
        private static IDealerService _dealerService;
        private static IDatasetService _datasetService;
        private static IRestClient _restClient;
        private static Vehicles _vehicles;
#pragma warning restore 8618

        public static async Task<Answer?> RunAsync()
        {
            _restClient = new RestClient();
            _vehicleService = new VehicleService(_restClient);
            _dealerService = new DealerService(_restClient);
            _datasetService = new DatasetService(_restClient);

            var datasetId = await _datasetService.GetDatasetId();
            _vehicles = _vehicleService.GetVehiclesAsync(datasetId);
            ConcurrentBag<Vehicle> vehicleList = new ConcurrentBag<Vehicle>();

            async void GetVehicleWorker(int index)
            {
                var response = await _vehicleService.GetVehicleAsync(datasetId, _vehicles.VehicleIds[index]);
                vehicleList.Add(response);
            }

            Parallel.For(0, _vehicles.VehicleIds.Count, GetVehicleWorker);


            ConcurrentBag<Dealer> dealers = new ConcurrentBag<Dealer>();
            var dealerIdList = vehicleList.Select(vehicle => vehicle.DealerId).Distinct().ToList();

            async void GetDealerWorker(int index)
            {
                var response = await _dealerService.GetDealerInfoAsync(datasetId, dealerIdList[index]);
                response.Vehicles = vehicleList.Where(g => g.DealerId == response.DealerId).ToList();
                dealers.Add(response);
            }

            Parallel.For(0, dealerIdList.Count, GetDealerWorker);


            var messageAnswer = await _datasetService.PostAnswer(datasetId, dealers.ToList());

            return messageAnswer;
        }
    }
}