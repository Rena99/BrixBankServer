using Xunit;
using System.Collections.Generic;
using System;
using System.Linq;
using Account.Services.Interfaces;
using AutoMapper;
using Account.Data.Entities;
using Moq;
using Microsoft.EntityFrameworkCore;
using Account.Data;
using Account.Data.Repositories;
using Account.Services.Services;
using Account.Services.Models;

namespace Account.Tests
{
    public class AccountTests
    {
        private readonly INewAccountService _service;
        public AccountTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebApi.Mappings.AutoMapper());
            });
            var mapper = config.CreateMapper();
            var customersData = new List<Customer>
            {
                new Customer { FirstName = "Rena", LastName="Markiewitz", Email="renamarkiewitz@gmail.com", CustomerId=new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5"), Password="7b+FFC1nuhIs8LeP3eYZBhM/oKOAgn7GzlduldTh+VU=", Salt="/PAaJXFGVGOxEWdGHv9TD+P6s9DaYmTws8ZMUkCp9Ow=" },
            };
            var accountsData = new List<Data.Entities.Account>
            {
                new Data.Entities.Account {AccountId=new Guid("9CFE55DF-65E2-4204-BAD5-08D82BD21687"), Balance=1000000, Customer=customersData.FirstOrDefault(c=>c.CustomerId== new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5")), OpenDate=Convert.ToDateTime("2020-07-19 13:03:43.4326053"), CustomerId=new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5") },
            };
            var customers = new Mock<DbSet<Customer>>();
            customers.SetSource(customersData);
            var accounts = new Mock<DbSet<Data.Entities.Account>>();
            accounts.SetSource(accountsData);
            var context = new Mock<AccountContext>();
            context.Setup(m => m.Accounts).Returns(accounts.Object);
            context.Setup(m => m.Customers).Returns(customers.Object);
            var newAccountRepository = new NewAccountRepository(context.Object, mapper);
            _service = new NewAccountService(newAccountRepository);
        }

        //[Fact]
        //public void AddCustomer_New_ReturnTrue()
        //{
        //    //Arrange
        //    var customerModel = new CustomerModel()
        //    {
        //        FirstName = "Rachelly",
        //        LastName = "Lamberger",
        //        Email = "rachelly@gmail.com",
        //        Password = "rachelly@brixbank"
        //    };

        //    //Act
        //    var result = _service.AddCustomer(customerModel);

        //    //Assert
        //    Assert.True(result);
        //}
    }
}
