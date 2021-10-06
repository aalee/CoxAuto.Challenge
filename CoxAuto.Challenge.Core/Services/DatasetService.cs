using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Interfaces.Services;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Response;
using Newtonsoft.Json;

namespace CoxAuto.Challenge.Core.Services
{
    public class DatasetService : IDatasetService
    {
        private IRestClient _restClient;
        public DatasetService(IRestClient restClient)
        {
            _restClient = restClient;
        }
        public async Task<string> GetDatasetId()
        {
            var dataset = JsonConvert.DeserializeObject<DatasetId>(await _restClient.GetDataSet().Result.Content.ReadAsStringAsync());
            if (dataset?.datasetId != null) return dataset.datasetId;
            throw new Exception("DataSetId not found");
        }

        public async Task<Answer?> PostAnswer(string datasetId, List<Dealer> dealers)
        {
            var result = await _restClient.PostAnswer(datasetId, dealers);
            var message = JsonConvert.DeserializeObject<Answer>(await result.Content.ReadAsStringAsync());
            return message;
        }
    }
}