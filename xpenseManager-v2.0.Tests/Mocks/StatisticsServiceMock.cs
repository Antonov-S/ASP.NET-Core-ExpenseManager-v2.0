namespace xpenseManager_v2._0.Tests.Mocks
{
    using Moq;
    using ExpenseManager_v2._0.Services.Statistics;   

    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    { 
                        TotalTransactions = 10,
                        TotalUsers = 15
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
