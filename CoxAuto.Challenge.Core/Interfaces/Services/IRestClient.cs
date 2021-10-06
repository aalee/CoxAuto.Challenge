using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Models;

namespace CoxAuto.Challenge.Core.Interfaces.Services
{
    public interface IRestClient
    {
        Task<HttpResponseMessage> GetDataSet();
        Task<HttpResponseMessage> GetVehicles(string datasetId);
        HttpResponseMessage GetVehicle(string datasetId, int vehicleId);
        Task<HttpResponseMessage> GetDealers(string datasetId, int dealersId);
        Task<HttpResponseMessage> PostAnswer(string datasetId, List<Dealer> dealers);
    }
}