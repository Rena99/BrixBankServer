using System;
using System.Collections.Generic;
using Account.Services.Models;

namespace Account.Services.Interfaces
{
    public interface IOperationService
    {
        List<OperationHistoryModel> GetOperations(int page, int number, Guid accountId);
        List<OperationHistoryModel> GetSortedList(string sort, int page, int number, Guid accountId);
        List<OperationHistoryModel> GetFilteredList(DateTime from, DateTime to, int page, int number, Guid accountId);
    }
}
