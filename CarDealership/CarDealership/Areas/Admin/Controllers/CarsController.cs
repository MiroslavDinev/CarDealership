namespace CarDealership.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
