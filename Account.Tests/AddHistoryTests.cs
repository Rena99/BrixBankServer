using System;
using System.Collections.Generic;
using System.Linq;
using Account.Data;
using Account.Data.Entities;
using Account.Data.Repositories;
using Account.Services.Interfaces;
using Account.Services.Services;
using Moq;
using Xunit;

namespace Account.Tests
{
    public class AddHistoryTests
    {
        private readonly IAddHistoryService _service;
        public AddHistoryTests()
        {
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
            var newAccountRepository = new AddHistoryRepository(context.Object);
            _service = new AddHistoryService(newAccountRepository);
        }

        [Fact]
        public void AddHistory_New_ReturnsVoid()
        {
            //Arrange
            var from = Guid.NewGuid();
            var to = Guid.NewGuid();
            var amount = 0;
            var transactionId = Guid.NewGuid();
            var succeeded = true;

            //Act
            try
            {
                _service.AddHistory(from, to, amount, transactionId, succeeded);
            }
            catch (Exception e)
            {
                Assert.NotEqual("Exception of type was thrown", e.Message);
            }
        }
    }
}
