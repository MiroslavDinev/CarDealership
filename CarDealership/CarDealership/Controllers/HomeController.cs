namespace CarDealership.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using CarDealership.Models.Home;
    using CarDealership.Services.Statistics;
    using CarDealership.Services.Cars;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ICarService carService;

        public HomeController(IStatisticsService statistics, ICarService carService)
        {
            this.statistics = statistics;
            this.carService = carService;
        }

        public IActionResult Index()
        {
            var cars = this.carService.Latest();

            var totalStatistics = this.statistics.Total();

            return this.View(new IndexViewModel 
            {
                Cars = cars.ToList(),
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
