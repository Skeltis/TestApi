namespace TestApp.BLL.Contracts.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public CompanyDto Company { get; set; }
}

