using AcademyApp.Core.Models;
using AcademyApp.BLL.Dtos;

namespace AcademyApp.BLL.Interfaces;

public interface IStudentService
{
    void AddStudent(StudentCreateDto dto);
    Task AddStudentAsync(StudentCreateDto dto);
    
    List<Student> GetAllStudents();
    Task<List<Student>> GetAllStudentsAsync();
    
    StudentReturnDto GetStudentById(int id);
    Task<StudentReturnDto> GetStudentByIdAsync(int id);
    
    void UpdateStudent(StudentUpdateDto dto);
    Task UpdateStudentAsync(StudentUpdateDto dto);
    
    void DeleteStudent(int id);
    Task DeleteStudentAsync(int id);
    
    
}
