/*using Explorer.Tours.API.Dtos;
using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Tours.API.Public.Administration;
using Explorer.API.Controllers.Tourist.Tour;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.API.Public.Recommendation;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Tours.Tests.Integration.Tourism;

[Collection("Sequential")]

public class TourExecutionCommandTests : BaseToursIntegrationTest
{
    public TourExecutionCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Create_succeeds()
    {
        // Arrange - Input data
        var touristId = -21;
        var tourId = -4;
        var expectedResponseCode = 200;
        var expectedStatus = ExecutionStatus.InProgress;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope, touristId.ToString());
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.Create(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
        // Assert - Database
        var storedEntity = dbContext.TourExecution.FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);
        storedEntity.ShouldNotBeNull();
    }
    [Fact]
    public void Abandon_succeeds()
    {
        // Arrange - Input data
        var authorId = "-21";
        var tourId = -4;
        var expectedResponseCode = 200;
        var expectedStatus = ExecutionStatus.Abandoned;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope, authorId);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.Abandon(-5).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
        // Assert - Database
        var storedEntity = dbContext.TourExecution.FirstOrDefault(t => t.Id == -5);
        storedEntity.ShouldNotBeNull();
        storedEntity.ExecutionStatus.ShouldBe(expectedStatus);
    }

    

    [Fact]
    public void RegisterActivity_succeeds()
    {
        // Arrange - Input data
        var touristId = -21;
        var tourExecutionId = -2;
        var expectedResponseCode = 200;
        var expectedStatus = ExecutionStatus.Completed;
        TouristPositionDto positionDto = new TouristPositionDto();
        positionDto.Longitude = 45;
        positionDto.Latitude = 45;
        positionDto.Id = -1;
        positionDto.CreatorId = -21;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope, touristId.ToString());
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.CheckPosition(positionDto, tourExecutionId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
        // Assert - Database
        var storedEntity = dbContext.TourExecution.FirstOrDefault(t => t.Id == tourExecutionId);
        storedEntity.ShouldNotBeNull();
        storedEntity.ExecutionStatus.ShouldBe(expectedStatus);
    }

    [Fact]
    public void Complete_succeeds()
    {
        // Arrange - Input data
        var touristId =1;
        var tourExecutionId = -1;
        var expectedResponseCode = 200;
        var expectedStatus = ExecutionStatus.Completed;
        TouristPositionDto positionDto = new TouristPositionDto();
        positionDto.Longitude = 45;
        positionDto.Latitude = 45;
        positionDto.Id = -1;
        positionDto.CreatorId = 1;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope, touristId.ToString());
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.CheckPosition(positionDto, tourExecutionId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
        // Assert - Database
        var storedEntity = dbContext.TourExecution.FirstOrDefault(t => t.Id == tourExecutionId);
        storedEntity.ShouldNotBeNull();
        storedEntity.ExecutionStatus.ShouldBe(expectedStatus);
        storedEntity.CompletedCheckpoints.Count.ShouldBe(3);
    }

    private static TourExecutionController CreateController(IServiceScope scope, string personId)
    {
		var tourExecutionService = scope.ServiceProvider.GetRequiredService<ITourExecutionService>();
		var tourRecommendationService = scope.ServiceProvider.GetRequiredService<ITourRecommendationService>();
		var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

		return new TourExecutionController(tourExecutionService, tourRecommendationService, emailService)
		{
			ControllerContext = BuildContext(personId)
		};
	}
}*/