namespace CarDealership.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;

    using CarDealership.Models.Cars;
    using CarDealership.Services.Cars;
    using CarDealership.Infrastructure;
    using CarDealership.Services.Dealers;
    using static WebConstants;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IDealersService dealersService;
        private readonly IMapper mapper;

        public CarsController(ICarService carService, IDealersService dealersService, IMapper mapper)
        {
            this.carService = carService;
            this.dealersService = dealersService;
            this.mapper = mapper;
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

            var carId = this.carService
                .Create(car.Brand, car.Model, car.Description, car.ImageUrl, car.Year, car.CategoryId, dealerId);

            this.TempData[GlobalMessageKey] = "Car successfully added and awaiting approval!";

            return RedirectToAction(nameof(Details), new { id= carId, information = car.GetInformation()});            
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

            if (!this.dealersService.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            var car = this.carService.Details(id);

            if(car.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var carForm = this.mapper.Map<CarFormModel>(car);
            carForm.Categories = this.carService.GetCarCategories();

            return this.View(carForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CarFormModel car)
        {
            var userId = this.User.GetId();

            var dealerId = this.dealersService.GetUserById(userId);

            if (dealerId == 0 && !User.IsAdmin())
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

            if (!this.carService.CarIsByDealer(id, dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.carService.Edit(id, car.Brand, car.Model, car.Description, car.ImageUrl, car.Year, car.CategoryId, this.User.IsAdmin());

            this.TempData[GlobalMessageKey] = $"Car successfully edited{(this.User.IsAdmin() ? string.Empty : "and awaiting approval")} !";

            return RedirectToAction(nameof(Details), new { id = id, information = car.GetInformation() });
        }

        public IActionResult Details(int id, string information)
        {
            var car = this.carService.Details(id);

            if(information != car.GetInformation())
            {
                return BadRequest();
            }

            return View(car);
        }
    }
}
