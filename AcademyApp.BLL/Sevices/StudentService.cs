using AcademyApp.BLL.Interfaces;
using AcademyApp.Core.Models;
using AcademyApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.BLL.Sevices;

public class StudentService : IStudentService
{
    private readonly AcademyDbContext _academyDbContext;
    
    public StudentService(AcademyDbContext academyDbContext)
    {
        _academyDbContext = academyDbContext;
    }

    public void AddStudent(Student student)
    {
        if (_academyDbContext.Students.Any(s => s.Email.ToLower() == student.Email.ToLower()))
        {
            throw new Exception("Student with the same email already exists.");
        }
        
        _academyDbContext.Students.Add(student);
        _academyDbContext.SaveChanges();
    }

    public List<Student> GetAllStudents()
    {
        return _academyDbContext.Students.Include(s => s.Group).ToList();
    }

    public Student GetStudentById(int id)
    {
        var student = _academyDbContext.Students.Include(s => s.Group).FirstOrDefault(s => s.Id == id);
        if (student is null)
        {
            throw new Exception("Student not found.");
        }
        return student;
    }


    public void UpdateStudent(Student student)
    {
        var existingStudent = _academyDbContext.Students.Find(student.Id);
        if (existingStudent is null)
        {
            throw new Exception("Student not found.");
        }

        if (_academyDbContext.Students.Any(s => s.Email.ToLower() == student.Email.ToLower() && s.Id != student.Id))
        {
            throw new Exception("Student with the same email already exists.");
        }

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Email = student.Email;
        existingStudent.DateOfBirth = student.DateOfBirth;
        existingStudent.GroupId = student.GroupId;
        
        _academyDbContext.SaveChanges();
    }

    public void DeleteStudent(int id)
    {
        var student = _academyDbContext.Students.Find(id);
        if (student is null)
        {
            throw new Exception("Student not found.");
        }

        _academyDbContext.Students.Remove(student);
        _academyDbContext.SaveChanges();
    }

    public async Task AddStudentAsync(Student student)
    {
        if (await _academyDbContext.Students.AnyAsync(s => s.Email.ToLower() == student.Email.ToLower()))
        {
            throw new Exception("Student with the same email already exists.");
        }
        
        await _academyDbContext.Students.AddAsync(student);
        await _academyDbContext.SaveChangesAsync();
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _academyDbContext.Students.Include(s => s.Group).ToListAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        var student = await _academyDbContext.Students.Include(s => s.Group).FirstOrDefaultAsync(s => s.Id == id);
        if (student is null)
        {
            throw new Exception("Student not found.");
        }
        return student;
    }


    public async Task UpdateStudentAsync(Student student)
    {
        var existingStudent = await _academyDbContext.Students.FindAsync(student.Id);
        if (existingStudent is null)
        {
            throw new Exception("Student not found.");
        }

        if (await _academyDbContext.Students.AnyAsync(s => s.Email.ToLower() == student.Email.ToLower() && s.Id != student.Id))
        {
            throw new Exception("Student with the same email already exists.");
        }

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Email = student.Email;
        existingStudent.DateOfBirth = student.DateOfBirth;
        existingStudent.GroupId = student.GroupId;
        
        await _academyDbContext.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _academyDbContext.Students.FindAsync(id);
        if (student is null)
        {
            throw new Exception("Student not found.");
        }

        _academyDbContext.Students.Remove(student);
        await _academyDbContext.SaveChangesAsync();
    }
}
