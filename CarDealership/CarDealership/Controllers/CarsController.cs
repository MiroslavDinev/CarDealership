namespace CarDealership.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using CarDealership.Data;
    using CarDealership.Data.Models;
    using CarDealership.Models.Cars;
    using CarDealership.Infrastructure;
    using CarDealership.Services.Cars;

    public class CarsController : Controller
    {
        private readonly CarDealershipDbContext data;
        private readonly ICarService carService;

        public CarsController(CarDealershipDbContext data, ICarService carService)
        {
            this.data = data;
            this.carService = carService;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.IsUserDealer())
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return this.View(new CarAddFormModel
            {
                Categories = this.GetCarCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarAddFormModel car)
        {
            var dealerId = this.data.Dealers
                .Where(x => x.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!this.data.Categories.Any(x=> x.Id==car.CategoryId))
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
                CategoryId = car.CategoryId,
                DealerId = dealerId
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
            
        }

        public IActionResult All([FromQuery]AllCarsQueryModel query)
        {
            var queryResult = this.carService.All(query.Brand, query.SearchTerm, query.Sorting, query.CurrentPage, AllCarsQueryModel.CarsPerPage);

            var carBrands = this.carService.AllCarBrands();

            query.Brands = carBrands;
            query.Cars = queryResult.Cars;
            query.TotalCars = queryResult.TotalCars;

            return this.View(query);
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

        private bool IsUserDealer()
        {
            return this.data.Dealers
                .Any(x => x.UserId == this.User.GetId());

        }
    }
}
