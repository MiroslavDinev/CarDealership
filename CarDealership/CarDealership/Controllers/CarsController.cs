namespace CarDealership.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using CarDealership.Models.Cars;
    using CarDealership.Services.Cars;
    using CarDealership.Infrastructure;
    using CarDealership.Services.Dealers;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IDealersService dealersService;

        public CarsController(ICarService carService, IDealersService dealersService)
        {
            this.carService = carService;
            this.dealersService = dealersService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.GetId();

            if (!this.dealersService.IsDealer(userId))
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return this.View(new CarFormModel
            {
                Categories = this.carService.GetCarCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFormModel car)
        {
            var userId = this.User.GetId();

            var dealerId = this.dealersService.GetUserById(userId);

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!this.carService.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.carService.GetCarCategories();

                return this.View(car);
            }

            this.carService.Create(car.Brand, car.Model, car.Description, car.ImageUrl, car.Year, car.CategoryId, dealerId);

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

        [Authorize]
        public IActionResult Mine()
        {
            var myCars = this.carService.ByUser(this.User.GetId());
            return this.View(myCars);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.dealersService.IsDealer(userId))
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            var car = this.carService.Details(id);

            if(car.UserId != userId)
            {
                return Unauthorized();
            }

            return this.View(new CarFormModel
            {
                Brand = car.Brand,
                Model = car.Model,
                ImageUrl = car.ImageUrl,
                Description = car.Description,
                Year = car.Year,
                CategoryId = car.CategoryId,
                Categories = this.carService.GetCarCategories()
            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Edit(int id, CarFormModel car)
        {
            var userId = this.User.GetId();

            var dealerId = this.dealersService.GetUserById(userId);

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!this.carService.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.carService.GetCarCategories();

                return this.View(car);
            }

            if (!this.carService.CarIsByDealer(id, dealerId))
            {
                return BadRequest();
            }

            this.carService.Edit(id, car.Brand, car.Model, car.Description, car.ImageUrl, car.Year, car.CategoryId);

            return RedirectToAction(nameof(All));
        }
    }
}
