using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Transaction.Data;
using Transaction.Data.Repositories;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;
using Transaction.Services.Services;
using Xunit;

namespace Transaction.Tests
{
    public class TransactionDetailsTest
    {
        private readonly ITransactionDetailsService _service;
        public TransactionDetailsTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebApi.Mappings.AutoMapper());
            });
            var mapper = config.CreateMapper();
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
            var transactionRepository = new TransactionDetailsRepository(context.Object, mapper);
            _service = new TransactionDetailsService(transactionRepository);
        }

        [Fact]
        public async Task GetTransaction_Exists_ReturnTransactionModelAsync()
        {
            //Arrange
            var transactionId = Guid.Parse("96DAF25B-F86A-4A76-8E03-91B9C1AA7C6C");

            //Act
            var result = await _service.GetTransaction(transactionId).ConfigureAwait(false);

            //Assert
            Assert.IsType<TransactionModel>(result);
        }

        [Fact]
        public async Task GetTransaction_DoesNotExist_ReturnNullAsync()
        {
            //Arrange
            var transactionId = Guid.NewGuid();

            //Act
            var result = await _service.GetTransaction(transactionId).ConfigureAwait(false);

            //Assert
            Assert.True(result==null);
        }
    }
}

