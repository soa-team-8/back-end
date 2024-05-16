using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.Author.Administration
{
    public class TourProtoController : ToursService.ToursServiceBase
    {
        private readonly ILogger<TourProtoController> _logger;

        public TourProtoController(ILogger<TourProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<ActionResponse> CreateTour(TourDto message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.CreateTourAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new ActionResponse
            {
                Succes = response.Succes
            };
        }
        
        public override async Task<ListTourDtoResponse> GetAllTours(GetAllToursRequest message, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.GetAllToursAsync(message);

            // Console.WriteLine(response.AccessToken);

            var list = new GrpcServiceTranscoding.ListTourDtoResponse();

            // Dodajte sve ture iz odgovora u listu tura ListTourDtoResponse
            foreach (var tDto in response.Tours)
            {
                list.Tours.Add(tDto);
            }

            return list;
        }

        public override async Task<ActionResponse> AddEquipment(AddEquipmentRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.AddEquipmentAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new ActionResponse
            {
                Succes = response.Succes
            };
        }

        public override async Task<ActionResponse> RemoveEquipment(RemoveEquipmentRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.RemoveEquipmentAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new ActionResponse
            {
                Succes = response.Succes
            };
        }

        public override async Task<TourDto> GetTourDetails(GetTourDetailsRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.GetTourDetailsAsync(message);

            // Console.WriteLine(response.AccessToken);

            var tourDto = new TourDto
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                DemandingnessLevel = response.DemandingnessLevel,
                Price = response.Price,
                Tags = { response.Tags }, // Pretpostavljajući da je Tags IEnumerable<string>
                AuthorId = response.AuthorId,
                Status = response.Status,
                Closed = response.Closed
            };

            // Mapirajte odgovarajuće podatke za EquipmentDto iz odgovora
            foreach (var equipment in response.Equipment)
            {
                tourDto.Equipment.Add(new EquipmentDto
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    Description = equipment.Description
                });
            }

            // Mapirajte odgovarajuće podatke za CheckpointDto iz odgovora
            foreach (var checkpoint in response.Checkpoints)
            {
                tourDto.Checkpoints.Add(new CheckpointDto
                {
                    Id = checkpoint.Id,
                    TourId = checkpoint.TourId,
                    AuthorId = checkpoint.AuthorId,
                    Longitude = checkpoint.Longitude,
                    Latitude = checkpoint.Latitude,
                    Name = checkpoint.Name,
                    Description = checkpoint.Description,
                    Pictures = { checkpoint.Pictures }, // Pretpostavljajući da je Pictures IEnumerable<string>
                    RequiredTimeInSeconds = checkpoint.RequiredTimeInSeconds,
                    // Mapiranje za CheckpointSecretDto zavisi od strukture odgovora, možda ćete morati dodatno mapirati
                    EncounterId = checkpoint.EncounterId,
                    IsSecretPrerequisite = checkpoint.IsSecretPrerequisite
                });
            }

            // Mapirajte odgovarajuće podatke za PublishedTourDto iz odgovora
            foreach (var publishedTour in response.PublishedTours)
            {
                tourDto.PublishedTours.Add(new PublishedTourDto
                {
                    PublishingDate = publishedTour.PublishingDate
                });
            }

            // Mapirajte odgovarajuće podatke za ArchivedTourDto iz odgovora
            foreach (var archivedTour in response.ArchivedTours)
            {
                tourDto.ArchivedTours.Add(new ArchivedTourDto
                {
                    ArchivingDate = archivedTour.ArchivingDate
                });
            }

            // Mapirajte odgovarajuće podatke za TourTimeDto iz odgovora
            foreach (var tourTime in response.TourTimes)
            {
                tourDto.TourTimes.Add(new TourTimeDto
                {
                    TimeInSeconds = tourTime.TimeInSeconds,
                    Distance = tourTime.Distance,
                    Transportation = tourTime.Transportation
                });
            }

            // Mapirajte odgovarajuće podatke za TourRatingDto iz odgovora
            foreach (var tourRating in response.TourRatings)
            {
                tourDto.TourRatings.Add(new TourRatingDto
                {
                    Id = tourRating.Id,
                    Rating = tourRating.Rating,
                    Comment = tourRating.Comment,
                    TouristId = tourRating.TouristId,
                    TourId = tourRating.TourId,
                    TourDate = tourRating.TourDate,
                    CreationDate = tourRating.CreationDate,
                    // Pretpostavljajući da je ImageNames IEnumerable<string>
                    ImageNames = { tourRating.ImageNames }
                });
            }

            return tourDto;
        }

        public override async Task<ActionResponse> DeleteTour(DeleteTourRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.DeleteTourAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new ActionResponse
            {
                Succes = response.Succes
            };
        }

        public override async Task<ActionResponse> PublishTour(PublishTourRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.PublishTourAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new ActionResponse
            {
                Succes = response.Succes
            };
        }

        public override async Task<ActionResponse> ArchiveTour(ArchiveTourRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.ArchiveTourAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new ActionResponse
            {
                Succes = response.Succes
            };
        }

        public override async Task<ActionResponse> UpdateTour(UpdateTourRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.UpdateTourAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new ActionResponse
            {
                Succes = response.Succes
            };
        }

        public override async Task<ListTourDtoResponse> GetToursByAuthor(GetToursByAuthorRequest message,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new ToursService.ToursServiceClient(channel);
            var response = await client.GetToursByAuthorAsync(message);

            // Console.WriteLine(response.AccessToken);
            var listTourDtoResponse = new ListTourDtoResponse();

            // Dodajte sve ture iz odgovora u listu tura ListTourDtoResponse
            foreach (var tourDto in response.Tours)
            {
                listTourDtoResponse.Tours.Add(tourDto);
            }

            return listTourDtoResponse;

        }
    }
}
