using Account.Services.Interfaces;
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
        public void GetEmail([FromBody] string email)
        {
            _service.GetEmail(email);
        }
    }
}
