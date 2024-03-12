﻿using Explorer.API.Services;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Explorer.API.Controllers.Author.Administration;

[Route("api/administration/checkpoint")]
public class CheckpointController : BaseApiController
{
    private readonly ICheckpointService _checkpointService;
    private readonly ImageService _imageService;
    private readonly HttpClient _httpClient;

    public CheckpointController(ICheckpointService checkpointService, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:3000");
        _checkpointService = checkpointService;
        _imageService = new ImageService();
    }

    [HttpPost("create/{status}")]
    [Authorize(Policy = "authorPolicy")]
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

    [HttpPut("create-secret/{checkpointId:int}")]
    [Authorize(Policy = "authorPolicy")]
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

    [HttpPut("update-secret/{checkpointId:int}")]
    [Authorize(Policy = "authorPolicy")]
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

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "authorPolicy")]
    public async Task<ActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"/checkpoints/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "authorPolicy")]
    public ActionResult<List<CheckpointDto>> GetAllByTour([FromQuery] int page, [FromQuery] int pageSize, int id)
    {
        var result = _checkpointService.GetPagedByTour(page, pageSize, id);
        return CreateResponse(result);
    }

    [HttpGet("details/{id:int}")]
    [Authorize(Policy = "authorPolicy")]
    public ActionResult<CheckpointDto> GetById(int id)
    {
        var result = _checkpointService.Get(id);
        return CreateResponse(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "authorPolicy")]
    public ActionResult<CheckpointDto> Update([FromForm] CheckpointDto checkpoint, int id, [FromForm] List<IFormFile>? pictures = null)
    {
        if (pictures != null && pictures.Any())
        {
            var imageNames = _imageService.UploadImages(pictures);
            checkpoint.Pictures = imageNames;
        }

        checkpoint.Id = id;
        var result = _checkpointService.Update(checkpoint, User.PersonId());
        return CreateResponse(result);
    }

    [HttpGet]
    public ActionResult<PagedResult<CheckpointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _checkpointService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }
}