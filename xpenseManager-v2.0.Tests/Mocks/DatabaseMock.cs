namespace xpenseManager_v2._0.Tests.Mocks
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using ExpenseManager_v2._0.Data;    

    public static class DatabaseMock
    {
        public static ExpenseManagerDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ExpenseManagerDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ExpenseManagerDbContext(dbContextOptions);
            }
        }
    }
}
