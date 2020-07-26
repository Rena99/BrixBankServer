using Account.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly INewAccountService _service;
        private readonly IMapper _mapper;

        public EmailVerificationController(INewAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public int GetEmail([FromBody] string email)
        {
            return _service.GetEmail(email);
        }
    }
}
