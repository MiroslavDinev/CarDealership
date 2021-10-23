using System.Collections.Generic;

namespace CarDealership.Models.Home
{
    public class IndexViewModel
    {
        public int TotalCars { get; set; }
        public int TotalUsers { get; set; }
        public int TotalRents { get; set; }

        public IList<CarIndexViewModel> Cars { get; set; }
    }
}
