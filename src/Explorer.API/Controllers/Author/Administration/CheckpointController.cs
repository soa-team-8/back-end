using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Explorer.API.Controllers.Author.Administration;

[Route("api/administration/checkpoint")]
public class CheckpointController : BaseApiController
{
    private readonly HttpClient _httpClient;

    public CheckpointController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:3000");
    }

    //[HttpPost("create/{status}")]
    //[Authorize(Policy = "authorPolicy")]
    public async Task<ActionResult<CheckpointDto>> Create([FromForm] CheckpointDto checkpoint, [FromRoute] string status, [FromForm] List<IFormFile>? pictures = null)
    {
        var formData = new MultipartFormDataContent();

        var checkpointJson = JsonConvert.SerializeObject(checkpoint);
        formData.Add(new StringContent(checkpointJson), "checkpoint");

        if (pictures != null)
        {
            foreach (var picture in pictures)
            {
                formData.Add(new StreamContent(picture.OpenReadStream()), "pictures", picture.FileName);
            }
        }

        var response = await _httpClient.PostAsync("/checkpoints", formData);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var createdCheckpoint = JsonConvert.DeserializeObject<CheckpointDto>(jsonResponse);

        return Ok(createdCheckpoint);
    }

    //[HttpPut("create-secret/{checkpointId:int}")]
    //[Authorize(Policy = "authorPolicy")]
    public async Task<ActionResult<CheckpointDto>> CreateCheckpointSecret([FromForm] CheckpointSecretDto checkpointSecret, int checkpointId, [FromForm] List<IFormFile>? pictures = null)
    {
        var formData = new MultipartFormDataContent();

        var checkpointSecretJson = JsonConvert.SerializeObject(checkpointSecret);
        formData.Add(new StringContent(checkpointSecretJson), "checkpointSecret");

        if (pictures != null)
        {
            foreach (var picture in pictures)
            {
                formData.Add(new StreamContent(picture.OpenReadStream()), "pictures", picture.FileName);
            }
        }

        var response = await _httpClient.PutAsync($"/checkpoints/{checkpointId}/checkpoint-secret", formData);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var createdCheckpoint = JsonConvert.DeserializeObject<CheckpointDto>(jsonResponse);

        return Ok(createdCheckpoint);
    }

    //[HttpPut("update-secret/{checkpointId:int}")]
    //[Authorize(Policy = "authorPolicy")]
    public async Task<ActionResult<CheckpointDto>> UpdateCheckpointSecret([FromForm] CheckpointSecretDto checkpointSecret, int checkpointId, [FromForm] List<IFormFile>? pictures = null)
    {
        var formData = new MultipartFormDataContent();

        var checkpointSecretJson = JsonConvert.SerializeObject(checkpointSecret);
        formData.Add(new StringContent(checkpointSecretJson), "checkpointSecret");

        if (pictures != null)
        {
            foreach (var picture in pictures)
            {
                formData.Add(new StreamContent(picture.OpenReadStream()), "pictures", picture.FileName);
            }
        }

        var response = await _httpClient.PutAsync($"/checkpoints/{checkpointId}/checkpoint-secret", formData);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var createdCheckpoint = JsonConvert.DeserializeObject<CheckpointDto>(jsonResponse);

        return Ok(createdCheckpoint);
    }

   // [HttpDelete("{id:int}")]
    //[Authorize(Policy = "authorPolicy")]
    public async Task<ActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"/checkpoints/{id}");
        response.EnsureSuccessStatusCode();
        return Ok(new { message = "Checkpoint deleted successfully" });
    }

    //[HttpGet("{id:int}")]
    //[Authorize(Policy = "authorPolicy")]
    public async Task<ActionResult> GetAllByTour([FromQuery] int page, [FromQuery] int pageSize, int id)
    {
        var response = await _httpClient.GetAsync($"/checkpoints/{id}/tour");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("details/{id:int}")]
    [Authorize(Policy = "authorPolicy")]
    public async Task<ActionResult> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"/checkpoints/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    //[HttpPut("{id:int}")]
    //[Authorize(Policy = "authorPolicy")]
    public async Task<ActionResult<CheckpointDto>> Update([FromForm] CheckpointDto checkpoint, int id, [FromForm] List<IFormFile>? pictures = null)
    {
        var formData = new MultipartFormDataContent();

        var checkpointJson = JsonConvert.SerializeObject(checkpoint);
        formData.Add(new StringContent(checkpointJson), "checkpoint");

        if (pictures != null)
        {
            foreach (var picture in pictures)
            {
                formData.Add(new StreamContent(picture.OpenReadStream()), "pictures", picture.FileName);
            }
        }

        var response = await _httpClient.PutAsync($"/checkpoints/{id}", formData);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var createdCheckpoint = JsonConvert.DeserializeObject<CheckpointDto>(jsonResponse);

        return Ok(createdCheckpoint);
    }

    //[HttpGet]
    public async Task<ActionResult> GetAllPaged([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = await _httpClient.GetAsync("/checkpoints");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }
}