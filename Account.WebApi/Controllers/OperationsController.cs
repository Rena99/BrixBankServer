using System;
using System.Collections.Generic;
using Account.Services.Interfaces;
using Account.Services.Models;
using Account.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _service;
        private readonly IMapper _mapper;

        public OperationsController(IMapper mapper, IOperationService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{page}/{number}")]
        public List<OperationHistoryDTO> GetOperations(int page, int number, [FromQuery] Guid accountId)
        {
            try
            {
                List<OperationHistoryModel> historyModels = _service.GetOperations(page, number, accountId);
                return mapList(historyModels);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<OperationHistoryDTO> mapList(List<OperationHistoryModel> historyModels)
        {
            List<OperationHistoryDTO> operationHistories = new List<OperationHistoryDTO>();
            foreach (var item in historyModels)
            {
                operationHistories.Add(_mapper.Map<OperationHistoryDTO>(item));
            }
            return operationHistories;
        }

        [HttpGet("sort/{page}/{number}")]
        public List<OperationHistoryDTO> GetSortedList([FromQuery] Guid accountId, [FromQuery]string sort, int page, int number)
        {
            try
            {
                List<OperationHistoryModel> historyModels = _service.GetSortedList(sort, page, number, accountId);
                return mapList(historyModels);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("filter/{page}/{number}")]
        public List<OperationHistoryDTO> GetFilteredList([FromQuery] Guid accountId, [FromQuery]DateTime from, [FromQuery] DateTime to, int page, int number)
        {
            try
            {
                List<OperationHistoryModel> historyModels = _service.GetFilteredList(from, to, page, number, accountId);
                return mapList(historyModels);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
