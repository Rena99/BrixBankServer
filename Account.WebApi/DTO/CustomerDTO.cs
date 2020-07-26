namespace Account.WebApi.DTO
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int VerificationCode { get; set; }
    }
}
