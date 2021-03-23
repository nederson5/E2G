using AutoFixture.Xunit2;
using DadJokeService.Controllers;
using DadJokeService.Models;
using LazyCache.Testing.Moq;
using Moq;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DadJokeService.Tests.Unit.Controllers
{
    public class DadJokeApiTests
    {
        [Theory]
        [AutoData]
        public async Task RandomJoke_JokeExists_RandomJokeReturned(Mock<IRestClient> mockRestClient, Mock<IRestResponse<RandomJokeResponse>> mockResult, RandomJokeResponse expectedResult)
        {
            var mockCache = Create.MockedCachingService();
            mockResult.Setup(mock => mock.Data).Returns(expectedResult);
            mockRestClient.Setup(mock => mock.ExecuteGetAsync<RandomJokeResponse>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResult.Object);
            DadJokeController controller = new DadJokeController(mockCache, mockRestClient.Object);

            var joke = await controller.RandomJoke();

            var actualCache = Mock.Get(mockCache);

            Assert.Equal(expectedResult.Joke, joke);
        }
    }
}
