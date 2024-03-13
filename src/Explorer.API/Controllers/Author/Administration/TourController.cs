using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration;

[Authorize(Policy = "authorPolicy")]
[Route("api/administration/tour")]
public class TourController : BaseApiController
{
    private readonly ITourService _tourService;
    private readonly HttpClient _httpClient;

    public TourController(ITourService tourService, IHttpClientFactory httpClientFactory)
    {
        _tourService = tourService;
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:3000");
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TourDto tour)
    {
        var response = await _httpClient.PostAsJsonAsync("/tours", tour);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("by-author")]
    public async Task<ActionResult<List<TourDto>>> GetToursByAuthor([FromQuery] int page, [FromQuery] int pageSize)
    {
        var authorId = User.PersonId();

        var response = await _httpClient.GetAsync($"/tours/{authorId}/by-author");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var response = await _httpClient.GetAsync("/tours");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpPut("add/{tourId:int}/{equipmentId:int}")]
    public async Task<ActionResult> AddEquipment(int tourId, int equipmentId)
    {
        var response = await _httpClient.PutAsync($"/tours/{tourId}/{equipmentId}/add", null);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpPut("remove/{tourId:int}/{equipmentId:int}")]
    public async Task<ActionResult> RemoveEquipment(int tourId, int equipmentId)
    {
        var response = await _httpClient.PutAsync($"/tours/{tourId}/{equipmentId}/remove", null);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("details/{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"/tours/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"/tours/{id}");
        response.EnsureSuccessStatusCode();
        return Ok(new { message = "Tour deleted successfully" });
    }

    // TODO: call on back-end

    [HttpPut("publishedTours/{id:int}")]
    public ActionResult<TourDto> Publish(int id)
    {
        var result = _tourService.Publish(id, User.PersonId());
        return CreateResponse(result);
    }

    [HttpPut("archivedTours/{id:int}")]
    public ActionResult<TourDto> Archive(int id)
    {
        var result = _tourService.Archive(id, User.PersonId());
        return CreateResponse(result);
    }

    [HttpPut("tourTime/{id:int}")]
    public ActionResult<TourDto> AddTime(TourTimesDto tourTimesDto, int id)
    {
        var result = _tourService.AddTime(tourTimesDto, id, User.PersonId());
        return CreateResponse(result);
    }

    [HttpPut("{id:int}")]
    public ActionResult<TourDto> Update([FromBody] TourDto tour)
    {
        tour.Equipment = new List<EquipmentDto>();
        var result = _tourService.Update(tour, User.PersonId());
        return CreateResponse(result);
    }
}