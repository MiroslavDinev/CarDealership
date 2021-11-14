namespace CarDealership.Tests.Controller
{
    using System;
    using System.Collections.Generic;

    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarDealership.Controllers;
    using CarDealership.Services.Cars;

    using static Data.Cars;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectModelWithView()
        {
            MyController<HomeController>
                .Instance(controller => controller
                    .WithData(TenPublicCars()))
                .Calling(c => c.Index())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(LatestCarsCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                        .WithValueOfType<List<LatestCarServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<LatestCarServiceModel>>()
                    .Passing(model => model.Count==3));
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
        }
    }
}
