using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Tourist.Encounters
{
    public class TouristEncounterProtoController : TouristEncounterService.TouristEncounterServiceBase
    {
        private readonly ILogger<TouristEncounterProtoController> _logger;

        public TouristEncounterProtoController(ILogger<TouristEncounterProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<TouristCreateEncounterResponse> TouristCreateEncounter(TouristCreateEncounterRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new TouristEncounterService.TouristEncounterServiceClient(channel);
            var response = await client.TouristCreateEncounterAsync(request);

            return await Task.FromResult(new TouristCreateEncounterResponse
            {
                Encounter = response.Encounter
            });
        }

        public override async Task<TouristGetAllEncountersResponse> TouristGetAllEncounters(TouristGetAllEncountersRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new TouristEncounterService.TouristEncounterServiceClient(channel);
            var response = await client.TouristGetAllEncountersAsync(request);

            return await Task.FromResult(new TouristGetAllEncountersResponse
            {
                Encounters = { response.Encounters }
            });
        }


    }
}
