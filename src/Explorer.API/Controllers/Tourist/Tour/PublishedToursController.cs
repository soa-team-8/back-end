using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Recommendation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shopping")]
    public class PublishedToursController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        private readonly ITourRecommendationService _tourRecommendationService;

        public PublishedToursController(IHttpClientFactory httpClientFactory, ITourRecommendationService tourRecommendationService)
        {
            _tourRecommendationService = tourRecommendationService;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:3000");
        }

        [HttpGet]
        public async Task<ActionResult<List<TourPreviewDto>>> GetPublishedTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var response = await _httpClient.GetAsync("/published-tours");
            response.EnsureSuccessStatusCode();
            var contentString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TourPreviewDto>>(contentString);
            return Ok(result);
        }

        [HttpGet("details/{id:int}")]
        public async Task<ActionResult<List<TourPreviewDto>>> GetPublishedTour(int id)
        {
            var response = await _httpClient.GetAsync($"/published-tours/{id}");
            response.EnsureSuccessStatusCode();
            var contentString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TourPreviewDto>>($"[{contentString}]");
            return Ok(result);
        }

        [HttpGet("averageRating/{tourId:int}")]
        public async Task<ActionResult<double>> GetAverageRating(long tourId)
        {
            var response = await _httpClient.GetAsync($"/published-tours/{tourId}/rating");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<double>(content);
            return Ok(result);
        }

        [HttpGet("recommendations/{id:int}")]
        public ActionResult<List<TourPreviewDto>> GetToursByAuthor(int id)
        {
            var result = _tourRecommendationService.GetAppropriateTours(id);
            return CreateResponse(result);
        }

        [HttpGet("active-recommendations/{id:int}")]
        public ActionResult<List<TourPreviewDto>> GetActiveTourRecommendations(int id)
        {
            var result = _tourRecommendationService.GetAppropriateActiveTours(id);
            return CreateResponse(result);
        }
    }
}
