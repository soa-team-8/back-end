using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Explorer.API.Controllers.Administrator.Administration
{
        [Authorize(Policy = "administratorPolicy")]
        [Route("api/administration/reportedIssues")]
        public class ReportedIssuesReviewController : BaseApiController
        {
        private readonly HttpClient _httpClient;

        public ReportedIssuesReviewController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:3000");
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReportedIssueDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var response = await _httpClient.GetAsync($"/reported-issues");
            response.EnsureSuccessStatusCode();
            var contentString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<PagedResult<ReportedIssueDto>>($"{{\"items\": {contentString}}}");
            return Ok(result);
        }

        [HttpPost("comment/{id:int}")]
        public async Task<ActionResult<ReportedIssueDto>> Respond([FromRoute] int id, [FromBody] ReportedIssueCommentDto ric)
        {
            var json = JsonConvert.SerializeObject(ric);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"/reported-issues/{id}/comment", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReportedIssueDto>(responseContent);
            return Ok(result);
        }

        [HttpPut("deadline/{id:int}")]
        public async Task<ActionResult<ReportedIssueDto>> AddDeadline(int id, [FromBody] DateTime deadline)
        {
            var json = JsonConvert.SerializeObject(deadline);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/reported-issues/{id}/deadline", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReportedIssueDto>(responseContent);
            return Ok(result);
        }

        [HttpPut("penalizeAuthor/{id:int}")]
        public async Task<ActionResult<ReportedIssueDto>> PenalizeAuthor(int id)
        {
            var response = await _httpClient.PutAsync($"/reported-issues/{id}/penalize", null);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReportedIssueDto>(responseContent);
            return Ok(result);
        }

        [HttpPut("closeReportedIssue/{id:int}")]
        public async Task<ActionResult<ReportedIssueDto>> Close(int id)
        {
            var response = await _httpClient.PutAsync($"/reported-issues/{id}/close", null);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ReportedIssueDto>(responseContent);
            return Ok(result);
        }
    }
}
