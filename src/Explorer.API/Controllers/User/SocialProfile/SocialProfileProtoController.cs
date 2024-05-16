

using Explorer.API.Controllers.Tourist.Encounters;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.User.SocialProfile
{
    public class SocialProfileProtoController : SocialProfileService.SocialProfileServiceBase
    {
        private readonly ILogger<SocialProfileProtoController> _logger;

        public SocialProfileProtoController(ILogger<SocialProfileProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<FollowResponse> Follow(FollowRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:9090", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new SocialProfileService.SocialProfileServiceClient(channel);
            var response = await client.FollowAsync(request);

            return await Task.FromResult(new FollowResponse
            {
                SocialProfile = response.SocialProfile
            });
        }


        public override async Task<UnFollowResponse> UnFollow(UnFollowRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:9090", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new SocialProfileService.SocialProfileServiceClient(channel);
            var response = await client.UnFollowAsync(request);

            return await Task.FromResult(new UnFollowResponse
            {
                SocialProfile = response.SocialProfile
            });
        }

        public override async Task<GetSocialProfileResponse> GetSocialProfile(GetSocialProfileRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:9090", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new SocialProfileService.SocialProfileServiceClient(channel);
            var response = await client.GetSocialProfileAsync(request);

            return await Task.FromResult(new GetSocialProfileResponse
            {
                SocialProfile = response.SocialProfile
            });
        }

        public override async Task<GetRecommendationsResponse> GetRecommendations(GetRecommendationsRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:9090", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new SocialProfileService.SocialProfileServiceClient(channel);
            var response = await client.GetRecommendationsAsync(request);

            return await Task.FromResult(new GetRecommendationsResponse
            {
                Recommendations = { response.Recommendations }
            });
        }
    }
}
