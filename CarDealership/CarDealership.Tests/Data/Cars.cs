namespace CarDealership.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using CarDealership.Data.Models;

    public static class Cars
    {
        public static IEnumerable<Car> TenPublicCars()
        {
            return Enumerable.Range(0, 10).Select(c => new Car
            {
                IsPublic = true
            });
        }
    }
}
