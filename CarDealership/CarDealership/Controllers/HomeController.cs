namespace CarDealership.Controllers
{
    using System.Linq;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using CarDealership.Data;
    using CarDealership.Models;
    using CarDealership.Models.Home;
    using CarDealership.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly CarDealershipDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(CarDealershipDbContext data, IStatisticsService statistics)
        {
            this.data = data;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var cars = this.data.Cars
                .OrderByDescending(x=> x.Id)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    Year = x.Year
                }).Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return this.View(new IndexViewModel 
            {
                Cars = cars,
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
