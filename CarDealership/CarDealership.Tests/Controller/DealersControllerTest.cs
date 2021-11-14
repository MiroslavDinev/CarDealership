namespace CarDealership.Tests.Controller
{
    using System.Linq;

    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarDealership.Controllers;
    using CarDealership.Models.Dealers;
    using CarDealership.Data.Models;

    using static WebConstants;

    public class DealersControllerTest
    {
        [Fact]
        public void GetCreateShouldBeForAuthorizedUserAndReturnView()
        {
            MyController<DealersController>.Instance()
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Theory]
        [InlineData("Dealer", "+3598888888")]

        public void PostCreateShouldBeForAuthorizedUsersAndShouldRedirectWithValidModel(string dealerName, string phoneNumber)
        {
            MyController<DealersController>.Instance(controller => controller
            .WithUser())
                .Calling(c => c.Create(new DealerCreateFormModel
                {
                    Name = dealerName,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                .WithSet<Dealer>(dealers => dealers
                .Any(d => d.Name == dealerName &&
                d.PhoneNumber == phoneNumber &&
                d.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All", "Cars");
        }
    }
}
