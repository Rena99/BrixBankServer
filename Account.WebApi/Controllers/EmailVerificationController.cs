﻿using Account.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly INewAccountService _service;

        public EmailVerificationController(INewAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        public int GetEmail([FromBody] string email)
        {
            return _service.GetEmail(email);
        }
    }
}
