namespace CarDealership.Models.Dealers
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Dealer;

    public class DealerCreateFormModel
    {
        [Required]
        [StringLength(MaxNameLength,MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxPhoneNumberLength, MinimumLength = MinPhoneNumberLength)]
        public string PhoneNumber { get; set; }
    }
}
