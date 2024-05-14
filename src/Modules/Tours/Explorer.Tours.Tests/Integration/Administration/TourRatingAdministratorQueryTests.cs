﻿/*using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;
[Collection("Sequential")]

public class TourRatingAdministratorQueryTests : BaseToursIntegrationTest
{
    public TourRatingAdministratorQueryTests(ToursTestFactory factory) : base(factory) { }

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

    private static TourRatingAdministratorController CreateController(IServiceScope scope)
    {
        return new TourRatingAdministratorController(scope.ServiceProvider.GetRequiredService<ITourRatingService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}*/