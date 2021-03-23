using DadJokeService.Models;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DadJokeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DadJokeController : ControllerBase
    {
        private readonly IAppCache jokeCache;
        private readonly IRestClient client;
        private const string resource = "https://icanhazdadjoke.com/";

        public DadJokeController(IAppCache jokeCache, IRestClient client)
        {
            this.jokeCache = jokeCache;
            this.client = client;
        }

        [HttpGet("randomJoke")]
        public async Task<RandomJokeResponse> RandomJoke()
        {
            var request = new RestRequest(resource);
            var response = await client.ExecuteGetAsync<RandomJokeResponse>(request);

            jokeCache.Add(response.Data.Id, response.Data, DateTimeOffset.Now.AddMinutes(20));

            return response.Data;
        }

        [HttpGet("{id}")]
        public async Task<RandomJokeResponse> Get(string id)
        {
            var request = new RestRequest(resource);
            request.Resource = "j/{id}";
            request.AddParameter("id", id, ParameterType.UrlSegment);
            Func<Task<RandomJokeResponse>> response = async () => (await client.ExecuteGetAsync<RandomJokeResponse>(request)).Data;

            var joke = await jokeCache.GetOrAddAsync(id, response);

            return joke;
        }
    }
}
