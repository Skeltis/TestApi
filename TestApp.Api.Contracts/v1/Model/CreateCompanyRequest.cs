using System.ComponentModel.DataAnnotations;

namespace TestApp.Api.Contracts.v1.Model
{
    public class CreateCompanyRequest
    {
        [Required]
        public string CompanyName { get; set; }
        public CreateUserRequest? User { get; set; }
    }
}
