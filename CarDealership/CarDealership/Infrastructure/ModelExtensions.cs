namespace CarDealership.Infrastructure
{
    using CarDealership.Services.Cars;

    public static class ModelExtensions
    {
        public static string GetInformation(this ICarModel car)
        {
            return car.Brand + "-" + car.Model + "-" + car.Year;
        }
    }
}
