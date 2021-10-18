using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarDealership.Data;

namespace CarDealership.Models.Cars
{
    public class CarAddFormModel
    {
        [Required]
        [StringLength(DataConstants.CarBrandMaxLength, MinimumLength = DataConstants.CarBrandmMinLength)]
        public string Brand { get; set; }

        [Required]
        [StringLength(DataConstants.CarModelMaxLength, MinimumLength = DataConstants.CarModelMinLength)]
        public string Model { get; set; }

        [Required]
        [MinLength(DataConstants.CarDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Range(DataConstants.CarYearMinValue, DataConstants.CarYearMaxValue)]
        public int Year { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }
    }
}
