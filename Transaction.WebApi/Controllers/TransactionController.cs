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
        public async Task<bool> PostTransaction(TransactionDTO transaction)
        {
            try
            {
                Guid guid = await _service.AddTransaction(_mapper.Map<TransactionModel>(transaction));
                TransactionAdded transactionAdded = _mapper.Map<TransactionAdded>(transaction);
                transactionAdded.TransactionId = guid;
                transactionAdded.MessageId = Guid.NewGuid().ToString();
                await _messageSession.Publish(transactionAdded);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
