using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Administrator.Administration
{
    public class ReportedIssuesReviewProtoController : ReportedIssuesReviewServices.ReportedIssuesReviewServicesBase
    {
        private readonly ILogger<ReportedIssuesReviewProtoController> _logger;

        public ReportedIssuesReviewProtoController(ILogger<ReportedIssuesReviewProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<PagedResultReportedIssueDto> GetAll(GetAllReportedIssuesRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ReportedIssuesReviewServices.ReportedIssuesReviewServicesClient(channel);
            var response = await client.GetAllAsync(message);

            // Console.WriteLine(response.AccessToken);

            var pagedResult = new PagedResultReportedIssueDto();
            pagedResult.Items.AddRange(response.Items); // Dodavanje svih prijavljenih problema iz odgovora

            return pagedResult;
        }
        /*
        public override async Task<GrpcServiceTranscoding.ReportedIssueDto> Respond(RespondToReportedIssueRequest message,
           ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ReportedIssuesReviewServices.ReportedIssuesReviewServicesClient(channel);
            var response = await client.RespondAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new 
        }*/
    }
}
