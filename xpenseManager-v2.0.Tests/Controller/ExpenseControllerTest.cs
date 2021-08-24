namespace xpenseManager_v2._0.Tests.Controller
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using ExpenseManager_v2._0.Controllers;

    public class ExpenseControllerTest
    {
        [Fact]
        public void AddShouldBeForAutorizedUsersAndReturnView()
            => MyMvc
            .Pipeline()
            .ShouldMap("/Expense/All")
            .To<ExpenseController>(c => c.All())
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();
    }
}
