using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Interfaces.Services;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Request;
using Newtonsoft.Json;

namespace CoxAuto.Challenge.Core.Services
{
    public class RestClient : IRestClient
    {
        private const string Urlbase = "http://api.coxauto-interview.com/api/";

        public async Task<HttpResponseMessage> GetDataSet()
        {
            var url = $"{Urlbase}datasetId";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url));
                return response;
            }
        }

        public async Task<HttpResponseMessage> GetVehicles(string datasetId)
        {
            var url = $"{Urlbase}{datasetId}/vehicles";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url));
                return response;
            }
        }

        public HttpResponseMessage GetVehicle(string datasetId, int vehicleId)
        {
            var url = $"{Urlbase}{datasetId}/vehicles/{vehicleId}";
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(new Uri(url)).Result;
                return response;
            }
        }

        public async Task<HttpResponseMessage> GetDealers(string datasetId, int dealersId)
        {
            var url = $"{Urlbase}{datasetId}/dealers/{dealersId}";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url));
                return response;
            }
        }

        public async Task<HttpResponseMessage> PostAnswer(string datasetId, List<Dealer> dealers)
        {
            var url = $"{Urlbase}{datasetId}/answer";
            using (var httpClient = new HttpClient())
            {
                var answer = new Answer
                {
                    Dealers = dealers
                };
                var content = new StringContent(JsonConvert.SerializeObject(answer), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(new Uri(url), content);
                return response;
            }
        }
    }
}