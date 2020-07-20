using System;
using Account.Services.Interfaces;
using Account.Services.Models;
using Account.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewAccountController : ControllerBase
    {
        private readonly INewAccountService _service;
        private readonly IMapper _mapper;
        public NewAccountController(INewAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public bool PostCustomer([FromBody] CustomerDTO customer)
        {
            try
            {
               return _service.AddCustomer(_mapper.Map<CustomerModel>(customer));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
