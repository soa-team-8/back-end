using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration;

[Authorize(Policy = "administratorPolicy")]
[Route("api/administration/equipment")]
public class EquipmentController : BaseApiController
{
    private readonly HttpClient _httpClient;

    public EquipmentController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:3000");
    }

    [HttpGet("paged")]
    public async Task<ActionResult> GetAllPaged([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = await _httpClient.GetAsync($"/equipment/paged?page={page}&pageSize={pageSize}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var response = await _httpClient.GetAsync("/equipment");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] EquipmentDto equipment)
    {
        var response = await _httpClient.PostAsJsonAsync("/equipment", equipment);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] EquipmentDto equipment)
    {
        var response = await _httpClient.PutAsJsonAsync($"/equipment/{id}", equipment);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"/equipment/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }
}