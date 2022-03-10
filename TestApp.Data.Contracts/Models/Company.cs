using TestApp.Data.Contracts.Interfaces;

namespace TestApp.Data.Contracts.Models
{
    public class Company : IHasGuidId
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
