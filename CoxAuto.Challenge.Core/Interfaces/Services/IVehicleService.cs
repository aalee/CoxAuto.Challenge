using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Models;
using CoxAuto.Challenge.Core.Models.Response;

namespace CoxAuto.Challenge.Core.Interfaces.Services
{
    public interface IVehicleService
    {
        Vehicles GetVehiclesAsync(string datasetId);
        Task<Vehicle> GetVehicleAsync(string datasetId, int vehicleId);
    }
}