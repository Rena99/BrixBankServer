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
                .ForPath(d => d.Customer.FirstName, a => a.MapFrom(s => s.FirstName))
                .ForPath(d => d.Customer.LastName, a => a.MapFrom(s => s.LastName))
                .ForPath(d => d.Customer.Email, a => a.MapFrom(s => s.Email))
                .ForPath(d => d.Customer.Password, a => a.MapFrom(s => s.Password))
                .ForPath(d => d.EmailVerification.Email, a => a.MapFrom(s => s.Email))
                .ForPath(d => d.EmailVerification.VerificationCode, a => a.MapFrom(s => s.VerificationCode));
            CreateMap<AccountModel, AccountDTO>()
                .ForMember(d => d.FirstName, a => a.MapFrom(s => s.Customer.FirstName))
                .ForMember(d => d.LastName, a => a.MapFrom(s => s.Customer.LastName));
        }
    }
}
