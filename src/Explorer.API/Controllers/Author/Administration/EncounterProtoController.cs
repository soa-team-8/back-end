using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;


namespace Explorer.API.Controllers.Author.Administration
{
    public class EncounterProtoController : EncounterService.EncounterServiceBase
    {
        private readonly ILogger<EncounterProtoController> _logger;

        public EncounterProtoController(ILogger<EncounterProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<EncounterResponse> CreateEncounter(CreateEncounterRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EncounterService.EncounterServiceClient(channel);
            var response = await client.CreateEncounterAsync(request);
            
            return await Task.FromResult(new EncounterResponse
            {
                Encounter = response.Encounter
            });
        }

        public override async Task<EncounterResponse> UpdateEncounter(UpdateEncounterRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EncounterService.EncounterServiceClient(channel);
            var response = await client.UpdateEncounterAsync(request);

            return await Task.FromResult(new EncounterResponse
            {
                Encounter = response.Encounter
            });
        }

        public override async Task<DeleteEncounterResponse> DeleteEncounter(DeleteEncounterRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EncounterService.EncounterServiceClient(channel);
            var response = await client.DeleteEncounterAsync(request);

            return new DeleteEncounterResponse
            {
                Success = response.Success
            };
        }

        public override async Task<EncounterResponse> GetEncounter(GetEncounterRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EncounterService.EncounterServiceClient(channel);
            var response = await client.GetEncounterAsync(request);

            return new EncounterResponse
            {
                Encounter = response.Encounter
            };
        }
    }
}
