namespace CarDealership.Services.Cars
{
    using System.Collections.Generic;
    using System.Linq;

    using CarDealership.Data;
    using CarDealership.Data.Models;
    using CarDealership.Models.Cars;

    public class CarService : ICarService
    {
        private readonly CarDealershipDbContext data;

        public CarService(CarDealershipDbContext data)
        {
            this.data = data;
        }
        public CarQueryServiceModel All(string brand, string searchTerm, CarSorting sorting, int currentPage, int carsPerPage)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(x => x.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(c => (c.Brand + " " + c.Model).ToLower().Contains(searchTerm.ToLower())
                || c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.DateCreated => carsQuery.OrderByDescending(x => x.Id),
                CarSorting.Year => carsQuery.OrderByDescending(x => x.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(x => x.Brand).ThenBy(x => x.Model),
                _ => carsQuery.OrderByDescending(x => x.Id)
            };

            var totalCars = carsQuery.Count();

            var allCars = GetCars(carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage));
                

            return new CarQueryServiceModel
            {
                CarsPerPage = carsPerPage,
                CurrentPage = currentPage,
                TotalCars = totalCars,
                Cars = allCars
            };
        }

        public int Create(string brand, string model, string description, string imageUrl, int year, int categoryId, int dealerId)
        {
            var carData = new Car
            {
                Brand = brand,
                Model = model,
                Description = description,
                ImageUrl = imageUrl,
                Year = year,
                CategoryId = categoryId,
                DealerId = dealerId
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return carData.Id;
        }

        public bool Edit(int id, string brand, string model, string description, string imageUrl, int year, int categoryId)
        {
            var carData = this.data.Cars.Find(id);

            if(carData == null)
            {
                return false;
            }

            carData.Brand = brand;
            carData.Model = model;
            carData.Description = description;
            carData.ImageUrl = imageUrl;
            carData.Year = year;
            carData.CategoryId = categoryId;
            
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<CarServiceModel> ByUser(string userId)
        {
            return GetCars(this.data.Cars
                .Where(x => x.Dealer.UserId == userId));
                
        }

        public IEnumerable<string> AllCarBrands()
        {
            return this.data.Cars
                .Select(x => x.Brand)
                .Distinct()
                .ToList();
        }

        public IEnumerable<CarCategoryServiceModel> GetCarCategories()
        {
            return this.data.Categories
                .Select(c => new CarCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
        }

        public CarDetailsServiceModel Details(int id)
        {
            return this.data.Cars
                .Where(c => c.Id == id)
                .Select(x => new CarDetailsServiceModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    Year = x.Year,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    DealerId = x.DealerId,
                    DealerName = x.Dealer.Name,
                    UserId = x.Dealer.UserId

                }).FirstOrDefault();
        }

        public bool CategoryExists(int categoryId)
        {
            return this.data.Categories.Any(x => x.Id == categoryId);
        }

        public bool CarIsByDealer(int carId, int dealerId)
        {
            return this.data.Cars
                .Any(x => x.Id == carId && x.DealerId == dealerId);
        }

        private static IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carsQuery)
        {
            return carsQuery
                .Select(x => new CarServiceModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Year = x.Year,
                    Category = x.Category.Name
                }).ToList();
        }
    }
}
