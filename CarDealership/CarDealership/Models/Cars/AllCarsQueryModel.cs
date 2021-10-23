﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models.Cars
{
    public class AllCarsQueryModel
    {
        public const int CarsPerPage = 3;

        public int CurrentPage { get; set; } = 1;

        public int TotalCars { get; set; }
        public string Brand { get; set; }
        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
        public CarSorting Sorting { get; set; }
        public IEnumerable<CarListingViewModel> Cars { get; set; }
    }
}