﻿using AutoMapper;
using Transaction.Services.Models;
using Transaction.WebApi.DTO;
using Messages;

namespace Transaction.WebApi.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<TransactionDTO, TransactionModel>();
            CreateMap<TransactionDTO, TransactionReceived>();
            CreateMap<Data.Entities.Transaction, TransactionModel>();
            CreateMap<TransactionModel, TransactionDTO>();
            CreateMap<TransactionModel, Data.Entities.Transaction>();
        }
    }
}
