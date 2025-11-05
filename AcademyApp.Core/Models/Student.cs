namespace AcademyApp.Core.Models;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Group Group { get; set; }
    public int GroupId { get; set; }

    public override string ToString()
    {
        return $"Student(Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, DateOfBirth: {DateOfBirth.ToShortDateString()})";
    }
}