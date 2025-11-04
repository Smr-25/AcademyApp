using System.ComponentModel.DataAnnotations.Schema;

namespace AcademyApp.Core.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Limit { get; set; }
    [NotMapped]
    public string ShortDescription => Description.Length > 15 ? Description.Substring(0, 15) + "..." : Description;
    public List<Student> Students { get; set; }

    public override string ToString()
    {
        return $"Group Id: {Id}, Name: {Name}, ShortDescription: {ShortDescription}, Limit: {Limit}";
    }
}