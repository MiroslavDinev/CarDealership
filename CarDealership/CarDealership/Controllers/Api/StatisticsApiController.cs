namespace CarDealership.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;

    using CarDealership.Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService statistics)
        {
            this.statisticsService = statistics;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {
            var totalStatistics = this.statisticsService.Total();
            return totalStatistics;
        }
    }
}
