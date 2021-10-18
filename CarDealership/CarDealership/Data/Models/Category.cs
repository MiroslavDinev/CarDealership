using System.Collections.Generic;

namespace CarDealership.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Cars = new HashSet<Car>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Car> Cars { get; set; }
    }
}
