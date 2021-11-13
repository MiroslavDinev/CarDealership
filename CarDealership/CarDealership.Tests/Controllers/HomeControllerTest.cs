namespace CarDealership.Tests.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using CarDealership.Data.Models;


    public class HomeControllerTest
    {
       private static IEnumerable<Car> GetCars()
        {
            return Enumerable.Range(0, 10).Select(c => new Car());
        }
    }
}
