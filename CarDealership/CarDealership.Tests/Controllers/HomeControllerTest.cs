namespace CarDealership.Tests.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Xunit;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc;

    using CarDealership.Controllers;
    using CarDealership.Data.Models;
    using CarDealership.Models.Home;
    using CarDealership.Services.Cars;
    using CarDealership.Services.Statistics;
    using CarDealership.Tests.Mocks;


    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange

            var homeController = new HomeController(null, null);

            //Act

            var result = homeController.Error();

            //Assert

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Cars.AddRange(GetCars());
            data.Users.Add(new User());
            data.SaveChanges();

            var statisticsService = new StatisticsService(data);
            var carService = new CarService(data, mapper);

            var homeController = new HomeController(statisticsService, carService);

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;
            var indexViewModel = Assert.IsType<IndexViewModel>(model);
            Assert.Equal(3, indexViewModel.Cars.Count());
            Assert.Equal(10, indexViewModel.TotalCars);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(controller => controller
                .WithData(GetCars())
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<IndexViewModel>()
                .Passing(m => m.Cars.Count == 3)));
        }

        private static IEnumerable<Car> GetCars()
        {
            return Enumerable.Range(0, 10).Select(c => new Car());
        }
    }
}
