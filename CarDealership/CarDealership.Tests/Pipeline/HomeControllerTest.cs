namespace CarDealership.Tests.Pipeline
{
    using System.Collections.Generic;

    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarDealership.Controllers;
    using CarDealership.Services.Cars;

    using static Data.Cars;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
        {
             MyMvc.Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(controller => controller.
                WithData(TenPublicCars()))
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<List<LatestCarServiceModel>>()
                .Passing(m => m.Count == 3));
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            MyMvc.Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error())
                 .Which()
                 .ShouldReturn()
                 .View();
        }
    }
}
