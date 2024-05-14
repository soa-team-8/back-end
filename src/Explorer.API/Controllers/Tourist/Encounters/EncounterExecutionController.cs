using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using FluentResults;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Diagnostics.Metrics;
using System.Net;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;

namespace Explorer.API.Controllers.Tourist.Encounters
{
    [Route("api/tourist/encounter-execution")]

    // TODO use UserID and authorize
    [Authorize(Policy = "touristPolicy")]
    public class EncounterExecutionController : BaseApiController
    {
        private readonly IEncounterExecutionService _encounterExecutionService;
        private readonly IEncounterService _encounterService;
        private readonly ITouristService _touristService;
        static readonly HttpClient Client = new HttpClient();

        public EncounterExecutionController(IEncounterExecutionService encounterExecutionService, IEncounterService encounterService, ITouristService touristService)
        {
            _encounterExecutionService = encounterExecutionService;
            _encounterService = encounterService;
            _touristService = touristService;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<EncounterDto>> GetById([FromRoute] int id)
        {

            using HttpResponseMessage response = await Client.GetAsync("http://encounters-api:3030/encounters/execution/" + id);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return CreateResponse(jsonResponse.ToResult());
        }

        /*
        [HttpGet("{id:int}")]
        public ActionResult<EncounterDto> GetById([FromRoute] int id)
        {

            var result = _encounterExecutionService.Get(id);
            return CreateResponse(result);
        }
        */

        [HttpPut]
        public async Task<ActionResult<EncounterExecutionDto>> Update([FromForm] EncounterExecutionDto encounterExecution)
        {
            /*
            var result = _encounterExecutionService.Update(encounterExecution, User.PersonId());
            return CreateResponse(result);
            */
            using StringContent jsonContent = new(JsonSerializer.Serialize(encounterExecution), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await Client.PutAsync("http://encounters-api:3030/encounters/execution/" + User.PersonId() + "/" + encounterExecution.Id, jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return CreateResponse(jsonResponse.ToResult());
        }

        /*
        [HttpPut]
           public ActionResult<EncounterExecutionDto> Update([FromForm] EncounterExecutionDto encounterExecution)
           {
           var result = _encounterExecutionService.Update(encounterExecution, User.PersonId());
           return CreateResponse(result);
           }
         */


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            using HttpResponseMessage response = await Client.DeleteAsync("http://encounters-api:3030/encounters/execution/" + User.PersonId() + "/" + id);

            HttpStatusCode statusCode = response.StatusCode;
            string content = await response.Content.ReadAsStringAsync();

            ActionResult actionResult = statusCode switch
            {
                HttpStatusCode.OK => Ok(content),
                HttpStatusCode.NotFound => NotFound(content),
                _ => StatusCode((int)statusCode, content),
            };

            return actionResult;
        }

        /*
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _encounterExecutionService.Delete(id, User.PersonId());
            return CreateResponse(result);
        }
        */


        [HttpPut("activate/{id:int}")]
        public async Task<ActionResult<EncounterExecutionDto>> Activate([FromRoute] int id, [FromForm] double touristLatitude, [FromForm] double touristLongitude)
        {
            var queryString = $"?touristLatitude={Uri.EscapeDataString(touristLatitude.ToString().Replace(",", "."))}&touristLongitude={Uri.EscapeDataString(touristLongitude.ToString().Replace(",", "."))}";
            using HttpResponseMessage response = await Client.PutAsync("http://encounters-api:3030/encounters/execution/activate/" + User.PersonId() + "/" + id + queryString, null);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return CreateResponse(jsonResponse.ToResult());
            /*
            var result = _encounterExecutionService.Activate(User.PersonId(), touristLatitude, touristLongitude, id);
            return CreateResponse(result);
            */
        }


        /*
        [HttpPut("activate/{id:int}")]
        public ActionResult<EncounterExecutionDto> Activate([FromRoute] int id, [FromForm] double touristLatitude, [FromForm] double touristLongitude)
        {
            var result = _encounterExecutionService.Activate(User.PersonId(), touristLatitude, touristLongitude, id);
            return CreateResponse(result);
        }
        */

        [HttpPut("completed/{id:int}")]
        public async Task<ActionResult<EncounterExecutionDto>> CompleteExecution([FromRoute] int id, [FromForm] double touristLatitude, [FromForm] double touristLongitude)
        {
            var queryString = $"?touristLatitude={Uri.EscapeDataString(touristLatitude.ToString().Replace(",", "."))}&touristLongitude={Uri.EscapeDataString(touristLongitude.ToString().Replace(",", "."))}";
            using HttpResponseMessage response = await Client.PutAsync("http://encounters-api:3030/encounters/execution/complete/" + User.PersonId() + "/" + id + queryString, null);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            string jsonExecution = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                var xp = (int)JObject.Parse(jsonResponse)["xp"];
                try
                {
                    JObject parsedObject = JObject.Parse(jsonResponse);
                    var executionToken = parsedObject["execution"];
                    jsonExecution = executionToken.ToString();
                    _touristService.UpdateTouristXpAndLevel(User.PersonId(), xp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return CreateResponse(jsonExecution.ToResult());
            /*
            var result = _encounterExecutionService.CompleteExecution(id, User.PersonId(), touristLatitude, touristLongitude);
            if (result.IsSuccess)
                result = _encounterService.AddEncounter(result.Value);
            return CreateResponse(result);
            */
        }

        /*
                 [HttpPut("completed/{id:int}")]
           public ActionResult<EncounterExecutionDto> CompleteExecution([FromRoute] int id, [FromForm] double touristLatitude, [FromForm] double touristLongitude)
           {
           var result = _encounterExecutionService.CompleteExecution(id, User.PersonId(), touristLatitude, touristLongitude);
           if (result.IsSuccess)
           result = _encounterService.AddEncounter(result.Value);
           return CreateResponse(result);
           }
           
         */

    

        [HttpGet("get-all/{id:int}")]
        public async Task<ActionResult<PagedResult<EncounterExecutionDto>>> GetAllByTourist(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            if (id != User.PersonId())
            {
                return Unauthorized();
            }

            using HttpResponseMessage response = await Client.GetAsync("http://encounters-api:3030/encounters/execution/get-all-by-tourist/" + User.PersonId());
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return CreateResponse(jsonResponse.ToResult());

            //var result = _encounterExecutionService.GetAllByTourist(id, page, pageSize);
            //return CreateResponse(result);
        }

        [HttpGet("get-all-completed")]
        public async Task<ActionResult<PagedResult<EncounterExecutionDto>>> GetAllCompletedByTourist([FromQuery] int page, [FromQuery] int pageSize)
        {
            using HttpResponseMessage response = await Client.GetAsync("http://encounters-api:3030/encounters/execution/get-completed-by-tourist/" + User.PersonId());
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return CreateResponse(jsonResponse.ToResult());

            //var result = _encounterExecutionService.GetAllCompletedByTourist(User.PersonId(), page, pageSize);
            //return CreateResponse(result);
        }
        
        [HttpGet("get-by-tour/{id:int}")]
        public async Task<ActionResult<EncounterExecutionDto>> GetByTour([FromRoute] int id, [FromQuery] double touristLatitude, [FromQuery] double touristLongitude)
        {
            using HttpResponseMessage checkpointResponse = await Client.GetAsync("http://localhost:3000/checkpoints/get-encounter-ids/" + id);
            var jsonCheckpointResponse = await checkpointResponse.Content.ReadAsStringAsync();
            List<int> encounterIDs = JsonSerializer.Deserialize<List<int>>(jsonCheckpointResponse);

            var queryString = $"?touristLatitude={Uri.EscapeDataString(touristLatitude.ToString().Replace(",", "."))}&touristLongitude={Uri.EscapeDataString(touristLongitude.ToString().Replace(",", "."))}&encounterIds={string.Join(",", encounterIDs)}";

            using HttpResponseMessage response = await Client.GetAsync("http://encounters-api:3030/encounters/execution/get-by-tour/" + User.PersonId() + queryString);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return CreateResponse(jsonResponse.ToResult());

            /*var result = _encounterExecutionService.GetVisibleByTour(id, touristLongitude, touristLatitude, User.PersonId());
            if(result.IsSuccess)
                result = _encounterService.AddEncounter(result.Value);
            return CreateResponse(result);*/
        }


        [HttpGet("social/checkRange/{id:int}/{tourId:int}")]
        public async Task<ActionResult<EncounterExecutionDto>> CheckPosition([FromRoute] int tourId, [FromRoute] int id, [FromQuery] double touristLatitude, [FromQuery] double touristLongitude)
        {
            using HttpResponseMessage checkpointResponse = await Client.GetAsync("http://localhost:3000/checkpoints/get-encounter-ids/" + tourId);
            var jsonCheckpointResponse = await checkpointResponse.Content.ReadAsStringAsync();
            List<int> encounterIDs = JsonSerializer.Deserialize<List<int>>(jsonCheckpointResponse);


            var queryString = $"?touristLatitude={Uri.EscapeDataString(touristLatitude.ToString().Replace(",", "."))}&touristLongitude={Uri.EscapeDataString(touristLongitude.ToString().Replace(",", "."))}&encounterIds={string.Join(",", encounterIDs)}";

            var response = await Client.GetAsync($"http://encounters-api:3030/encounters/execution/social-encounter/check-range/{id}/{tourId}/{User.PersonId()}{queryString}");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            string jsonExecution = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                var xp = (int)JObject.Parse(jsonResponse)["xp"];

                try
                {
                    JObject parsedObject = JObject.Parse(jsonResponse);
                    var executionToken = parsedObject["execution"];
                    jsonExecution = executionToken.ToString();
                    _touristService.UpdateTouristXpAndLevel(User.PersonId(), xp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }




            return CreateResponse(jsonExecution.ToResult());
        }

        /*
        [HttpGet("social/checkRange/{id:int}/{tourId:int}")]
        public ActionResult<EncounterExecutionDto> CheckPosition([FromRoute] int tourId, [FromRoute] int id, [FromQuery] double touristLatitude, [FromQuery] double touristLongitude)
        {
            var result = _encounterExecutionService.GetWithUpdatedLocation(tourId, id, touristLongitude, touristLatitude, User.PersonId());
            if (result.IsSuccess)
                result = _encounterService.AddEncounter(result.Value);
            return CreateResponse(result);
        }
        */

        [HttpGet("location/checkRange/{id:int}/{tourId:int}")]
        public async Task<ActionResult<EncounterExecutionDto>> CheckPositionLocationEncounter([FromRoute] int tourId, [FromRoute] int id, [FromQuery] double touristLatitude, [FromQuery] double touristLongitude)
        {
            using HttpResponseMessage checkpointResponse = await Client.GetAsync("http://localhost:3000/checkpoints/get-encounter-ids/" + tourId);
            var jsonCheckpointResponse = await checkpointResponse.Content.ReadAsStringAsync();
            List<int> encounterIDs = JsonSerializer.Deserialize<List<int>>(jsonCheckpointResponse);



            var queryString = $"?touristLatitude={Uri.EscapeDataString(touristLatitude.ToString().Replace(",", "."))}&touristLongitude={Uri.EscapeDataString(touristLongitude.ToString().Replace(",", "."))}&encounterIds={string.Join(",", encounterIDs)}";

            var response = await Client.GetAsync($"http://encounters-api:3030/encounters/execution/location-encounter/check-range/{id}/{tourId}/{User.PersonId()}{queryString}");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            string jsonExecution = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                var xp = (int)JObject.Parse(jsonResponse)["xp"];
                try
                {
                    JObject parsedObject = JObject.Parse(jsonResponse);
                    var executionToken = parsedObject["execution"];
                    jsonExecution = executionToken.ToString();
                    _touristService.UpdateTouristXpAndLevel(User.PersonId(), xp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }




            return CreateResponse(jsonExecution.ToResult());
        }

        /*
        [HttpGet("location/checkRange/{id:int}/{tourId:int}")]
        public ActionResult<EncounterExecutionDto> CheckPositionLocationEncounter([FromRoute] int tourId, [FromRoute] int id, [FromQuery] double touristLatitude, [FromQuery] double touristLongitude)
        {
            var result = _encounterExecutionService.GetHiddenLocationEncounterWithUpdatedLocation(tourId, id, touristLongitude, touristLatitude, User.PersonId());
            if (result.IsSuccess)
                result = _encounterService.AddEncounter(result.Value);
            return CreateResponse(result);
        }
        */

        [HttpGet("active/by-tour/{id:int}")]
        public async Task<ActionResult<List<EncounterExecutionDto>>> GetActiveByTour([FromRoute] int id)
        {
            using HttpResponseMessage checkpointResponse = await Client.GetAsync("http://localhost:3000/checkpoints/get-encounter-ids/" + id);
            var jsonCheckpointResponse = await checkpointResponse.Content.ReadAsStringAsync();
            List<int> encounterIDs = JsonSerializer.Deserialize<List<int>>(jsonCheckpointResponse);

            var queryString = $"?encounterIds={string.Join(",", encounterIDs)}";

            using HttpResponseMessage response = await Client.GetAsync("http://encounters-api:3030/encounters/execution/get-active-by-tour/" + User.PersonId() + queryString);


            var jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());

            /*
            var result = _encounterExecutionService.GetActiveByTour(User.PersonId(), id);
            if (result.IsSuccess)
                result = _encounterService.AddEncounters(result.Value);
            return CreateResponse(result);
            */
        }
    }
}
