namespace CarDealership.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using CarDealership.Data;
    using CarDealership.Data.Models;
    using CarDealership.Infrastructure;
    using CarDealership.Models.Dealers;
    
    public class DealersController : Controller
    {
        private readonly CarDealershipDbContext data;

        public DealersController(CarDealershipDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(DealerCreateFormModel dealer)
        {
            var userId = this.User.GetId();
            var userIsAlreadyDealer = this.data.Dealers
                .Any(x => x.UserId == userId);

            if(userIsAlreadyDealer)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return this.View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            return this.RedirectToAction("All", "Cars");
        }
    }
}
