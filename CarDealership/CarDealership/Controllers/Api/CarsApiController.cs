namespace CarDealership.Models.Api
{
    using Microsoft.AspNetCore.Mvc;

    using CarDealership.Models.Api.Cars;
    using CarDealership.Services.Cars;

    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly ICarService carService;

        public CarsApiController(ICarService carService)
        {
            this.carService = carService;
        }

        public CarQueryServiceModel All([FromQuery] AllCarsApiRequestModel query)
        {
            return this.carService.All(query.Brand, query.SearchTerm, query.Sorting, query.CurrentPage, query.CarsPerPage);
        }
    }
}
