using CarDealership.Data;
using CarDealership.Data.Models;
using CarDealership.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealership.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarDealershipDbContext data;

        public CarsController(CarDealershipDbContext data)
        {
            this.data = data;
        }
        public IActionResult Add()
        {
            return this.View(new CarAddFormModel
            {
                Categories = this.GetCarCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(CarAddFormModel car)
        {
            if(!this.data.Categories.Any(x=> x.Id==car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.GetCarCategories();

                return this.View(car);
            }

            var carData = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
            
        }

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
        {
            return this.data.Categories
                .Select(c => new CarCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
        }
    }
}
