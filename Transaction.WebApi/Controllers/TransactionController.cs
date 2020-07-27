using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;
using Transaction.WebApi.DTO;
using NServiceBus;
using System;
using Messages;

namespace Transaction.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;

        public TransactionController(ITransactionService service, IMapper mapper, IMessageSession messageSession)
        {
            _service = service;
            _mapper = mapper;
            _messageSession = messageSession;
        }

        [HttpPost]
        public async Task<bool> PostTransaction([FromBody]TransactionDTO transaction)
        {
            try
            {
                Guid guid = await _service.AddTransaction(_mapper.Map<TransactionModel>(transaction));
                TransactionReceived transactionReceived = _mapper.Map<TransactionReceived>(transaction);
                transactionReceived.TransactionId = guid;
                transactionReceived.MessageId = Guid.NewGuid().ToString();
                await _messageSession.Publish(transactionReceived);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
