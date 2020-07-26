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
            CreateMap<CustomerDTO, VerificationHelperModel>()
                .ForMember(d => d.Customer.FirstName, a => a.MapFrom(s => s.FirstName))
                .ForMember(d => d.Customer.LastName, a => a.MapFrom(s => s.LastName))
                .ForMember(d => d.Customer.Email, a => a.MapFrom(s => s.Email))
                .ForMember(d => d.Customer.Password, a => a.MapFrom(s => s.Password))
                .ForMember(d => d.EmailVerification.Email, a => a.MapFrom(s => s.Email))
                .ForMember(d => d.EmailVerification.VerificationCode, a => a.MapFrom(s => s.VerificationCode));
            CreateMap<AccountModel, AccountDTO>()
                .ForMember(d => d.FirstName, a => a.MapFrom(s => s.Customer.FirstName))
                .ForMember(d => d.LastName, a => a.MapFrom(s => s.Customer.LastName));
        }
    }
}
