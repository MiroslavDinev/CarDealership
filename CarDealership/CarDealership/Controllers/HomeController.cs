namespace CarDealership.Controllers
{
    using System.Linq;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;

    using CarDealership.Data;
    using CarDealership.Models;
    using CarDealership.Models.Home;
    using CarDealership.Services.Statistics;
    using AutoMapper.QueryableExtensions;

    public class HomeController : Controller
    {
        private readonly CarDealershipDbContext data;
        private readonly IStatisticsService statistics;
        private readonly IMapper mapper;

        public HomeController(CarDealershipDbContext data, IStatisticsService statistics, IMapper mapper)
        {
            this.data = data;
            this.statistics = statistics;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var cars = this.data.Cars
                .OrderByDescending(x=> x.Id)
                .ProjectTo<CarIndexViewModel>(this.mapper.ConfigurationProvider)
                .Take(3)
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
