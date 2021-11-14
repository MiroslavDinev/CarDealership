namespace CarDealership.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using CarDealership.Services.Cars;

    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly ICarService carService;
        private readonly IMemoryCache cache;

        public HomeController(ICarService carService, IMemoryCache cache)
        {
            this.carService = carService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var latestCars = this.cache.Get<List<LatestCarServiceModel>>(LatestCarsCacheKey);

            if(latestCars == null)
            {
                latestCars = this.carService.Latest().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestCarsCacheKey, latestCars, cacheOptions);
            }

            return this.View(latestCars);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
