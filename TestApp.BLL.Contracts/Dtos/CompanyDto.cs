namespace TestApp.BLL.Contracts.Dtos;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public ICollection<UserDto> Users { get; set; }
}

