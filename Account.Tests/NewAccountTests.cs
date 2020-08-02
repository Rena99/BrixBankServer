using Xunit;
using System.Collections.Generic;
using System;
using System.Linq;
using Account.Services.Interfaces;
using AutoMapper;
using Account.Data.Entities;
using Moq;
using Account.Data;
using Account.Data.Repositories;
using Account.Services.Services;
using Account.Services.Models;
using System.Threading.Tasks;

namespace Account.Tests
{
    public class NewAccountTests
    {
        private readonly INewAccountService _service;
        public NewAccountTests()
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
                new EmailVerification {Email="renamarkiewitz@gmail.com", VerificationCode=5678, ExpirationTime=DateTime.Now.AddMinutes(15) },
            }.AsQueryable();
            var operationsHistoryData = new List<OperationHistory>
            {
                new OperationHistory {OperationTime=DateTime.Now, AccountId=new Guid("9CFE55DF-65E2-4204-BAD5-08D82BD21687"), TransactionAmount=0, TransactionId=Guid.NewGuid(), Balance=10000, Debit=true, OperationHistoryId=Guid.NewGuid() },
                new OperationHistory {OperationTime=DateTime.Now.AddDays(1), AccountId=new Guid("9CFE55DF-65E2-4204-BAD5-08D82BD21687"), TransactionAmount=0, TransactionId=Guid.NewGuid(), Balance=10000, Debit=false, OperationHistoryId=Guid.NewGuid() },
            }.AsQueryable();
            var context = new Mock<AccountContext>();
            context.SetupGet(x => x.Customers).Returns(MockDBSetExtensions.GetDbSet(customersData).Object);
            context.SetupGet(x => x.Accounts).Returns(MockDBSetExtensions.GetDbSet(accountsData).Object);
            context.SetupGet(x => x.EmailVerifications).Returns(MockDBSetExtensions.GetDbSet(emailVerificationsData).Object);
            context.SetupGet(x => x.OperationsHistory).Returns(MockDBSetExtensions.GetDbSet(operationsHistoryData).Object);
            var newAccountRepository = new NewAccountRepository(context.Object, mapper);
            _service = new NewAccountService(newAccountRepository);
        }

        [Fact]
        public async Task AddCustomer_New_ReturnTrueAsync()
        {
            //Arrange
            var customerModel = new CustomerModel()
            {
                FirstName = "Rachelly",
                LastName = "Lamberger",
                Email = "rachelly@gmail.com",
                Password = "rachelly@brixbank",
            };
            var emailVerificationModel = new EmailVerificationModel()
            {
                VerificationCode = 1234,
                Email = "rachelly@gmail.com",
            };
            var verificationHelperModel = new VerificationHelperModel()
            {
                Customer = customerModel,
                EmailVerification = emailVerificationModel
            };

            //Act
            var result = await _service.AddCustomer(verificationHelperModel);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddCustomer_Old_ReturnFalseAsync()
        {
            //Arrange
            var customerModel = new CustomerModel()
            {
                FirstName = "Rachelly",
                LastName = "Lamberger",
                Email = "renamarkiewitz@gmail.com",
                Password = "rachelly@brixbank",
            };
            var emailVerificationModel = new EmailVerificationModel()
            {
                VerificationCode = 1234,
                Email = "renamarkiewitz@gmail.com",
            };
            var verificationHelperModel = new VerificationHelperModel()
            {
                Customer = customerModel,
                EmailVerification = emailVerificationModel
            };

            //Act
            var result = await _service.AddCustomer(verificationHelperModel);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetEmail_New_ReturnsVoid()
        {
            //Arrange
            var email = "renamarkiewitz@gmail.com";

            //Act
            try
            {
                _service.GetEmail(email);
            }
            catch(Exception e)
            {
                Assert.NotEqual("Exception of type was thrown", e.Message);
            }
        }
    }
}
