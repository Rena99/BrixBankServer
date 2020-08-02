﻿using System;
using System.Collections.Generic;
using System.Linq;
using Account.Data;
using Account.Data.Entities;
using Account.Data.Repositories;
using Account.Services.Interfaces;
using Account.Services.Models;
using Account.Services.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Account.Tests
{
    public class AccountInfoTests
    {
        private readonly IAccountInfoService _service;
        public AccountInfoTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebApi.Mappings.AutoMapper());
            });
            var mapper = config.CreateMapper();
            IQueryable<Customer> customersData = new List<Customer>
            {
                new Customer { FirstName = "Rena", LastName="Markiewitz", Email="renamarkiewitz@gmail.com", CustomerId=new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5"), Password="7b+FFC1nuhIs8LeP3eYZBhM/oKOAgn7GzlduldTh+VU=", Salt="/PAaJXFGVGOxEWdGHv9TD+P6s9DaYmTws8ZMUkCp9Ow=" },
            }.AsQueryable();
            var accountsData = new List<Data.Entities.Account>
            {
                new Data.Entities.Account {AccountId=new Guid("9CFE55DF-65E2-4204-BAD5-08D82BD21687"), Balance=1000000, Customer=customersData.FirstOrDefault(c=>c.CustomerId== new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5")), OpenDate=Convert.ToDateTime("2020-07-19 13:03:43.4326053"), CustomerId=new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5") },
            }.AsQueryable();
            var emailVerificationsData = new List<EmailVerification>
            {
                new EmailVerification {Email="rachelly@gmail.com", VerificationCode=1234, ExpirationTime=DateTime.Now.AddDays(1) },
            }.AsQueryable();
            //var customers = new Mock<DbSet<Customer>>();
            //customers.SetSource(customersData);
            //var accounts = new Mock<DbSet<Data.Entities.Account>>();
            //accounts.SetSource(accountsData);
            //var emailVerifications = new Mock<DbSet<EmailVerification>>();
            //emailVerifications.SetSource(emailVerificationsData);
            var context = new Mock<AccountContext>();
            context.SetupGet(x => x.Customers).Returns(MockDBSetExtensions.GetDbSet(customersData).Object);
            context.SetupGet(x => x.Accounts).Returns(MockDBSetExtensions.GetDbSet(accountsData).Object);
            context.SetupGet(x => x.EmailVerifications).Returns(MockDBSetExtensions.GetDbSet(emailVerificationsData).Object);

            //context.Setup(m => m.Accounts).Returns(accounts.Object);
            //context.Setup(m => m.Customers).Returns(customers.Object);
            //context.Setup(m => m.EmailVerifications).Returns(emailVerifications.Object);
            var accountInfoRepository = new AccountInfoRepository(context.Object, mapper);
            _service = new AccountInfoService(accountInfoRepository);
        }

        [Fact]
        public void GetAccount_Exists_ReturnAccounModel()
        {
            //Arrange
            var accountId = new Guid("9CFE55DF-65E2-4204-BAD5-08D82BD21687");

            //Act
            var result = _service.GetAccount(accountId).Result;

            //Assert
            Assert.IsType<AccountModel>(result);
        }
    }
}

