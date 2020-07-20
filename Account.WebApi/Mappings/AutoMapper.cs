using Account.Data.Entities;
using Account.Services.Models;
using Account.WebApi.DTO;
using AutoMapper;

namespace Account.WebApi.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CustomerModel, Customer>();
            CreateMap<Customer, CustomerModel>();
            CreateMap<Data.Entities.Account, AccountModel>();
            CreateMap<CustomerDTO, CustomerModel>();
            CreateMap<AccountModel, AccountDTO>()
                .ForMember(d => d.FirstName, a => a.MapFrom(s => s.Customer.FirstName))
                .ForMember(d => d.LastName, a => a.MapFrom(s => s.Customer.LastName));
        }
    }
}
