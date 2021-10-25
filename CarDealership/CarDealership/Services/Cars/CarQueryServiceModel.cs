﻿namespace CarDealership.Services.Cars
{
    using System.Collections.Generic;

    public class CarQueryServiceModel
    {
        public int CurrentPage { get; set; }
        public int CarsPerPage { get; set; }
        public int TotalCars { get; set; }
        public IEnumerable<CarServiceModel> Cars { get; set; }
    }
}
