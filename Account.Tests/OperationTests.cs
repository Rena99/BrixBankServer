//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Account.Data;
//using Account.Data.Entities;
//using Account.Data.Repositories;
//using Account.Services.Interfaces;
//using Account.Services.Models;
//using Account.Services.Services;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Xunit;

//namespace Account.Tests
//{
//    public class OperationTests
//    {
//        private readonly IOperationService _service;
//        public OperationTests()
//        {
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new WebApi.Mappings.AutoMapper());
//            });
//            var mapper = config.CreateMapper();
//            var customersData = new List<Customer>
//            {
//                new Customer { FirstName = "Rena", LastName="Markiewitz", Email="renamarkiewitz@gmail.com", CustomerId=new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5"), Password="7b+FFC1nuhIs8LeP3eYZBhM/oKOAgn7GzlduldTh+VU=", Salt="/PAaJXFGVGOxEWdGHv9TD+P6s9DaYmTws8ZMUkCp9Ow=" },
//            };
//            var accountsData = new List<Data.Entities.Account>
//            {
//                new Data.Entities.Account {AccountId=new Guid("9CFE55DF-65E2-4204-BAD5-08D82BD21687"), Balance=1000000, Customer=customersData.FirstOrDefault(c=>c.CustomerId== new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5")), OpenDate=Convert.ToDateTime("2020-07-19 13:03:43.4326053"), CustomerId=new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5") },
//            };
//            var emailVerificationsData = new List<EmailVerification>
//            {
//                new EmailVerification {Email="rachelly@gmail.com", VerificationCode=1234, ExpirationTime=DateTime.Now.AddDays(1) },
//            };
//            var operationsHistoryData = new List<OperationHistory>
//            {
//                new OperationHistory {OperationTime=DateTime.Now, AccountId=new Guid("9CFE55DF-65E2-4204-BAD5-08D82BD21687"), TransactionAmount=0, TransactionId=Guid.NewGuid(), Balance=10000, Debit=true, OperationHistoryId=Guid.NewGuid() },
//                new OperationHistory {OperationTime=DateTime.Now.AddDays(1), AccountId=new Guid("9CFE55DF-65E2-4204-BAD5-08D82BD21687"), TransactionAmount=0, TransactionId=Guid.NewGuid(), Balance=10000, Debit=false, OperationHistoryId=Guid.NewGuid() },
//            };
//            var customers = new Mock<DbSet<Customer>>();
//            customers.SetSource(customersData);
//            var accounts = new Mock<DbSet<Data.Entities.Account>>();
//            accounts.SetSource(accountsData);
//            var emailVerifications = new Mock<DbSet<EmailVerification>>();
//            emailVerifications.SetSource(emailVerificationsData);
//            var operationsHistory = new Mock<DbSet<OperationHistory>>();
//            operationsHistory.SetSource(operationsHistoryData);
//            var context = new Mock<AccountContext>();
//            context.Setup(m => m.Accounts).Returns(accounts.Object);
//            context.Setup(m => m.Customers).Returns(customers.Object);
//            context.Setup(m => m.EmailVerifications).Returns(emailVerifications.Object);
//            context.Setup(m => m.OperationsHistory).Returns(operationsHistory.Object);
//            var newAccountRepository = new OperationRepository(context.Object, mapper);
//            _service = new OperationService(newAccountRepository);
//        }

//        [Fact]
//        public void GetOperations_Exists_ReturnOperationList()
//        {
//            //Arrange
//            int page = 0;
//            int count = 1;
//            Guid accountId = Guid.Parse("9CFE55DF-65E2-4204-BAD5-08D82BD21687");

//            //Act
//            var result =  _service.GetOperations(page, count, accountId);

//            //Assert
//            Assert.IsType<OperationHistoryModel>(result.First());
//            Assert.True(result.Count == 1);
//        }

//        [Fact]
//        public void GetFilteredOperations_Exists_ReturnFilteredOperationList()
//        {
//            //Arrange
//            int page = 0;
//            int count = 5;
//            DateTime from = DateTime.MinValue;
//            DateTime to = DateTime.Now.AddHours(1);
//            Guid accountId = Guid.Parse("9CFE55DF-65E2-4204-BAD5-08D82BD21687");

//            //Act
//            var result = _service.GetFilteredList(from ,to, page, count, accountId);

//            //Assert
//            Assert.IsType<OperationHistoryModel>(result.First());
//            Assert.True(result.Count == 1);
//        }

//        [Fact]
//        public void GetSortedOperations_Exists_ReturnSortedOperationList()
//        {
//            //Arrange
//            int page = 0;
//            int count =5;
//            string sort = "date";
//            Guid accountId = Guid.Parse("9CFE55DF-65E2-4204-BAD5-08D82BD21687");

//            //Act
//            var result = _service.GetSortedList(sort, page, count, accountId);

//            //Assert
//            Assert.IsType<OperationHistoryModel>(result.First());
//            Assert.False(result.First().Debit);
//            Assert.True(result.Count == 2);
//        }
//    }
//}
