using System.ComponentModel.DataAnnotations;

namespace Account.WebApi.DTO
{
    public class CustomerDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Range(1000, 9999)]
        public int VerificationCode { get; set; }
    }
}
