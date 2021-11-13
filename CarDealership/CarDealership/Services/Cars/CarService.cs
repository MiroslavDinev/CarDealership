namespace CarDealership.Services.Cars
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CarDealership.Data;
    using CarDealership.Data.Models;
    using CarDealership.Models.Cars;

    public class CarService : ICarService
    {
        private readonly CarDealershipDbContext data;
        private readonly IConfigurationProvider mapper;

        public CarService(CarDealershipDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }
        public CarQueryServiceModel All(string brand = null, 
            string searchTerm = null, 
            CarSorting sorting = CarSorting.DateCreated, 
            int currentPage = 1, 
            int carsPerPage= int.MaxValue,
            bool publicOnly = true)
        {
            var carsQuery = this.data.Cars
                .Where(c=> publicOnly ? c.IsPublic : true);

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
                DealerId = dealerId,
                IsPublic = false
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return carData.Id;
        }

        public bool Edit(int id, string brand, string model, string description, string imageUrl, int year, int categoryId, bool isPublic)
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
            carData.IsPublic = isPublic;
            
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
            return this.data
                .Categories
                .ProjectTo<CarCategoryServiceModel>(this.mapper)
                .ToList();
        }

        public CarDetailsServiceModel Details(int id)
        {
            return this.data.Cars
                .Where(c => c.Id == id)
                .ProjectTo<CarDetailsServiceModel>(this.mapper)
                .FirstOrDefault();
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

        public IEnumerable<LatestCarServiceModel> Latest()
        {
            return this.data.Cars
                .Where(c=> c.IsPublic)
                .OrderByDescending(x => x.Id)
                .ProjectTo<LatestCarServiceModel>(this.mapper)
                .Take(3)
                .ToList();
        }

        public void ChangeVisibility(int carId)
        {
            var car = this.data.Cars.Find(carId);

            car.IsPublic = !car.IsPublic;

            this.data.SaveChanges();
        }

        private IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carsQuery)
        {
            return carsQuery
                .ProjectTo<CarServiceModel>(this.mapper)
                .ToList();
        }
    }
}
