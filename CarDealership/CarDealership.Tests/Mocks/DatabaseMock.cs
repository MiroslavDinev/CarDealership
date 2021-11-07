namespace CarDealership.Tests.Mocks
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using CarDealership.Data;

    public static class DatabaseMock
    {
        public static CarDealershipDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<CarDealershipDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

                return new CarDealershipDbContext(dbContextOptions);
            }
        }
    }
}
