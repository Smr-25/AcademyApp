namespace AcademyApp.Core.Models;

public class Group
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
    
    public int Limit { get; set; }
    public List<Student> Students { get; set; }

    public override string ToString()
    {
        return $"Group Id: {Id}, Name: {Name}, Description: {Description}, Limit: {Limit}";
    }
}