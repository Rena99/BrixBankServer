using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Transaction.Data;
using Transaction.Data.Repositories;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;
using Transaction.Services.Services;
using Xunit;

namespace Transaction.Tests
{
    public class TransactionTests
    {
        private readonly ITransactionService _service;
        public TransactionTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebApi.Mappings.AutoMapper());
            });
            var mapper = config.CreateMapper();
            var transactionsData = new List<Data.Entities.Transaction>
            {
            };
            var transactions = new Mock<DbSet<Data.Entities.Transaction>>();
            transactions.SetSource(transactionsData);
            var context = new Mock<TransactionContext>();
            context.Setup(m => m.Transactions).Returns(transactions.Object);
            var transactionRepository = new TransactionRepository(context.Object, mapper);
            _service = new TransactionService(transactionRepository);
        }

        [Fact]
        public async System.Threading.Tasks.Task AddTransaction_New_ReturnGuid()
        {
            //Arrange
            var transactionModel = new TransactionModel()
            {
                FromAccount = Guid.NewGuid(),
                ToAccount = Guid.NewGuid(),
                Amount = 20000,
            };

            //Act
            var result = await _service.AddTransaction(transactionModel);

            //Assert
            Guid guidResult;
            Assert.True(Guid.TryParse(result.ToString(), out guidResult));
        }
    }
}


