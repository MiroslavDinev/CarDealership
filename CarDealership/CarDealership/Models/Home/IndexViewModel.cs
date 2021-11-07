namespace CarDealership.Models.Home
{
    using CarDealership.Services.Cars;
    using System.Collections.Generic;
    public class IndexViewModel
    {
        public int TotalCars { get; set; }
        public int TotalUsers { get; set; }
        public int TotalRents { get; set; }

        public IList<LatestCarServiceModel> Cars { get; set; }
    }
}
