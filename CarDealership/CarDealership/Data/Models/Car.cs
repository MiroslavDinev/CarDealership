using System.ComponentModel.DataAnnotations;

namespace CarDealership.Data.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.CarBrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(DataConstants.CarModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
