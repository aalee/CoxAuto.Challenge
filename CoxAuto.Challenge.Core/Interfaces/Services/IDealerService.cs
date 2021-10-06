using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Models;

namespace CoxAuto.Challenge.Core.Interfaces.Services
{
    public interface IDealerService
    {
        Task<Dealer> GetDealerInfoAsync(string datasetId, int dealerId);
    }
}