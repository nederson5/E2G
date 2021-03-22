using DadJokeService.Models;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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


        // GET: api/<DadJokeController>
        [HttpGet("randomJoke")]
        public async Task<string> RandomJoke()
        {
            var request = new RestRequest(resource);
            var response = await client.ExecuteGetAsync<RandomJokeResponse>(request);

            jokeCache.Add(response.Data.Id, response.Data.Joke, DateTimeOffset.Now.AddMinutes(20));

            return response.Data.Joke;
        }

        // GET api/<DadJokeController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            var request = new RestRequest(resource);
            request.Resource = "j/{id}";
            request.AddParameter("id", id, ParameterType.UrlSegment);
            Func<Task<string>> response = async () => (await client.GetAsync<RandomJokeResponse>(request)).Joke;

            var joke = await jokeCache.GetOrAddAsync(id, response);

            return joke;
        }
    }
}
