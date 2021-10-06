using CoxAuto.Challenge.Core.Interfaces.Services;
using Xunit;

namespace CoxAuto.Challenge.Core.Services.Tests
{
    public class DatasetServiceTests
    {
        private readonly IDatasetService _datasetService;

        public DatasetServiceTests()
        {
            _datasetService = new DatasetService(new RestClient());
        }

        [Fact]
        public void GetDatasetIdTest()
        {
            var result = _datasetService.GetDatasetId();
            Assert.NotNull(result);
        }
    }
}