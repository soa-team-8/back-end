using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Recommendation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tour-execution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        private readonly ITourExecutionService _tourExecutionService;
        private readonly ITourRecommendationService _tourRecommendationService;
        private readonly IEmailService _emailService;

        public TourExecutionController(ITourExecutionService tourExecutionService, ITourRecommendationService tourRecommendationService,
            IEmailService emailService, IHttpClientFactory httpClientFactory)
        {
            _tourExecutionService = tourExecutionService;
            _tourRecommendationService = tourRecommendationService;
            _emailService = emailService;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://tours-api:3000");
        }

        [HttpPost("{tourId:int}")]
        public async Task<ActionResult<TourExecutionDto>> Create(long tourId)
        {
            var response = await _httpClient.PostAsync($"/tour-executions/{User.PersonId()}/{tourId}", null);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TourExecutionDto>(jsonResponse);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TourExecutionDto>> CheckPosition([FromBody] TouristPositionDto touristPosition, long id)
        {
            var jsonTouristPosition = JsonConvert.SerializeObject(touristPosition);
            var content = new StringContent(jsonTouristPosition, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/tour-executions/{id}", content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TourExecutionDto>(jsonResponse);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<TourExecutionDto>> Get([FromQuery] long tourId)
        {
            var response = await _httpClient.GetAsync($"/tour-executions/{User.PersonId()}/{tourId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TourExecutionDto>(jsonResponse);
            return Ok(result);
        }

        [HttpPut("abandoned")]
        public async Task<ActionResult<TourExecutionDto>> Abandon([FromBody] long id)
        {
            var response = await _httpClient.PutAsync($"/tour-executions/{User.PersonId()}/{id}", null);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TourExecutionDto>(jsonResponse);
            return Ok(result);
        }

        [HttpGet("get-suggested-tours/{id:int}")]
        public ActionResult<TourExecutionDto> GetSuggestedTours(long id)
        {
            var result = _tourExecutionService.GetSuggestedTours(id, User.PersonId(), _tourRecommendationService.GetAppropriateTours(User.PersonId()));
            return CreateResponse(result);
        }

        [HttpGet("send-tours-to-mail/{id:int}")]
        public ActionResult<TourPreviewDto> SendRecommendedToursToMail(long id)
        {
			var result = _tourExecutionService.GetSuggestedTours(id, User.PersonId(), _tourRecommendationService.GetAppropriateTours(User.PersonId()));
            string email = _tourExecutionService.GetEmailByUserId(User.PersonId()).Value;
            string name = _tourExecutionService.GetNameByUserId(User.PersonId()).Value;
			List<long> recommendedToursIds = new List<long>();
            List<string> tourNames = new List<string>();
			foreach (var rt in result.Value)
            {
                recommendedToursIds.Add(rt.Id);
                tourNames.Add(rt.Name);
            }
			_emailService.SendRecommendedToursEmail(email, name, recommendedToursIds, tourNames);
			return CreateResponse(result);
		}
    }
}
