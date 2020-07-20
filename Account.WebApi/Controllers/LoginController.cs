﻿
using Account.Services.Interfaces;
using Account.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Account.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IAccountInfoService _accountInfoService;
        private readonly IMapper _mapper;

        public LoginController(IMapper mapper, ILoginService loginService, IAccountInfoService accountInfoService)
        {
            _loginService = loginService;
            _mapper = mapper;
            _accountInfoService = accountInfoService;
        }

        [HttpGet]
        [Route("{action}")]
        public async Task<IActionResult> GetLogin([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                return Ok(await _loginService.Login(email, password));
            }
            catch
            {
                return Unauthorized();
            }
            
        }

        [HttpGet]
        [Route("{action}")]
        public async Task<IActionResult> GetAccount([FromQuery] string CustomerId)
        {
            try
            {
                var account = await _accountInfoService.GetAccount(new Guid(CustomerId));
                return Ok(_mapper.Map<AccountDTO>(account));
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
    }
}
