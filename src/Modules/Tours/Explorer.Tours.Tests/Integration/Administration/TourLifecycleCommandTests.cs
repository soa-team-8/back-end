/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourLifecycleCommandTests : BaseToursIntegrationTest
    {
        public TourLifecycleCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Publish_succeeds()
        {
            // Arrange - Input data
            var authorId = -12;
            var tourId = -1;
            var expectedResponseCode = 200;
            var expectedStatus = TourStatus.Published;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Publish(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
        }

        [Fact]
        public void Publish_fails_invalid_transport()
        {
            // Arrange - Input data
            var authorId = -11;
            var tourId = -3;
            var expectedResponseCode = 200;
            var expectedStatus = TourStatus.Draft;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Publish(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
        }

        [Fact]
        public void Publish_fails_invalid_checkpoints()
        {
            // Arrange - Input data
            var authorId = 2;
            var tourId = -5;
            var expectedStatus = TourStatus.Draft;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Publish(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
            storedEntity.Checkpoints.Count.ShouldBeLessThan(2);
        }

        [Fact]
        public void Publish_fails_invalid_user()
        {
            // Arrange - Input data
            var authorId = 5;
            var tourId = -1;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var expectedStatus = 400;
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Publish(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedStatus);

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Archive_succeeds()
        {
            // Arrange - Input data
            var authorId = -12;
            var tourId = -4;
            var expectedResponseCode = 200;
            var expectedStatus = TourStatus.Archived;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Archive(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
        }

        [Fact]
        public void Archive_fails_invalid_checkpoints()
        {
            // Arrange - Input data
            var authorId = 2;
            var tourId = -5;
            var expectedStatus = TourStatus.Draft;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Archive(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
        }

        [Fact]
        public void Archive_fails_invalid_user()
        {
            // Arrange - Input data
            var authorId = 5;
            var tourId = -6;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var expectedStatus = 400;
            var expectedTourStatus = TourStatus.Published;
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Archive(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedStatus);

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedTourStatus);
        }

        [Fact]
        public void Add_tour_time_succeeds()
        {
            // Arrange - Input data
            var authorId = -12;
            var tourId = -2;
            TourTimesDto tourTimesDto = new TourTimesDto();
            TourTimeDto tourTimeDto = new TourTimeDto();
            tourTimeDto.Transportation = "walking";
            tourTimeDto.TimeInSeconds = 3600;
            tourTimeDto.Distance = 1000;
            tourTimesDto.TourTimes = new List<TourTimeDto>();
            tourTimesDto.TourTimes.Add(tourTimeDto);
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.AddTime(tourTimesDto, tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.TourTimes.Count.ShouldBe(1);
        }

        [Fact]
        public void Adding_transport_fails_invalid_user()
        {
            // Arrange - Input data
            var authorId = 6;
            var tourId = -1;
            TourTimesDto tourTimesDto = new TourTimesDto();
            TourTimeDto tourTimeDto = new TourTimeDto();
            tourTimeDto.Transportation = "walking";
            tourTimeDto.TimeInSeconds = 3600;
            tourTimeDto.Distance = 1000;
            tourTimesDto.TourTimes = new List<TourTimeDto>();
            tourTimesDto.TourTimes.Add(tourTimeDto);
            var expectedCode = 400;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.AddTime(tourTimesDto, tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedCode);

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.TourTimes.Count.ShouldBe(1);
        }

        [Fact]
        public void Adding_equipment_succeeds()
        {
            // Arrange - Input data
            var authorId = -12;
            var tourId = -4;
            var expectedResponseCode = 200;
            var expectedCount = 1;
            var equipmentId = -1;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.AddEquipment(tourId, equipmentId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Equipment.Count.ShouldBe(expectedCount);
        }

        [Fact]
        public void Adding_equipment_fails_invalid_user()
        {
            // Arrange - Input data
            var authorId = 5;
            var tourId = -6;
            var expectedResponseCode = 400;
            var expectedCount = 0;
            var equipmentId = -1;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.AddEquipment(tourId, equipmentId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Equipment.Count.ShouldBe(0);
        }

        private static TourController CreateController(IServiceScope scope, string personId)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext(personId)
            };
        }
    }
}*/
