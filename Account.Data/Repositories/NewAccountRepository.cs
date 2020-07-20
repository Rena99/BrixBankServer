using Account.Data.Entities;
using Account.Services.Interfaces;
using Account.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

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

        public bool AddCustomer(CustomerModel customerModel)
        {
            try
            {
                if (_context.Customers.ToList().Any(c => c.Email == customerModel.Email))
                {
                    return false;
                }
                Customer customer = _mapper.Map<Customer>(customerModel);
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
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
