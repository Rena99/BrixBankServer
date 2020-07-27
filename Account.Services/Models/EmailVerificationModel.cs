using System;

namespace Account.Services.Models
{
    public class EmailVerificationModel
    {
        public string Email { get; set; }
        public int VerificationCode { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
