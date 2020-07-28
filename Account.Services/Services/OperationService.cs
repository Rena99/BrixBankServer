using System;
using System.Collections.Generic;
using Account.Services.Interfaces;
using Account.Services.Models;

namespace Account.Services.Services
{
    public class OperationService:IOperationService
    {
        private readonly IOperationRepository _repository;

        public OperationService(IOperationRepository repository)
        {
            _repository = repository;
        }

        public List<OperationHistoryModel> GetFilteredList(DateTime from, DateTime to, int page, int number, Guid accountId)
        {
            return _repository.GetFilteredList(from, to, page, number, accountId);
        }

        public List<OperationHistoryModel> GetOperations(int page, int number, Guid accountId)
        {
            return _repository.GetOperations(page, number, accountId);
        }

        public List<OperationHistoryModel> GetSortedList(string sort, int page, int number, Guid accountId)
        {
            return _repository.GetSortedList(sort, page, number, accountId);
        }
    }
}
