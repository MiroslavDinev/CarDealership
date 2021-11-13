namespace CarDealership.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarDealership.Services.Cars;

    public class CarsController : AdminController
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }
        public IActionResult All()
        {
            var cars = this.carService
                .All(publicOnly: false)
                .Cars;

            return View(cars);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.carService.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
