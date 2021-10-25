namespace CarDealership.Services.Cars
{
    using System.Collections.Generic;
    using System.Linq;

    using CarDealership.Data;
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

            var allCars = carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
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

            return new CarQueryServiceModel
            {
                CarsPerPage = carsPerPage,
                CurrentPage = currentPage,
                TotalCars = totalCars,
                Cars = allCars
            };
        }

        public IEnumerable<string> AllCarBrands()
        {
            return this.data.Cars
                .Select(x => x.Brand)
                .Distinct()
                .ToList();
        }
    }
}
