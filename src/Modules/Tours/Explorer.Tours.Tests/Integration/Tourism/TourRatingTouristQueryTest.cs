/*using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Tourism;
[Collection("Sequential")]

public class TourRatingTouristQueryTest : BaseToursIntegrationTest
{
    public TourRatingTouristQueryTest(ToursTestFactory factory) : base(factory) { }
    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourRatingDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(1);
        result.TotalCount.ShouldBe(1);
    }
    private static TourRatingTouristController CreateController(IServiceScope scope)
    {
        return new TourRatingTouristController(scope.ServiceProvider.GetRequiredService<ITourRatingService>(),
            scope.ServiceProvider.GetRequiredService<IItemOwnershipService>(),
            scope.ServiceProvider.GetRequiredService<ITourExecutionRepository>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}*/