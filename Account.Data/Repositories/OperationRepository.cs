using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Data.Entities;
using Account.Services.Interfaces;
using Account.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Account.Data.Repositories
{
    public class OperationRepository:IOperationRepository
    {
        private readonly AccountContext _context;
        private readonly IMapper _mapper;

        public OperationRepository(AccountContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<OperationHistoryModel>> GetOperations(int page, int number, Guid accountId)
        {
            List<OperationHistory> operationHistories = await _context.OperationsHistory
                .Where(p => p.AccountId == accountId)
                .Skip(page * number).Take(number).ToListAsync();
            return mapList(operationHistories);
        }

        private List<OperationHistoryModel> mapList(List<OperationHistory> operationHistories)
        {
            List<OperationHistoryModel> historyModels = new List<OperationHistoryModel>();
            foreach (var item in operationHistories)
            {
                historyModels.Add(_mapper.Map<OperationHistoryModel>(item));
            }
            return historyModels;
        }

        public async Task<List<OperationHistoryModel>> GetFilteredList(DateTime from, DateTime to, int page, int number, Guid accountId)
        {
            List<OperationHistory> operationHistories = await _context.OperationsHistory
                .Where(p => p.OperationTime.CompareTo(from) >= 0 && p.OperationTime.CompareTo(to) < 0 && p.AccountId == accountId)
                .Skip(page * number).Take(number).ToListAsync();
            return mapList(operationHistories);
        }

        public async Task<List<OperationHistoryModel>> GetSortedList(string sort, int page, int number, Guid accountId)
        {
            List<OperationHistory> operationHistories = new List<OperationHistory>();
            switch (sort)
            {
                case "date":
                    operationHistories = await _context.OperationsHistory
                               .Where(p => p.AccountId == accountId)
                               .OrderByDescending(p => p.OperationTime)
                               .Skip(page * number).Take(number).ToListAsync();
                    break;
                case "amount": 
                    operationHistories = await _context.OperationsHistory
                               .Where(p => p.AccountId == accountId)
                               .OrderBy(p => p.TransactionAmount)
                               .Skip(page * number).Take(number).ToListAsync();
                    break;
                case "balance":
                        operationHistories = await _context.OperationsHistory
                                       .Where(p => p.AccountId == accountId)
                                       .OrderBy(p => p.Balance)
                                       .Skip(page * number).Take(number).ToListAsync();
                    break;

                default:
                    operationHistories = await _context.OperationsHistory
                           .Where(p => p.AccountId == accountId)
                          .OrderBy(p => p.Debit)
                          .Skip(page * number).Take(number).ToListAsync();
                    break;
            }
            return mapList(operationHistories);
        }
    }
}
