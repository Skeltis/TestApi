using System.ComponentModel.DataAnnotations;

namespace TestApp.Api.Contracts.v1.Model
{
    public class CreateUserUnderCompanyRequest : CreateUserRequest
    {
        [Required]
        public Guid CompanyId { get; set; }
    }
}
