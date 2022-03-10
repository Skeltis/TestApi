using System.ComponentModel.DataAnnotations;

namespace TestApp.Api.Contracts.v1.Model
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
