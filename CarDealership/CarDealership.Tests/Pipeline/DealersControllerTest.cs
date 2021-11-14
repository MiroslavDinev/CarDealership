namespace CarDealership.Tests.Pipeline
{
    using System.Linq;

    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarDealership.Data.Models;
    using CarDealership.Models.Dealers;
    using CarDealership.Controllers;

    using static WebConstants;

    public class DealersControllerTest
    {
        [Fact]
        public void GetCreateShouldBeForAuthorizedUsersAndReturnView()
        {
            MyMvc.Pipeline()
                .ShouldMap(request => request
                .WithPath("/Dealers/Create")
                .WithUser())
                .To<DealersController>(c => c.Create())
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Theory]
        [InlineData("Dealer", "+3598888888")]

        public void PostCreateShouldBeForAuthorizedUsersAndShouldRedirectWithValidModel(string dealerName, string phoneNumber)
        {
            MyPipeline.Configuration()
                .ShouldMap(request=> request
            .WithPath("/Dealers/Create")
            .WithMethod(HttpMethod.Post)
            .WithFormFields(new 
            { 
                Name = dealerName,
                PhoneNumber = phoneNumber
            })
            .WithUser()
            .WithAntiForgeryToken())
                .To<DealersController>(c=> c.Create(new DealerCreateFormModel
                {
                    Name = dealerName,
                    PhoneNumber = phoneNumber
                }))
                .Which()
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
