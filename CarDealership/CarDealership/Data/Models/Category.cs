namespace CarDealership.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Category;
    public class Category
    {
        public Category()
        {
            this.Cars = new HashSet<Car>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Car> Cars { get; set; }
    }
}
