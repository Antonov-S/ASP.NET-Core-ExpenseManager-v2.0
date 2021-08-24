namespace ExpenseManager_v2._0.Infrastructure
{
    using AutoMapper;
    using System.Globalization;
    using ExpenseManager_v2._0.Data.Models;
    using ExpenseManager_v2._0.Services.Credit;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddCreditServiceModel, Credit>().ReverseMap();

            CreateMap<Credit, CreditServiceListingModel>()
                .ForMember(
                p => p.MaturityDate,
                opt => opt.MapFrom(p => p.MaturityDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ReverseMap();

            CreateMap<Credit, CreditDetailsServiceModel>()
                .ForMember(
                p => p.MaturityDate,
                opt => opt.MapFrom(p => p.MaturityDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ReverseMap();

            CreateMap<ListingInstallmentLoansServiceModel, InstallmentLoan>().ReverseMap();
        }
    }
}
