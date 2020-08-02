using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Account.Services.Models;

namespace Account.Services.Interfaces
{
    public interface IOperationRepository
    {
        Task<List<OperationHistoryModel>> GetOperations(int page, int number, Guid accountId);
        Task<List<OperationHistoryModel>> GetFilteredList(DateTime from, DateTime to, int page, int number, Guid accountId);
        Task<List<OperationHistoryModel>> GetSortedList(string sort, int page, int number, Guid accountId);
    }
}
