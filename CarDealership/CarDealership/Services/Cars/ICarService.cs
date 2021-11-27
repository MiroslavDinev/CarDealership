namespace CarDealership.Services.Cars
{
    using System.Collections.Generic;

    using CarDealership.Models.Cars;

    public interface ICarService
    {
        CarQueryServiceModel All(string brand = null, 
            string searchTerm = null, 
            CarSorting sorting = CarSorting.DateCreated, 
            int currentPage = 1,
            int carsPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<string> AllCarBrands();

        IEnumerable<LatestCarServiceModel> Latest();

        IEnumerable<CarServiceModel> ByUser(string userId);

        IEnumerable<CarCategoryServiceModel> GetCarCategories();

        CarDetailsServiceModel Details(int carId);

        bool CategoryExists(int categoryId);

        void ChangeVisibility(int carId);
        bool Delete(int carId);

        int Create(string brand, string model, string description, string imageUrl, int year, int categoryId, int dealerId);
        bool Edit(int carId, string brand, string model, string description, string imageUrl, int year, int categoryId, bool isPublic);

        bool CarIsByDealer(int carId, int dealerId);
    }
}
