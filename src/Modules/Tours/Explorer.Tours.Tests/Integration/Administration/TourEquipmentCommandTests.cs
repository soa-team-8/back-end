/*using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourEquipmentCommandTests : BaseToursIntegrationTest
    {
        public TourEquipmentCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Add_tour_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var tourId = -2;
            var equipmentId = -1;
            var authorId = -12;

            // Act
            var result = ((ObjectResult)controller.AddEquipment(tourId, equipmentId).Result)?.Value as TourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(tourId);

            // Assert - Database
            var addedRelationship =
                dbContext.TourEquipment.FirstOrDefault(te => te.TourId == tourId && te.EquipmentId == equipmentId);
            addedRelationship.ShouldNotBeNull();
            addedRelationship.TourId.ShouldBe(tourId);
            addedRelationship.EquipmentId.ShouldBe(equipmentId);
        }

        [Fact]
        public void Remove_tour_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var tourId = -2;
            var equipmentId = -2;

            // Act
            var result = ((ObjectResult)controller.RemoveEquipment(tourId, equipmentId).Result)?.Value as TourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(tourId);

            // Assert - Database
            var addedRelationship =
                dbContext.TourEquipment.FirstOrDefault(te => te.TourId == tourId && te.EquipmentId == equipmentId);
            addedRelationship.ShouldBeNull();
        }

        [Fact]
        public void Add_not_exists_tour()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -10;
            var equipmentId = -2;

            // Act
            var response = (ObjectResult)controller.AddEquipment(tourId, equipmentId).Result;

            // Assert - Response
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(404);

        }

        [Fact]
        public void Remove_not_exists_tour()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -10;
            var equipmentId = -1;

            // Act
            var response = (ObjectResult)controller.RemoveEquipment(tourId, equipmentId).Result;

            // Assert - Response
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(404);

        }

        [Fact]
        public void Add_not_exists_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -1;
            var equipmentId = -21;

            // Act
            var response = (ObjectResult)controller.AddEquipment(tourId, equipmentId).Result;

            // Assert - Response
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(404);

        }

        [Fact]
        public void Remove_not_exists_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -1;
            var equipmentId = -10;

            // Act
            var response = (ObjectResult)controller.RemoveEquipment(tourId, equipmentId).Result;

            // Assert - Response
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(404);

        }

        [Fact]
        public void Tour_already_have_same_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -1;
            var equipmentId = -1;

            // Act
            var response = (ObjectResult)controller.AddEquipment(tourId, equipmentId).Result;

            // Assert - Response
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(400);

        }


        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-12")
            };
        }
    }
}*/