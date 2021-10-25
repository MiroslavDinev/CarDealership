namespace CarDealership.Services.Cars
{
    using System.Collections.Generic;

    using CarDealership.Models.Cars;

    public interface ICarService
    {
        CarQueryServiceModel All(string brand, string searchTerm, CarSorting sorting, int currentPage, int carsPerPage);

        IEnumerable<string> AllCarBrands();
    }
}
