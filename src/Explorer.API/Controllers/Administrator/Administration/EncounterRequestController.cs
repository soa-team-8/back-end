using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "touristAndAdministratorPolicy")]
    [Route("api/administration/encounterRequests")]
    public class EncounterRequestController : BaseApiController
    {
        private readonly IEncounterRequestService _encounterRequestService;
        private readonly HttpClient Client = new HttpClient();

        public EncounterRequestController(IEncounterRequestService encounterRequestService)
        {
            _encounterRequestService = encounterRequestService;
        }

        [HttpPost]
        public ActionResult<EncounterRequestDto> Create([FromBody] EncounterRequestDto request)
        {
            var result = _encounterRequestService.Create(request);
            return CreateResponse(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EncounterRequestDto>>> GetAll()
        {
            using HttpResponseMessage response = await Client.GetAsync("http://encounters-api:3030/requests/getAll");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }

        [HttpPut("accept/{id:int}")]
        public async Task<ActionResult<EncounterRequestDto>> AcceptRequest(int id)
        {
            using HttpResponseMessage response = await Client.PutAsync($"http://encounters-api:3030/requests/acceptReq/{id}", null);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }

        [HttpPut("reject/{id:int}")]
        public async Task<ActionResult<EncounterRequestDto>> RejectRequest(int id)
        {
            using HttpResponseMessage response = await Client.PutAsync($"http://encounters-api:3030/requests/rejectReq/{id}", null);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }

    }
}
