using CarDealership.Data;
using CarDealership.Models;
using CarDealership.Models.Cars;
using CarDealership.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealership.Controllers
{
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
                TotalCars = totalCars
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
