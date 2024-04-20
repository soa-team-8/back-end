using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

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

            if (response.IsSuccessStatusCode)
            {
                return Ok("Followed successfully.");
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest("Invalid follower ID or followed ID.");
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return StatusCode(500, "Failed to follow user.");
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Unknown error occurred.");
            }


            //var result = _userProfileService.Follow(followerId, followedId);

            //return CreateResponse(result);
        }

        [HttpPost("un-follow/{followerId:int}/{unFollowedId:int}")]
        public async Task<ActionResult<SocialProfileDto>> UnFollow(int followerId, int unFollowedId)
        {
            var response = await _httpClient.PostAsync($"/social-profile/unfollow/{followerId}/{unFollowedId}", null);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Followed successfully.");
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest("Invalid follower ID or followed ID.");
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return StatusCode(500, "Failed to follow user.");
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Unknown error occurred.");
            }
            //var result = _userProfileService.UnFollow(followerId, unFollowedId);

            //return CreateResponse(result);
        }

        [HttpGet("get/{userId:int}")]
        public async Task<ActionResult<SocialProfileDto>> GetSocialProfile(int userId)
        {

            var socialProfile = _userProfileService.Get(userId);

            return CreateResponse(socialProfile);
        }
    }
}
