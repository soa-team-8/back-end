using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-rating")]
    public class TourRatingTouristController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        public TourRatingTouristController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://tours-api:3000");
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TourRatingDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var response = await _httpClient.GetAsync("/tour-ratings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

		[HttpGet("getTourRating/{id:int}")]
		public async Task<ActionResult<TourRatingDto>> GetTourRating(int id)
		{
            var response = await _httpClient.GetAsync($"/tour-ratings/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TourRatingDto>(jsonResponse);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TourRatingDto>> Create([FromForm] TourRatingDto tourRating, [FromForm] List<IFormFile>? images = null)
        {
            var formData = new MultipartFormDataContent();

            var tourRatingJson = JsonConvert.SerializeObject(tourRating);
            formData.Add(new StringContent(tourRatingJson), "tourRating");

            if (images != null)
            {
                foreach (var image in images)
                {
                    formData.Add(new StreamContent(image.OpenReadStream()), "images", image.FileName);
                }
            }

            var response = await _httpClient.PostAsync("/tour-ratings", formData);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var createdTourRating = JsonConvert.DeserializeObject<TourRatingDto>(jsonResponse);

            return Ok(createdTourRating);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TourRatingDto>> Update([FromBody] TourRatingDto tourRating, long id)
        {
            var formData = new MultipartFormDataContent();

            var checkpointJson = JsonConvert.SerializeObject(tourRating);
            formData.Add(new StringContent(checkpointJson), "tourRating");

            var response = await _httpClient.PutAsync($"/tour-ratings/{id}", formData);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var createdCheckpoint = JsonConvert.DeserializeObject<TourRatingDto>(jsonResponse);

            return Ok(createdCheckpoint);
        }
    }
}
