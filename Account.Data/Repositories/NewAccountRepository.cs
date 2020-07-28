using Account.Data.Entities;
using Account.Services.Interfaces;
using Account.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Account.Data.Repositories
{
    public class NewAccountRepository:INewAccountRepository
    {
        private readonly AccountContext _context;
        private readonly IMapper _mapper;

        public NewAccountRepository(AccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddCustomer(VerificationHelperModel customerModel)
        {
            try
            {
                EmailVerification emailVerification = await _context.EmailVerifications.FirstOrDefaultAsync(e => e.Email == customerModel.EmailVerification.Email).ConfigureAwait(false);
                if (emailVerification.VerificationCode != customerModel.EmailVerification.VerificationCode ||
                    emailVerification.ExpirationTime.CompareTo(DateTime.Now) !< 0)
                    return false;
                if (_context.Customers.ToList().Any(c => c.Email == customerModel.Customer.Email))
                {
                    return false;
                }
                Customer customer = _mapper.Map<Customer>(customerModel.Customer);
                customer.Salt = Hashing.GetSalt();
                customer.CustomerId = Guid.NewGuid();
                customer.Password = Hashing.GenerateHash(customer.Password, customer.Salt);
                _context.Customers.Add(customer);
                _context.Accounts.Add(new Entities.Account()
                {
                    CustomerId = customer.CustomerId,
                    AccountId = Guid.NewGuid(),
                    OpenDate=DateTime.Now
                });
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetEmail(string email)
        {
            int code = new Random().Next(1000, 9999);
            _context.EmailVerifications.Add(new EmailVerification()
            {
                Email = email,
                VerificationCode = code,
                ExpirationTime = DateTime.Now.AddDays(1),
                EmailVerificationId = Guid.NewGuid()
            });
            _context.SaveChanges();
            return code;
        }
    }
}
