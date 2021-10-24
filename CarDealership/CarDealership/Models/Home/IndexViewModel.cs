﻿namespace CarDealership.Models.Home
{
    using System.Collections.Generic;
    public class IndexViewModel
    {
        public int TotalCars { get; set; }
        public int TotalUsers { get; set; }
        public int TotalRents { get; set; }

        public IList<CarIndexViewModel> Cars { get; set; }
    }
}
