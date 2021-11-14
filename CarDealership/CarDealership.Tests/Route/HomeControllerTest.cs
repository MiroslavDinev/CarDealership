namespace CarDealership.Tests.Route
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarDealership.Controllers;
    public class HomeControllerTest
    {
        [Fact]
        public void IndexActionShouldBeMapped()
        {
            MyRouting.Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());               
        } 
        
        [Fact]
        public void ErrorActionShouldBeMapper()
        {
            MyRouting.Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
        }
    }
}
