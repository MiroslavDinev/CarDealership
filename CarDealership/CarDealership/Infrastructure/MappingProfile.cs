namespace CarDealership.Infrastructure
{
    using AutoMapper;

    using CarDealership.Data.Models;
    using CarDealership.Models.Cars;
    using CarDealership.Services.Cars;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, CarCategoryServiceModel>();
            this.CreateMap<CarDetailsServiceModel,CarFormModel>();
            this.CreateMap<Car,LatestCarServiceModel>();
            this.CreateMap<Car, CarServiceModel>();
            this.CreateMap<Car, CarDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
