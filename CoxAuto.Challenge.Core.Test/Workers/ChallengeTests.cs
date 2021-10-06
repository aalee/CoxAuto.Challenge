using Xunit;

namespace CoxAuto.Challenge.Core.Workers.Tests
{
    public class ChallengeTests
    {
        [Fact]
        public void RunAsyncTest()
        {
            var messageAnswer = Challenge.RunAsync();

            Assert.NotNull(messageAnswer);
            if (messageAnswer?.Result != null) Assert.NotNull(messageAnswer?.Result.Message);
        }
    }
}