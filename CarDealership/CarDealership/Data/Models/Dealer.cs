namespace CarDealership.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Dealer;
    public class Dealer
    {
        public Dealer()
        {
            this.Cars = new HashSet<Car>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
