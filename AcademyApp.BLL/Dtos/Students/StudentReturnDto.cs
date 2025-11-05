namespace AcademyApp.BLL.Dtos;

public class StudentReturnDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; } = null!;
}