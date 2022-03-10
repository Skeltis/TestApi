using System.ComponentModel.DataAnnotations;

namespace TestApp.Api.Contracts.v1.Model
{
    public class CreateUserUnderCompanyRequest
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
    }
}
