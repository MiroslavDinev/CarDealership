namespace CarDealership.Services.Statistics
{
    using System.Linq;

    using CarDealership.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly CarDealershipDbContext data;

        public StatisticsService(CarDealershipDbContext data)
        {
            this.data = data;
        }
        public StatisticsServiceModel Total()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers
            };
        }
    }
}
