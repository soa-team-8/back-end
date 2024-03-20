using Explorer.API.Services;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/encounter")]
    //[Authorize(Policy = "authorPolicy")]


    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly ImageService _imageService;
        static readonly HttpClient Client = new HttpClient();

        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
            _imageService = new ImageService();
        }


        [HttpPost]
        public async Task<ActionResult<EncounterDto>> Create([FromForm] EncounterDto encounter,[FromQuery] long checkpointId, [FromQuery] bool isSecretPrerequisite, [FromForm] List<IFormFile>? imageF = null)
        {

            using var formData = new MultipartFormDataContent();
            var jsonContent = new StringContent(JsonSerializer.Serialize(encounter));
            formData.Add(jsonContent, "encounter");

            // Dodajte slike u multipart form data
            if (imageF != null && imageF.Any())
            {
                foreach (var imageFile in imageF)
                {
                    var imageStream = imageFile.OpenReadStream();
                    var imageContent = new StreamContent(imageStream);
                    formData.Add(imageContent, "pictures", imageFile.FileName);
                }
            }

            var response = await Client.PostAsync($"http://localhost:3030/encounters/author", formData);;
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var encId = (long)JObject.Parse(jsonResponse)["id"];
                response = await Client.PutAsync($"http://localhost:3000/checkpoints/setEnc/{checkpointId}/{encId}/{isSecretPrerequisite}", null);
            }

            jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }

        [HttpPut]
        public async Task<ActionResult<EncounterDto>> Update([FromForm] EncounterDto encounter, [FromForm] List<IFormFile>? imageF = null)
        {

            if (imageF != null && imageF.Any())
            {
                var imageNames = _imageService.UploadImages(imageF);
                if (encounter.Type == "Location")
                    encounter.Image = imageNames[0];
            }

            using StringContent jsonContent = new(JsonSerializer.Serialize(encounter), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await Client.PutAsync("http://localhost:3030/encounters/" + encounter.Id, jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();


            //var result = _encounterService.Update(encounter,User.PersonId());
            return CreateResponse(jsonResponse.ToResult());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var result = _encounterService.Delete(id, User.PersonId());
            using HttpResponseMessage response = await Client.DeleteAsync("http://localhost:3030/encounters/" + id);

            // Extract status code from HttpResponseMessage
            HttpStatusCode statusCode = response.StatusCode;

            // Extract content from HttpResponseMessage
            string content = await response.Content.ReadAsStringAsync();

            // Create ActionResult based on status code and content
            ActionResult actionResult = statusCode switch
            {
                HttpStatusCode.OK => Ok(content),
                HttpStatusCode.NotFound => NotFound(content),
                _ => StatusCode((int)statusCode, content),
            };

            // Return ActionResult
            return actionResult;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EncounterDto>> GetById(int id)
        {
            //var result = _encounterService.Get(id);
            using HttpResponseMessage response = await Client.GetAsync("http://localhost:3030/encounters/" + id);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return CreateResponse(jsonResponse.ToResult());
        }

    }
}
