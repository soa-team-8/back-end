using Explorer.API.Services;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/encounter")]
    [Authorize(Policy = "authorPolicy")]


    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly ImageService _imageService;
        private readonly HttpClient Client = new HttpClient();

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

            var response = await Client.PostAsync($"http://localhost:3000/encounters/author", formData);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var encId = (int)JObject.Parse(jsonResponse)["id"];
                response = await Client.PutAsync($"http://localhost:3000/checkpoints/setEnc/{checkpointId}/{encId}/{isSecretPrerequisite}/{encounter.AuthorId}", formData);
            }

            jsonResponse = await response.Content.ReadAsStringAsync();
            return CreateResponse(jsonResponse.ToResult());
        }

        [HttpPut]
        public ActionResult<EncounterDto> Update([FromForm] EncounterDto encounter, [FromForm] List<IFormFile>? imageF = null)
        {

            if (imageF != null && imageF.Any())
            {
                var imageNames = _imageService.UploadImages(imageF);
                if (encounter.Type == "Location")
                    encounter.Image = imageNames[0];
            }

            var result = _encounterService.Update(encounter,User.PersonId());
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _encounterService.Delete(id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<EncounterDto> GetById(int id)
        {
            var result = _encounterService.Get(id);
            return CreateResponse(result);
        }

    }
}
