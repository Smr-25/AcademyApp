using AcademyApp.Core.Models;

namespace AcademyApp.BLL.Interfaces;

public interface IStudentService
{
    void AddStudent(Student student);
    List<Student> GetAllStudents();
    Student GetStudentById(int id);
    void UpdateStudent(Student student);
    void DeleteStudent(int id);

    Task AddStudentAsync(Student student);
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(int id);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
}
