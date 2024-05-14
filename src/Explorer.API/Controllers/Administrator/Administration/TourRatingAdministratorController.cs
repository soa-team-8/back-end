using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/tour-rating")]
    public class TourRatingAdministratorController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        public TourRatingAdministratorController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:3000");
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TourRatingDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var response = await _httpClient.GetAsync("/tour-ratings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<PagedResult<TourRatingDto>>($"{{\"items\": {content}}}");
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/tour-ratings/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
    }
}
