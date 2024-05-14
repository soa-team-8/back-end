/*using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class ReportedIssueCommandTests : BaseToursIntegrationTest
    {
        public ReportedIssueCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //NECIJI TEST U CONTROLERU PRIMA VISE PARAM A U TESTU ENTITIY
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new ReportedIssueDto
            {
                Category = "problem",
                Description= "Description",
                Priority= 1,
                TourId= -1,
                TouristId= -1,
                Time= DateTime.Now.ToUniversalTime()
            };

            // Act
            //var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ReportedIssueDto;

            // Assert - Response
            //result.ShouldNotBeNull();
            //result.Id.ShouldNotBe(0);
            //result.Category.ShouldBe(newEntity.Category);

            // Assert - Database
            //var storedEntity = dbContext.ReportedIssues.FirstOrDefault(i => i.Category == newEntity.Category);
            //storedEntity.ShouldNotBeNull();
            //storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_missing_category()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ReportedIssueDto
            {
                Description = "Description",
                Priority = 1,
                TourId = -1,
                TouristId = -1,
                Time = DateTime.Now.ToUniversalTime()
            };

            // Act
            //var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            //result.StatusCode.ShouldBe(400);
        }




        private static ReportingIssueController CreateController(IServiceScope scope)
        {
            return new ReportingIssueController(scope.ServiceProvider.GetRequiredService<IReportingIssueService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
*/