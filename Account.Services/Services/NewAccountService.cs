using Account.Services.Interfaces;
using Account.Services.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Account.Services.Services
{
    public class NewAccountService:INewAccountService
    {
        private readonly INewAccountRepository _repository;

        public NewAccountService(INewAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddCustomer(VerificationHelperModel customerModel)
        {
            return await _repository.AddCustomer(customerModel);
        }

        public void GetEmail(string email)
        {
            try
            {
                int code= _repository.GetEmail(email);
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["EmailName"]);
                var toAddress = new MailAddress(email, email);
                string fromPassword = ConfigurationManager.AppSettings["Password"];
                string subject = ConfigurationManager.AppSettings["Subject"];
                string body = "In order to successfully join our bank please enter the pin below when prompted.\n"+code+" \nRegards,\nBrix Bank ";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
