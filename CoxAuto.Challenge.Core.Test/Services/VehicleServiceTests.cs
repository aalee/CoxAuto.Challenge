using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Interfaces.Services;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Response;
using Xunit;
using Xunit.Abstractions;

namespace CoxAuto.Challenge.Core.Services.Tests
{
    public class VehicleServiceTests
    {
        private const string datasetId = "vDR-MmaI2Qg";
        private readonly IVehicleService _vehicleService;
        private readonly IDealerService _dealerService;
        private readonly IDatasetService _datasetService;
        private Vehicles _vehicles;
        private readonly ITestOutputHelper output;

        public VehicleServiceTests(ITestOutputHelper output)
        {
            this.output = output;
            _vehicleService = new VehicleService(new RestClient());
            _dealerService = new DealerService(new RestClient());
            _datasetService = new DatasetService(new RestClient());
        }

        [Fact]
        public async Task GetVehiclesAsyncTest()
        {
            _vehicles = _vehicleService.GetVehiclesAsync(datasetId);
            var vehicleList = new ConcurrentBag<Vehicle>();

            void GetCarDetail(int item)
            {
                var response = _vehicleService.GetVehicleAsync(datasetId, item).Result;
                vehicleList.Add(response);
            }


            Parallel.ForEach(_vehicles.VehicleIds, GetCarDetail);


            var dealers = new ConcurrentBag<Dealer>();
            var dealerIdList = vehicleList.Select(vehicle => vehicle.DealerId).Distinct().ToList();

            Parallel.ForEach(dealerIdList, dealerId =>
            {
                var response = _dealerService.GetDealerInfoAsync(datasetId, dealerId).Result;
                dealers.Add(response);
            });


            foreach (var dealer in dealers)
                dealer.Vehicles = vehicleList.Where(g => g.DealerId == dealer.DealerId).ToList();

            var messageAnswer = await _datasetService.PostAnswer(datasetId, dealers.ToList());
            Assert.NotNull(messageAnswer);
            Assert.True(messageAnswer?.TotalMilliseconds >= 100);
            Assert.True(_vehicles.VehicleIds.Count == vehicleList.Count);
            Assert.True(messageAnswer?.Success);
        }
    }
}