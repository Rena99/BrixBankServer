using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Transaction.Services.Interfaces;
using Transaction.WebApi.DTO;

namespace Transaction.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionDetailsController : ControllerBase
    {
        private readonly ITransactionDetailsService _service;
        private readonly IMapper _mapper;

        public TransactionDetailsController(ITransactionDetailsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<TransactionDTO> GetTransaction(Guid id)
        {
            try
            {
                return _mapper.Map<TransactionDTO>(await _service.GetTransaction(id));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
