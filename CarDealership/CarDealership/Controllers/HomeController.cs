namespace CarDealership.Controllers
{
    using System.Linq;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using CarDealership.Data;
    using CarDealership.Models;
    using CarDealership.Models.Home;
    public class HomeController : Controller
    {
        private readonly CarDealershipDbContext data;

        public HomeController(CarDealershipDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

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

            return this.View(new IndexViewModel 
            {
                Cars = cars,
                TotalCars = totalCars,
                TotalUsers = totalUsers
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
