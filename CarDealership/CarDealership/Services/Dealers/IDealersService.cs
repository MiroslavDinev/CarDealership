namespace CarDealership.Services.Dealers
{
    public interface IDealersService
    {
        bool IsDealer(string userId);

        int GetUserById(string userId);
    }
}
