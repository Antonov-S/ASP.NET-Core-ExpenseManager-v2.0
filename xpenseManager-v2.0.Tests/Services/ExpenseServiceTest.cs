namespace xpenseManager_v2._0.Tests.Services
{
    using Xunit;
    using ExpenseManager_v2._0.Services.Expense;
    using xpenseManager_v2._0.Tests.Mocks;

    public class ExpenseServicetest
    {
        private const int Id = 5;

        [Fact]
        public void IsExpenseExistShouldReturnTrueWhenExpenseExist()
        {
            // Arrange
            var expenceService = GetExpenseService();

            // Act
            var result = expenceService.IsExpenseExist(Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsExpenseExistShouldReturnFalseWhenExpenseDosentExist()
        {
            // Arrange
            var expenceService = GetExpenseService();

            // Act
            var result = expenceService.IsExpenseExist(6);

            // Assert
            Assert.False(result);
        }

        private static IExpenseService GetExpenseService()
        {
            const int Id = 5;
            var data = DatabaseMock.Instance;

            data.Expenses.Add(new ExpenseManager_v2._0.Data.Models.Expense { Id = Id });
            data.SaveChanges();

            return new ExpenseService(data);
        }
    }
}
