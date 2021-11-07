namespace CarDealership.Tests.Services
{
    using Xunit;

    using CarDealership.Data.Models;
    using CarDealership.Services.Dealers;
    using CarDealership.Tests.Mocks;

    public class DealersServiceTests
    {
        [Fact]
        public void IsDealerShouldReturnTrueWhenUserIsDealer()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var userId = "Test1234";
            data.Dealers.Add(new Dealer { UserId = userId });
            data.SaveChanges();
            var dealersService = new DealersService(data);

            //Act
            var result = dealersService.IsDealer(userId);

            //Assert
            Assert.True(result);
        }
    }
}
