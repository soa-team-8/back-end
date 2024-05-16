using Explorer.API.Controllers.Author.Administration;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscodingCheckpoint;
using GrpcServiceTranscodingEquipment;

namespace Explorer.API.Controllers.Administrator.Administration
{
    public class EquipmentProtoController : EquipmentsService.EquipmentsServiceBase
    {
        private readonly ILogger<EquipmentProtoController> _logger;

        public EquipmentProtoController(ILogger<EquipmentProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<GrpcServiceTranscodingEquipment.PagedEquipmentDto> GetAllPagedEquipments(GetAllPagedEquipmentRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EquipmentsService.EquipmentsServiceClient(channel);
            var response = await client.GetAllPagedEquipmentsAsync(message);

            // Console.WriteLine(response.AccessToken);

            var paged = new GrpcServiceTranscodingEquipment.PagedEquipmentDto();

            // Dodajte sve ture iz odgovora u listu tura ListTourDtoResponse
            foreach (var eq in response.Items)
            {
                paged.Items.Add(eq);
            }

            return paged;
        }

        public override async Task<GrpcServiceTranscodingEquipment.EquipmentListDto> GetAllEquipment(GetAllEquipmentRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EquipmentsService.EquipmentsServiceClient(channel);
            var response = await client.GetAllEquipmentAsync(message);

            // Console.WriteLine(response.AccessToken);

            var list = new GrpcServiceTranscodingEquipment.EquipmentListDto();

            // Dodajte sve ture iz odgovora u listu tura ListTourDtoResponse
            foreach (var eq in response.Items)
            {
                list.Items.Add(eq);
            }

            return list;
        }

        public override async Task<GrpcServiceTranscodingEquipment.EquipmentDto3> CreateEquipment(CreateEquipmentRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EquipmentsService.EquipmentsServiceClient(channel);
            var response = await client.CreateEquipmentAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new GrpcServiceTranscodingEquipment.EquipmentDto3
            {
                Id = response.Id,
                Description = response.Description,
                Name = response.Name,
            };
        }

        public override async Task<GrpcServiceTranscodingEquipment.EquipmentDto3> UpdateEquipment(UpdateEquipmentRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EquipmentsService.EquipmentsServiceClient(channel);
            var response = await client.UpdateEquipmentAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new GrpcServiceTranscodingEquipment.EquipmentDto3
            {
                Id = response.Id,
                Description = response.Description,
                Name = response.Name,
            };
        }

        public override async Task<GrpcServiceTranscodingEquipment.ActionResponse3> DeleteEquipment(DeleteEquipmentRequest message,
          ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new EquipmentsService.EquipmentsServiceClient(channel);
            var response = await client.DeleteEquipmentAsync(message);

            // Console.WriteLine(response.AccessToken);

            return new GrpcServiceTranscodingEquipment.ActionResponse3
            {
                Succes = response.Succes,
            };
        }
    }
}
