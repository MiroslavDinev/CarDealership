namespace CarDealership.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;

    using CarDealership.Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {
            var totalStatistics = this.statistics.Total();

            return new StatisticsServiceModel
            {
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers,
                TotalRents = 0
            };
        }
    }
}
