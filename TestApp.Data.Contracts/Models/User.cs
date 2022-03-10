using TestApp.Data.Contracts.Interfaces;

namespace TestApp.Data.Contracts.Models
{
    public class User : IHasGuidId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
