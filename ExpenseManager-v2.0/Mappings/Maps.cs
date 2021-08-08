namespace ExpenseManager_v2._0.Mappings
{
    using AutoMapper;
    using ExpenseManager_v2._0.Services.Credit;

    using static ExpenseManager_v2._0.Data.DataConstants;

    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Credit, CreditServiceListingModel>();
        }
    }
}
