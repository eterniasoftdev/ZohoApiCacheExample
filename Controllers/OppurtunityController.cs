using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace ApiCacheExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpportunityController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IDatabase _redis;

        public OpportunityController(HttpClient client, IConnectionMultiplexer muxer)
        {
            _client = client;
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ZohoCachingApp", "1.0"));
            _redis = muxer.GetDatabase();
        }

        [HttpGet(Name = "GetOpportunity")]
        public async Task<ResultTemplate> Get(string? Deal_Name, string? id)
        {
            string json;
            var watch = Stopwatch.StartNew();
            var keyName = $"forecast:{Deal_Name},{id}";
            json = await _redis.StringGetAsync(keyName);
            if (string.IsNullOrEmpty(json))
            {
                json = await GetOpportunity();
                var setTask = _redis.StringSetAsync(keyName, json);
                var expireTask = _redis.KeyExpireAsync(keyName, TimeSpan.FromSeconds(3600));
                await Task.WhenAll(setTask, expireTask);
            }

            var SerialData =
                JsonSerializer.Deserialize<IEnumerable<DataTemplate>>(json);
            watch.Stop();
            var result = new ResultTemplate(SerialData);

            return result;
        }

        private async Task<string> GetOpportunity()
        {
            
            var RequestQuery = $"https://www.zohoapis.in/crm/v6/Deals?fields=Customer_Name,Account_Name,Deal_Name&page=1&per_page=200&cvid=158127000092619482";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "1000.f72347bc0bb9085b290781b4af0c79ac.736339182047d8445cf73d35a5dd77fc");
            var Result = await _client.GetFromJsonAsync<JsonObject>(RequestQuery);
            var getdataJson = Result["data"].ToJsonString();
            return getdataJson;
        }
    }
}
