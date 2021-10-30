namespace CarDealership.Services.Cars
{
    public class CarDetailsServiceModel : CarServiceModel
    {
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
