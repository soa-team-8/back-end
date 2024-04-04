using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/reportingIssue")]
    public class ReportingIssueController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        public ReportingIssueController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://tours-api:3000");
        }

        [HttpPost("{category}/{description}/{priority}/{tourId}/{touristId}")]
        public async Task<ActionResult<ReportedIssueDto>> Create(string category, string description, int priority, int tourId, int touristId)
        {
            var response = await _httpClient.PostAsync($"/reported-issues/{category}/{description}/{priority}/{tourId}/{touristId}", null);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReportedIssueDto>(responseContent);

            return Ok(result);
        }

        [HttpPut("resolve/{id:int}")]
        public async Task<ActionResult<ReportedIssueDto>> Resolve(int id)
        {
            var response = await _httpClient.PutAsync($"/reported-issues/{id}/resolve", null);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReportedIssueDto>(responseContent);

            return Ok(result);
        }

        [HttpPost("comment/{id:int}")]
        public async Task<ActionResult<ReportedIssueDto>> AddComment([FromBody] ReportedIssueCommentDto ric, int id)
        {
            var json = JsonConvert.SerializeObject(ric);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"/reported-issues/{id}/comment", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReportedIssueDto>(responseContent);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PagedResult<ReportedIssueDto>>> GetAllByTourist(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var response = await _httpClient.GetAsync($"/reported-issues/{id}/tourist");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            
            var result = JsonConvert.DeserializeObject<PagedResult<ReportedIssueDto>>($"{{\"items\": {responseContent}}}");
            return Ok(result);
        }
    }
}
