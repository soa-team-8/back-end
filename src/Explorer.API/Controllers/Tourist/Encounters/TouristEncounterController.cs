using Explorer.API.Services;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text;
using System.Text.Json;
using static Mysqlx.Notice.Warning.Types;

namespace Explorer.API.Controllers.Tourist.Encounters
{
    [Route("api/administration/touristEncounter")]
    public class TouristEncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly ImageService _imageService;
        private readonly HttpClient Client = new HttpClient();
        private readonly ITouristService _touristService;

        public TouristEncounterController(IEncounterService encounterService,ITouristService touristService)
        {
            _encounterService = encounterService;
            _touristService = touristService;
            _imageService = new ImageService();

        }

        [HttpPost]
        [Authorize(Policy = "touristPolicy")]
        public async Task<ActionResult<EncounterDto>> Create([FromForm] EncounterDto encounter, [FromQuery] long checkpointId, [FromQuery] bool isSecretPrerequisite, [FromForm] List<IFormFile>? imageF = null)
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
            
            var response = await Client.PostAsync($"http://localhost:3000/encounters/tourist/{_touristService.GetTouristById(User.PersonId()).Value.Level}/{User.PersonId()}", formData);
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
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<EncounterDto> Update([FromForm] EncounterDto encounter, [FromForm] List<IFormFile>? imageF = null)
        {

            if (imageF != null && imageF.Any())
            {
                var imageNames = _imageService.UploadImages(imageF);
                if (encounter.Type == "Location")
                    encounter.Image = imageNames[0];
            }

            var result = _encounterService.Update(encounter, User.PersonId());
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult Delete(int id)
        {
            var result = _encounterService.Delete(id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpGet]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<PagedResult<EncounterDto>> GetAll()
        {
            var result = _encounterService.GetPaged(0, 0);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<EncounterDto> GetById(int id)
        {
            var result = _encounterService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet("requestInfo/{encounterId:long}")]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<EncounterDto> GetRequestInfo(long encounterId)
        {
            var result = _encounterService.GetRequestInfo(encounterId);
            return CreateResponse(result);
        }

        // Funkcija za transformaciju koordinata
        private double TransformisiKoordinatu(double koordinata)
        {
            // Pretvori broj u string kako bi se mogao indeksirati
            string koordinataString = koordinata.ToString();

            // Ako je koordinata dovoljno dugačka
            if (koordinataString.Length > 2)
            {
                // Uzmi prva dva znaka
                string prviDeo = koordinataString.Substring(0, 2);

                // Uzmi ostatak broja posle prva dva znaka
                string drugiDeo = koordinataString.Substring(2);

                // Sastavi transformisanu vrednost
                string transformisanaKoordinataString = prviDeo + '.' + drugiDeo;

                // Parsiraj rezultat nazad kao double
                if (double.TryParse(transformisanaKoordinataString, NumberStyles.Any, CultureInfo.InvariantCulture, out double transformisanaKoordinata))
                {
                    return transformisanaKoordinata;
                }
            }

            // Ako je koordinata prekratka ili neuspešno parsiranje, vrati nepromenjenu vrednost
            return koordinata;
        }
    }
}
