namespace CarDealership.Infrastructure
{
    using AutoMapper;
    using CarDealership.Data.Models;
    using CarDealership.Models.Cars;
    using CarDealership.Models.Home;
    using CarDealership.Services.Cars;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<CarDetailsServiceModel,CarFormModel>();
            this.CreateMap<Car,LatestCarServiceModel>();
            this.CreateMap<Car, CarDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
