//using Xunit;
//using CoxAuto.Challenge.Core.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CoxAuto.Challenge.Core.Interfaces.Services;
//using CoxAuto.Challenge.Core.Models;
//using CoxAuto.Challenge.Core.Models.Response;
//using Xunit.Abstractions;
//using Xunit.Sdk;
//using Newtonsoft.Json;

//namespace CoxAuto.Challenge.Core.Services.Tests
//{
//    public class VehicleServiceTests
//    {
//        const string datasetId = "vDR-MmaI2Qg";
//        private IVehicleService _vehicleService;
//        private IDealerService _dealerService;
//        private IDatasetService _datasetService;
//        private Vehicles _vehicles;
//        private readonly ITestOutputHelper output;

//        public VehicleServiceTests(ITestOutputHelper output)
//        {
//            this.output = output;
//            _vehicleService = new VehicleService(new RestClient());
//            _dealerService = new DealerService(new RestClient());
//            _datasetService = new DatasetService(new RestClient());
//        }

//        [Fact()]
//        public async Task GetVehiclesAsyncTest()
//        {
//            _vehicles = _vehicleService.GetVehiclesAsync(datasetId);
//            List<Vehicle> vehicleList = new List<Vehicle>();

//            void GetCarDetail(int item)
//            {
//                var response = _vehicleService.GetVehicleAsync(datasetId, item).Result;
//                vehicleList.Add(response);
//            }


//            var invoke = Parallel.ForEach(_vehicles.VehicleIds, GetCarDetail);

//            while (!invoke.IsCompleted)
//            {
//                List<Dealer> dealers = new List<Dealer>();
//                var dealerIdList = vehicleList.Select(vehicle => vehicle.DealerId).ToList();

//                var invokeDealer = Parallel.ForEach(dealerIdList, dealerId =>
//                {
//                    var response = _dealerService.GetDealerInfoAsync(datasetId, dealerId.Value).Result;
//                    dealers.Add(response);
//                });

//                while (!invokeDealer.IsCompleted)
//                {
//                    foreach (var dealer in dealers)
//                    {

//                        dealer.Vehicles = vehicleList.Where(g => g.DealerId == dealer.DealerId).ToList();
//                    }

//                    var messageAnswer = await _datasetService.PostAnswer(datasetId, dealers);
                    

//                    Assert.True(_vehicles.VehicleIds.Count == vehicleList.Count);
//                }


//            }

//        }


//    }
//}