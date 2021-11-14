namespace CarDealership.Tests.Route
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarDealership.Controllers;
    using CarDealership.Models.Dealers;

    public class DealersControllerTest
    {
        [Fact]
        public void GetRouteTestShouldBeMapped()
        {
            MyRouting.Configuration()
                .ShouldMap("/Dealers/Create")
                .To<DealersController>(c => c.Create());
        }

        [Fact]
        public void PostRouteTestShouldBeMapped()
        {
            MyRouting.Configuration()
                .ShouldMap(request => request
                .WithPath("/Dealers/Create")
                .WithMethod(HttpMethod.Post))
                .To<DealersController>(c => c.Create(With.Any<DealerCreateFormModel>()));
                
        }
    }
}
