using Xunit;
using CoxAuto.Challenge.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoxAuto.Challenge.Core.Interfaces.Services;

namespace CoxAuto.Challenge.Core.Services.Tests
{
    public class DatasetServiceTests
    {
        private IDatasetService _datasetService;
        public DatasetServiceTests()
        {
            _datasetService = new DatasetService(new RestClient());
        }
        [Fact()]
        public async Task GetDatasetIdTest()
        {
            var result = _datasetService.GetDatasetId();
            Assert.NotNull(result);
        }
    }
}