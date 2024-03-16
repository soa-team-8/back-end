using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/tour-rating")]
    public class TourRatingAuthorController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        public TourRatingAuthorController(IHttpClientFactory httpClientFactory)
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
            return Ok(content);
        }
    }
}
