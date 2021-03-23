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
    public class DadJokeControllerTests
    {
        [Theory]
        [AutoData]
        public async Task GetRandomJoke_JokeExists_RandomJokeReturned(Mock<IRestClient> mockRestClient, Mock<IRestResponse<RandomJokeResponse>> mockResponse, RandomJokeResponse expectedResult)
        {
            mockResponse.Setup(mock => mock.Data).Returns(expectedResult);
            mockRestClient.Setup(mock => mock.ExecuteGetAsync<RandomJokeResponse>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse.Object);

            var cache = Create.MockedCachingService();
            DadJokeController controller = new DadJokeController(cache, mockRestClient.Object);

            var response = await controller.RandomJoke();

            var mockCache = Mock.Get(cache);
            mockCache.Verify();
            mockResponse.Verify();
            mockRestClient.Verify(x => x.ExecuteGetAsync<RandomJokeResponse>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(expectedResult.Id, response.Id);
            Assert.Equal(expectedResult.Joke, response.Joke);
        }

        [Theory]
        [AutoData]
        public async Task GetJokeById_JokeExists_JokeReturned(Mock<IRestClient> mockRestClient, Mock<IRestResponse<RandomJokeResponse>> mockResponse, RandomJokeResponse expectedResult)
        {
            mockResponse.Setup(mock => mock.Data).Returns(expectedResult);
            mockRestClient.Setup(mock => mock.ExecuteGetAsync<RandomJokeResponse>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse.Object);

            var cache = Create.MockedCachingService();
            DadJokeController controller = new DadJokeController(cache, mockRestClient.Object);

            var response = await controller.Get(expectedResult.Id);

            var mockCache = Mock.Get(cache);
            mockCache.Verify();
            mockResponse.Verify();
            mockRestClient.Verify(x => x.ExecuteGetAsync<RandomJokeResponse>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(expectedResult.Id, response.Id);
            Assert.Equal(expectedResult.Joke, response.Joke);
        }
    }
}
