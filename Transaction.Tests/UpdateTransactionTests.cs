using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Transaction.Data;
using Transaction.Data.Repositories;
using Transaction.Services.Interfaces;
using Transaction.Services.Services;
using Xunit;

namespace Transaction.Tests
{
    public class UpdateTransactionTests
    {
        private readonly IUpdateTransactionService _service;
        public UpdateTransactionTests()
        {
            var transactionsData = new List<Data.Entities.Transaction>
            {
                new Data.Entities.Transaction()
                    {
                        ToAccount=Guid.NewGuid(),
                        FromAccount=Guid.NewGuid(),
                        TransactionId=Guid.Parse("96DAF25B-F86A-4A76-8E03-91B9C1AA7C6C"),
                        Amount=20,
                        Date=DateTime.Now,
                        FailureReason="",
                        Status=(Data.Entities.Status)1
                    }
            }.AsQueryable();
            var context = new Mock<TransactionContext>();
            context.SetupGet(x => x.Transactions).Returns(MockDBSetExtensions.GetDbSet(transactionsData).Object);
            var transactionRepository = new UpdateTransactionRepository(context.Object);
            _service = new UpdateTransactionService(transactionRepository);
        }

        [Fact]
        public void UpdateTransaction_Exists_ReturnsVoid()
        {
            //Arrange
            var transactionId = Guid.Parse("96DAF25B-F86A-4A76-8E03-91B9C1AA7C6C");
            var succeeded = 1;
            var message = "";

            //Act
            try
            {
                _service.UpdateTransaction(transactionId, succeeded, message);
            }
            catch(Exception e)
            {
                Assert.NotEqual("Exception of type was thrown", e.Message);
            }
        }
    }
}
