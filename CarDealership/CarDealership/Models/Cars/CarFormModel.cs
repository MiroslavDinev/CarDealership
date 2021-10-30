namespace CarDealership.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Car;
    public class CarFormModel
    {
        [Required]
        [StringLength(CarBrandMaxLength, MinimumLength = CarBrandmMinLength)]
        public string Brand { get; set; }

        [Required]
        [StringLength(CarModelMaxLength, MinimumLength =CarModelMinLength)]
        public string Model { get; set; }

        [Required]
        [MinLength(CarDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }
    }
}
