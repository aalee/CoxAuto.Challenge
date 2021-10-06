using System.Collections.Generic;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Response;

namespace CoxAuto.Challenge.Core.Interfaces.Services
{
    public interface IDatasetService
    {
        Task<string> GetDatasetId();
        Task<Answer?> PostAnswer(string datasetId, List<Dealer> dealers);
    }
}