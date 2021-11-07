namespace CarDealership.Tests.Controllers.Api
{
    using Xunit;

    using CarDealership.Controllers.Api;
    using CarDealership.Tests.Mocks;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsService = StatisticsServiceMock.Instance;
            var statisticsController = new StatisticsApiController(statisticsService);

            //Act
            var result = statisticsController.GetStatistics();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalCars);
            Assert.Equal(10, result.TotalRents);
            Assert.Equal(15, result.TotalUsers);
        }
    }
}
