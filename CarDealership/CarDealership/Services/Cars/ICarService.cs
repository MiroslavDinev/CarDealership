namespace CarDealership.Services.Cars
{
    using System.Collections.Generic;

    using CarDealership.Models.Cars;

    public interface ICarService
    {
        CarQueryServiceModel All(string brand, string searchTerm, CarSorting sorting, int currentPage, int carsPerPage);

        IEnumerable<string> AllCarBrands();
        IEnumerable<LatestCarServiceModel> Latest();

        IEnumerable<CarServiceModel> ByUser(string userId);

        IEnumerable<CarCategoryServiceModel> GetCarCategories();

        CarDetailsServiceModel Details(int carId);

        bool CategoryExists(int categoryId);

        int Create(string brand, string model, string description, string imageUrl, int year, int categoryId, int dealerId);
        bool Edit(int carId, string brand, string model, string description, string imageUrl, int year, int categoryId);

        bool CarIsByDealer(int carId, int dealerId);
    }
}
