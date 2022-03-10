namespace TestApp.Api.Contracts.v1.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid CompanyId { get; set; }
    }
}
