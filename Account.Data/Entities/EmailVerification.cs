using System;

namespace Account.Data.Entities
{
    public class EmailVerification
    {
        public Guid EmailVerificationId { get; set; }
        public string Email { get; set; }
        public int VerificationCode { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
