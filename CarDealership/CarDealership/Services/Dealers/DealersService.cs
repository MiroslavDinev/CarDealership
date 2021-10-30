namespace CarDealership.Services.Dealers
{
    using CarDealership.Data;
    using System.Linq;

    public class DealersService : IDealersService
    {
        private readonly CarDealershipDbContext data;

        public DealersService(CarDealershipDbContext data)
        {
            this.data = data;
        }

        public int GetUserById(string userId)
        {
            return this.data.Dealers
                .Where(x => x.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
        }

        public bool IsDealer(string userId)
        {
            return this.data.Dealers.Any(x => x.UserId == userId);
        }
    }
}
