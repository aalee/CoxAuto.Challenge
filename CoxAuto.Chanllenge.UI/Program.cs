using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Interfaces.Services;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Response;
using CoxAuto.Challenge.Core.Services;
using Newtonsoft.Json;
using Answer = CoxAuto.Challenge.Core.Models.Request.Answer;

namespace CoxAuto.Challenge.UI
{
    class Program
    {
        private static IVehicleService _vehicleService;
        private static IDealerService _dealerService;
        private static IDatasetService _datasetService;
        private static Vehicles _vehicles;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Working...");
            _vehicleService = new VehicleService(new RestClient());
            _dealerService = new DealerService(new RestClient());
            _datasetService = new DatasetService(new RestClient());

            var datasetId =await _datasetService.GetDatasetId();
            _vehicles = _vehicleService.GetVehiclesAsync(datasetId);
            ConcurrentBag<Vehicle> vehicleList = new ConcurrentBag<Vehicle>();

            Parallel.For(0, _vehicles.VehicleIds.Count, async index =>
            {
                var response = await _vehicleService.GetVehicleAsync(datasetId, _vehicles.VehicleIds[index]);
                vehicleList.Add(response);
            });


            ConcurrentBag<Dealer> dealers = new ConcurrentBag<Dealer>();
            var dealerIdList = vehicleList.Select(vehicle => vehicle.DealerId).Distinct().ToList();

            Parallel.For(0, dealerIdList.Count, async index =>
            {
                var response =await _dealerService.GetDealerInfoAsync(datasetId, dealerIdList[index]);
                response.Vehicles = vehicleList.Where(g => g.DealerId == response.DealerId).ToList();
                dealers.Add(response);
            });


            var messageAnswer = await _datasetService.PostAnswer(datasetId, dealers.ToList());
            

            var answer = new Answer()
            {
                Dealers = dealers.ToList()
            };
            Console.WriteLine($"The payload was: {JsonConvert.SerializeObject(answer, Formatting.Indented)}");
            Console.Write(JsonConvert.SerializeObject(answer, Formatting.Indented));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Was success this execution?, {messageAnswer.Success}");
            Console.WriteLine($"Message from API: {messageAnswer.Message}");
            Console.WriteLine($"This execution took {messageAnswer.TotalMilliseconds / 1000} seconds");
            Console.WriteLine($"Or {messageAnswer.TotalMilliseconds} Milliseconds");
            Console.WriteLine();
            Console.WriteLine("Press any key to terminate...");
            Console.ReadKey();

        }
    }
}
