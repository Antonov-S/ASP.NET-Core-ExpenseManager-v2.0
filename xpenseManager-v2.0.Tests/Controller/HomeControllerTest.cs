namespace xpenseManager_v2._0.Tests.Controller
{
    using Xunit;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Controllers;
    using ExpenseManager_v2._0.Services.Statistics;
    using xpenseManager_v2._0.Tests.Mocks;
    using ExpenseManager_v2._0.Models.Home;
    using System.Linq;
    using ExpenseManager_v2._0.Data.Models;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.Expenses.AddRange(Enumerable.Range(0, 10).Select(i => new ExpenseManager_v2._0.Data.Models.Expense()));
            data.ApplicationUsers.Add(new ApplicationUser());
            data.SaveChanges();

            var statisticService = new StatisticsService(data);

            var homeController = new HomeController(statisticService);

            // Act
            var result = homeController.Index();

            // Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(10, indexViewModel.TotalTransactions);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }


        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(null);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
