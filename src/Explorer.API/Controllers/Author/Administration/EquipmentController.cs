using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Explorer.API.Controllers.Author.Administration;

[Authorize(Policy = "authorPolicy")]
[Route("api/manipulation/equipment")]
public class EquipmentController : BaseApiController
{
    private readonly HttpClient _httpClient;

    public EquipmentController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:3000");
    }

    [HttpPost("get-available/{id:int}")]
    public async Task<ActionResult<List<EquipmentDto>>> GetAvailableEquipment([FromBody] List<long> currentEquipmentIds, int id)
    {
        var response = await _httpClient.PostAsJsonAsync($"/equipment/{id}/get-available", currentEquipmentIds);
        response.EnsureSuccessStatusCode();
        var contentString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<EquipmentDto>>(contentString);
        return Ok(result);
    }
}