

using Explorer.API.Controllers.Author.Administration;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Tourist.Encounters
{
    public class EncounterRequestProtoController : EncounterRequestService.EncounterRequestServiceBase
    {
        private readonly ILogger<EncounterRequestProtoController> _logger;

        public EncounterRequestProtoController(ILogger<EncounterRequestProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<EncounterRequestResponseDto> CreateEncounterRequest(CreateEncounterRequestDto request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EncounterRequestService.EncounterRequestServiceClient(channel);
            var response = await client.CreateEncounterRequestAsync(request);

            return await Task.FromResult(new EncounterRequestResponseDto
            {
                EncounterRequest = response.EncounterRequest
            });
        }

        
        public override async Task<GetAllEncounterRequestsResponse> GetAllEncounterRequests(GetAllEncounterRequestsRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EncounterRequestService.EncounterRequestServiceClient(channel);
            var response = await client.GetAllEncounterRequestsAsync(request);

            return await Task.FromResult(new GetAllEncounterRequestsResponse
            {
                EncounterRequests = { response.EncounterRequests }
            });
        }

        public override async Task<EncounterRequestResponseDto> AcceptEncounterRequest(AcceptEncounterRequestDto request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EncounterRequestService.EncounterRequestServiceClient(channel);
            var response = await client.AcceptEncounterRequestAsync(request);

            return await Task.FromResult(new EncounterRequestResponseDto
            {
                EncounterRequest = response.EncounterRequest
            });
        }

        public override async Task<EncounterRequestResponseDto> RejectEncounterRequest(RejectEncounterRequestDto request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3030", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EncounterRequestService.EncounterRequestServiceClient(channel);
            var response = await client.RejectEncounterRequestAsync(request);

            return await Task.FromResult(new EncounterRequestResponseDto
            {
                EncounterRequest = response.EncounterRequest
            });
        }
    }
}
