/*using Explorer.Tours.API.Dtos;
using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.API.Controllers.Tourist.Tour;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public.Recommendation;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Tours.Tests.Integration.Tourism;

[Collection("Sequential")]

public class TourExecutionQueryTests : BaseToursIntegrationTest
{
    public TourExecutionQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_by_tour_and_user()
    {
        var userId = 3;
        var tourId = -1;
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope, userId.ToString());

        // Act
        var result = ((ObjectResult)controller.Get(tourId).Result)?.Value as TourExecutionDto;

        // Assert
        result.ShouldNotBeNull();
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