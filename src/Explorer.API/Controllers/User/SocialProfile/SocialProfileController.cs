using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Explorer.API.Controllers.User.SocialProfile
{
    [Authorize(Policy = "userPolicy")]
    [Route("api/social-profile")]
    public class SocialProfileController : BaseApiController
    {
        private readonly HttpClient _httpClient;
        private readonly ISocialProfileService _userProfileService;
        

        public SocialProfileController(IHttpClientFactory httpClientFactory, ISocialProfileService userProfileService)
        {
            _userProfileService = userProfileService;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:9090");
        }

        [HttpPost("follow/{followerId:int}/{followedId:int}")]
        public async Task<ActionResult<SocialProfileDto>> Follow(int followerId, int followedId)
        {
            var response = await _httpClient.PostAsync($"/social-profile/follow/{followerId}/{followedId}", null);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SocialProfileDto>(jsonResponse);
            return Ok(result);
        }

        [HttpPost("un-follow/{followerId:int}/{unFollowedId:int}")]
        public async Task<ActionResult<SocialProfileDto>> UnFollow(int followerId, int unFollowedId)
        {
            var response = await _httpClient.PostAsync($"/social-profile/unfollow/{followerId}/{unFollowedId}", null);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SocialProfileDto>(jsonResponse);
            return Ok(result);
        }

        [HttpGet("get/{userId:int}")]
        public async Task<ActionResult<SocialProfileDto>> GetSocialProfile(int userId)
        {
            var response = await _httpClient.GetAsync($"/social-profile/{userId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SocialProfileDto>(jsonResponse);
            return Ok(result);
        }

        [HttpGet("recommendations/{userId:int}")]
        public async Task<ActionResult<List<UserDto>>> GetRecommendations(int userId)
        {
            var response = await _httpClient.GetAsync($"/social-profile/recommendations/{userId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<UserDto>>(jsonResponse);
            return Ok(result);
        }
    }
}
