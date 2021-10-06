using System;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Interfaces.Services;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Response;
using Newtonsoft.Json;

namespace CoxAuto.Challenge.Core.Services
{
    public class DealerService : IDealerService
    {
 
        private IRestClient _restClient;
        public DealerService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Dealer> GetDealerInfoAsync(string datasetId, int dealerId)
        {
            var dealer = JsonConvert.DeserializeObject<Dealer>(await _restClient.GetDealers(datasetId, dealerId).Result.Content.ReadAsStringAsync());
            if (dealer != null) return dealer;
            throw new Exception("dealer not found");
        }
    }
}