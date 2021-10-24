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

    public class CarsController : Controller
    {
        private readonly CarDealershipDbContext data;

        public CarsController(CarDealershipDbContext data)
        {
            this.data = data;
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
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(x => x.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                carsQuery = carsQuery.Where(c => (c.Brand + " " + c.Model).ToLower().Contains(query.SearchTerm.ToLower())
                || c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            carsQuery = query.Sorting switch
            {
                CarSorting.DateCreated => carsQuery.OrderByDescending(x => x.Id),
                CarSorting.Year => carsQuery.OrderByDescending(x => x.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(x => x.Brand).ThenBy(x => x.Model),
                _=> carsQuery.OrderByDescending(x => x.Id)
            };

            var totalCars = carsQuery.Count();

            var allCars = carsQuery
                .Skip((query.CurrentPage-1) * AllCarsQueryModel.CarsPerPage)
                .Take(AllCarsQueryModel.CarsPerPage)
                .Select(x => new CarListingViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Year = x.Year,
                    Category = x.Category.Name
                }).ToList();

            var carBrands = this.data.Cars
                .Select(x => x.Brand)
                .Distinct()
                .ToList();

            query.Brands = carBrands;
            query.Cars = allCars;
            query.TotalCars = totalCars;

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
