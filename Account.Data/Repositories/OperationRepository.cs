using System;
using System.Collections.Generic;
using System.Linq;
using Account.Data.Entities;
using Account.Services.Interfaces;
using Account.Services.Models;
using AutoMapper;

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

        public List<OperationHistoryModel> GetOperations(int page, int number, Guid accountId)
        {
            List<OperationHistory> operationHistories = _context.OperationsHistory
                .Where(p => p.AccountId == accountId)
                .Skip(page * number).Take(number).ToList();
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

        public List<OperationHistoryModel> GetFilteredList(DateTime from, DateTime to, int page, int number, Guid accountId)
        {
            List<OperationHistory> operationHistories = _context.OperationsHistory
                .Where(p => p.OperationTime.CompareTo(from) >= 0 && p.OperationTime.CompareTo(to) < 0 && p.AccountId == accountId)
                .Skip(page * number).Take(number).ToList();
            return mapList(operationHistories);
        }

        public List<OperationHistoryModel> GetSortedList(string sort, int page, int number, Guid accountId)
        {
            //use dictionary and predicate to sort plus add more than one sorting option
            List<OperationHistory> operationHistories = new List<OperationHistory>();
            if (sort == "date")
            {
               operationHistories = _context.OperationsHistory
                               .Where(p=>p.AccountId== accountId)
                               .OrderByDescending(p => p.OperationTime)
                               .Skip(page * number).Take(number).ToList();
            }
            else if (sort == "amount")
            {
                operationHistories = _context.OperationsHistory
                               .Where(p => p.AccountId == accountId)
                               .OrderBy(p => p.TransactionAmount)
                               .Skip(page * number).Take(number).ToList();
            }
            else if (sort == "balance")
            {
                operationHistories = _context.OperationsHistory
                               .Where(p => p.AccountId == accountId)
                               .OrderBy(p => p.Balance)
                               .Skip(page * number).Take(number).ToList();
            }
            else if (sort == "debit")
            {
                operationHistories = _context.OperationsHistory
                                .Where(p => p.AccountId == accountId)
                               .OrderBy(p => p.Debit)
                               .Skip(page * number).Take(number).ToList();
            }
            return mapList(operationHistories);
        }
    }
}
