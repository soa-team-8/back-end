using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/reported-issue-response")]
    public class ReportedIssueRespondingController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        public ReportedIssueRespondingController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:3000");
        }

        [HttpPost("response/{id:int}")]
        public async Task<ActionResult<ReportedIssueDto>> Respond([FromBody] ReportedIssueCommentDto ric, int id)
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
        public async Task<ActionResult<PagedResult<ReportedIssueDto>>> GetAllByAuthor(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var response = await _httpClient.GetAsync($"/reported-issues/{id}/author");
            response.EnsureSuccessStatusCode();
            var contentString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PagedResult<ReportedIssueDto>>(contentString);
            return Ok(result);
        }
    }
}
