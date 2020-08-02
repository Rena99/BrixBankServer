//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Account.Data;
//using Account.Data.Entities;
//using Account.Data.Repositories;
//using Account.Services.Interfaces;
//using Account.Services.Services;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Xunit;

//namespace Account.Tests
//{
//    public class LoginTests
//    {
//        private readonly ILoginService _service;
//        public LoginTests()
//        {
//            var customersData = new List<Customer>
//            {
//                new Customer { FirstName = "Rena", LastName="Markiewitz", Email="renamarkiewitz@gmail.com", CustomerId=new Guid("E7775225-9C97-4460-D971-08D82BD1D7C5"), Password="FBlFNDkEHjfkEgs/wa+qhbAPFgCFzuqz6qCOzv2fyr8=", Salt="6bFInUvH1tH+XhXMPGFHN7In8NtFcK/bLVmiaQmjl80=" },
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
//            var newAccountRepository = new LoginRepository(context.Object);
//            _service = new LoginService(newAccountRepository);
//        }

//        [Fact]
//        public void Login_Exists_ReturnsGuid()
//        {
//            //Arrange
//            string email = "renamarkiewitz@gmail.com";
//            string password = "rena@brixbank";

//            //Act
//            var result = _service.Login(email, password).Result;

//            //Assert
//            Assert.IsType<Guid>(result);
//            Assert.Equal("9CFE55DF-65E2-4204-BAD5-08D82BD21687", result.ToString());
//        }
//    }
//}
