﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var transactionRepository = new TransactionRepository(context.Object, mapper);
            _service = new TransactionService(transactionRepository);
        }

        [Fact]
        public void AddTransaction_New_ReturnGuidAsync()
        {
            //Arrange
            var transactionModel = new TransactionModel()
            {
                FromAccount = Guid.NewGuid(),
                ToAccount = Guid.NewGuid(),
                Amount = 20000,
            };

            //Act
            var result = _service.AddTransaction(transactionModel).Result;

            //Assert
            Guid guidResult;
            Assert.True(Guid.TryParse(result.ToString(), out guidResult));
        }
    }
}


