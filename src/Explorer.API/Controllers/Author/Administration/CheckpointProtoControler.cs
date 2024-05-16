using Explorer.API.Controllers.Administrator.Administration;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using GrpcServiceTranscodingCheckpoint;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    public class CheckpointProtoControler : CheckpointsService.CheckpointsServiceBase
    {
        private readonly ILogger<CheckpointProtoControler> _logger;

        public CheckpointProtoControler(ILogger<CheckpointProtoControler> logger)
        {
            _logger = logger;
        }

        public override async Task<GrpcServiceTranscodingCheckpoint.CheckpointDto2> CreateCheckpoint(CreateCheckpointRequest message,
           ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new CheckpointsService.CheckpointsServiceClient(channel);
            var response = await client.CreateCheckpointAsync(message);

            // Console.WriteLine(response.AccessToken);

            var checkpointDto = new GrpcServiceTranscodingCheckpoint.CheckpointDto2
            {
                Id = response.Id,
                TourId = response.TourId,
                AuthorId = response.AuthorId,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                Name = response.Name,
                Description = response.Description,
                Pictures = { response.Pictures }, // Ako je response.Pictures IEnumerable<string>
                RequiredTimeInSeconds = response.RequiredTimeInSeconds,
                EncounterId = response.EncounterId,
                IsSecretPrerequisite = response.IsSecretPrerequisite
            };

            if (response.CheckpointSecret != null)
            {
                checkpointDto.CheckpointSecret = new GrpcServiceTranscodingCheckpoint.CheckpointSecretDto2
                {
                    // Popunite polja CheckpointSecretDto objekta
                    Description = response.CheckpointSecret.Description,
                    Pictures = { response.CheckpointSecret.Pictures } // Ako je response.CheckpointSecret.Pictures IEnumerable<string>
                };
            }

            return checkpointDto;
        }

        public override async Task<GrpcServiceTranscodingCheckpoint.CheckpointDto2> CreateCheckpointSecret(CreateCheckpointSecretRequest message,
           ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new CheckpointsService.CheckpointsServiceClient(channel);
            var response = await client.CreateCheckpointSecretAsync(message);

            // Console.WriteLine(response.AccessToken);

            var checkpointDto = new GrpcServiceTranscodingCheckpoint.CheckpointDto2
            {
                Id = response.Id,
                TourId = response.TourId,
                AuthorId = response.AuthorId,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                Name = response.Name,
                Description = response.Description,
                Pictures = { response.Pictures }, // Ako je response.Pictures IEnumerable<string>
                RequiredTimeInSeconds = response.RequiredTimeInSeconds,
                EncounterId = response.EncounterId,
                IsSecretPrerequisite = response.IsSecretPrerequisite
            };

            if (response.CheckpointSecret != null)
            {
                checkpointDto.CheckpointSecret = new GrpcServiceTranscodingCheckpoint.CheckpointSecretDto2
                {
                    // Popunite polja CheckpointSecretDto objekta
                    Description = response.CheckpointSecret.Description,
                    Pictures = { response.CheckpointSecret.Pictures } // Ako je response.CheckpointSecret.Pictures IEnumerable<string>
                };
            }

            return checkpointDto;
        }

        public override async Task<GrpcServiceTranscodingCheckpoint.CheckpointDto2> UpdateCheckpointSecret(UpdateCheckpointSecretRequest message,
           ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new CheckpointsService.CheckpointsServiceClient(channel);
            var response = await client.UpdateCheckpointSecretAsync(message);

            // Console.WriteLine(response.AccessToken);

            var checkpointDto = new GrpcServiceTranscodingCheckpoint.CheckpointDto2
            {
                Id = response.Id,
                TourId = response.TourId,
                AuthorId = response.AuthorId,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                Name = response.Name,
                Description = response.Description,
                Pictures = { response.Pictures }, // Ako je response.Pictures IEnumerable<string>
                RequiredTimeInSeconds = response.RequiredTimeInSeconds,
                EncounterId = response.EncounterId,
                IsSecretPrerequisite = response.IsSecretPrerequisite
            };

            if (response.CheckpointSecret != null)
            {
                checkpointDto.CheckpointSecret = new GrpcServiceTranscodingCheckpoint.CheckpointSecretDto2
                {
                    // Popunite polja CheckpointSecretDto objekta
                    Description = response.CheckpointSecret.Description,
                    Pictures = { response.CheckpointSecret.Pictures } // Ako je response.CheckpointSecret.Pictures IEnumerable<string>
                };
            }

            return checkpointDto;
        }

        public override async Task<GrpcServiceTranscodingCheckpoint.ActionResponse2> DeleteCheckpoint(DeleteCheckpointRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new CheckpointsService.CheckpointsServiceClient(channel);
            var response = await client.DeleteCheckpointAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new GrpcServiceTranscodingCheckpoint.ActionResponse2
            {
                Succes = response.Succes
            };
        }

        public override async Task<GrpcServiceTranscodingCheckpoint.ListCheckpointDtoResponse> GetAllByTour(GetCheckpointsByTourRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new CheckpointsService.CheckpointsServiceClient(channel);
            var response = await client.GetAllByTourAsync(message);

            // Console.WriteLine(response.AccessToken);

            var listCheckpointDtoResponse = new GrpcServiceTranscodingCheckpoint.ListCheckpointDtoResponse();

            // Dodajte sve ture iz odgovora u listu tura ListTourDtoResponse
            foreach (var chDto in response.Checkpoints)
            {
                listCheckpointDtoResponse.Checkpoints.Add(chDto);
            }

            return listCheckpointDtoResponse;
        }

        public override async Task<GrpcServiceTranscodingCheckpoint.CheckpointDto2> GetById(GetCheckpointDetailsRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new CheckpointsService.CheckpointsServiceClient(channel);
            var response = await client.GetByIdAsync(message);

            // Console.WriteLine(response.AccessToken);

            var checkpointDto = new GrpcServiceTranscodingCheckpoint.CheckpointDto2
            {
                Id = response.Id,
                TourId = response.TourId,
                AuthorId = response.AuthorId,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                Name = response.Name,
                Description = response.Description,
                Pictures = { response.Pictures }, // Ako je response.Pictures IEnumerable<string>
                RequiredTimeInSeconds = response.RequiredTimeInSeconds,
                EncounterId = response.EncounterId,
                IsSecretPrerequisite = response.IsSecretPrerequisite
            };

            if (response.CheckpointSecret != null)
            {
                checkpointDto.CheckpointSecret = new GrpcServiceTranscodingCheckpoint.CheckpointSecretDto2
                {
                    // Popunite polja CheckpointSecretDto objekta
                    Description = response.CheckpointSecret.Description,
                    Pictures = { response.CheckpointSecret.Pictures } // Ako je response.CheckpointSecret.Pictures IEnumerable<string>
                };
            }

            return checkpointDto;
        }

        public override async Task<GrpcServiceTranscodingCheckpoint.CheckpointDto2> UpdateCheckpoint(UpdateCheckpointRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new CheckpointsService.CheckpointsServiceClient(channel);
            var response = await client.UpdateCheckpointAsync(message);

            // Console.WriteLine(response.AccessToken);

            var checkpointDto = new GrpcServiceTranscodingCheckpoint.CheckpointDto2
            {
                Id = response.Id,
                TourId = response.TourId,
                AuthorId = response.AuthorId,
                Longitude = response.Longitude,
                Latitude = response.Latitude,
                Name = response.Name,
                Description = response.Description,
                Pictures = { response.Pictures }, // Ako je response.Pictures IEnumerable<string>
                RequiredTimeInSeconds = response.RequiredTimeInSeconds,
                EncounterId = response.EncounterId,
                IsSecretPrerequisite = response.IsSecretPrerequisite
            };

            if (response.CheckpointSecret != null)
            {
                checkpointDto.CheckpointSecret = new GrpcServiceTranscodingCheckpoint.CheckpointSecretDto2
                {
                    // Popunite polja CheckpointSecretDto objekta
                    Description = response.CheckpointSecret.Description,
                    Pictures = { response.CheckpointSecret.Pictures } // Ako je response.CheckpointSecret.Pictures IEnumerable<string>
                };
            }

            return checkpointDto;
        }

        public override async Task<GrpcServiceTranscodingCheckpoint.ListCheckpointDtoResponse> GetAllPagedCheckpoints(GetAllPagedCheckpointsRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new CheckpointsService.CheckpointsServiceClient(channel);
            var response = await client.GetAllPagedCheckpointsAsync(message);

            // Console.WriteLine(response.AccessToken);

            var listCheckpointDtoResponse = new GrpcServiceTranscodingCheckpoint.ListCheckpointDtoResponse();

            // Dodajte sve ture iz odgovora u listu tura ListTourDtoResponse
            foreach (var chDto in response.Checkpoints)
            {
                listCheckpointDtoResponse.Checkpoints.Add(chDto);
            }

            return listCheckpointDtoResponse;
        }
    }
}
