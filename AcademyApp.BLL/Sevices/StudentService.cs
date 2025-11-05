using AcademyApp.BLL.Interfaces;
using AcademyApp.Core.Models;
using AcademyApp.DLL.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;
using AcademyApp.BLL.Dtos;
using AcademyApp.BLL.Profiles;
using System.Text.RegularExpressions;
using Group = AcademyApp.Core.Models.Group;

namespace AcademyApp.BLL.Sevices;

public class StudentService : IStudentService
{
    private readonly IRepository<Student> _studentRepo;
    private readonly IRepository<Group> _groupRepo;
    private const string EmailDomainPattern = "^[a-zA-Z0-9._%+-]+@code\\.edu\\.az$";

    public StudentService(IRepository<Student> studentRepo, IRepository<Group> groupRepo)
    {
        _studentRepo = studentRepo;
        _groupRepo = groupRepo;
    }

    public void AddStudent(StudentCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) ||
            !Regex.IsMatch(dto.Email, EmailDomainPattern, RegexOptions.IgnoreCase))
        {
            throw new Exception("Email must be a valid address in the 'code.edu.az' domain.");
        }

        if (_studentRepo.IsExists(s => s.Email.ToLower() == dto.Email.ToLower()))
        {
            throw new Exception("Student with the same email already exists.");
        }

        if (dto.GroupId != 0)
        {
            var group = _groupRepo.Get(dto.GroupId);
            if (group is null)
                throw new Exception("Target group not found.");

            var currentCount = _studentRepo.GetAll(s => s.GroupId == dto.GroupId).Count();
            if (currentCount >= group.Limit)
                throw new Exception("Target group is full.");
        }

        _studentRepo.Add(MapProfile.StudentCreateDtoToStudent(dto));
        _studentRepo.SaveChanges();
    }

    public async Task AddStudentAsync(StudentCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) ||
            !Regex.IsMatch(dto.Email, EmailDomainPattern, RegexOptions.IgnoreCase))
        {
            throw new Exception("Email must be a valid address in the 'code.edu.az' domain.");
        }

        if (await _studentRepo.IsExistsAsync(s => s.Email.ToLower() == dto.Email.ToLower()))
        {
            throw new Exception("Student with the same email already exists.");
        }

        if (dto.GroupId != 0)
        {
            var group = await _groupRepo.GetAsync(dto.GroupId);
            if (group is null)
                throw new Exception("Target group not found.");

            var currentCount = await _studentRepo.GetAll(s => s.GroupId == dto.GroupId).CountAsync();
            if (currentCount >= group.Limit)
                throw new Exception("Target group is full.");
        }

        await _studentRepo.AddAsync(MapProfile.StudentCreateDtoToStudent(dto));
        await _studentRepo.SaveChangesAsync();
    }

    public List<Student> GetAllStudents() =>
        _studentRepo.GetAll(tracking: false, page: 0, take: int.MaxValue, includes: "Group").ToList();

    public async Task<List<Student>> GetAllStudentsAsync() =>
        await _studentRepo.GetAll(tracking: false, page: 0, take: int.MaxValue, includes: "Group").ToListAsync();

    public StudentReturnDto GetStudentById(int id)
    {
        var student = _studentRepo.Get(id, isTracking: false, includes: "Group");
        if (student is null)
        {
            throw new Exception("Student not found.");
        }

        return MapProfile.StudentToStudentReturnDto(student);
    }

    public async Task<StudentReturnDto> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepo.GetAsync(id);
        if (student is null)
        {
            throw new Exception("Student not found.");
        }

        var studentWithGroup = _studentRepo.Get(id, isTracking: false, includes: "Group");
        return MapProfile.StudentToStudentReturnDto(studentWithGroup);
    }

    public void UpdateStudent(StudentUpdateDto dto)
    {
        var existingStudent = _studentRepo.Get(dto.Id);
        if (existingStudent is null)
        {
            throw new Exception("Student not found.");
        }

        if (string.IsNullOrWhiteSpace(dto.Email) ||
            !Regex.IsMatch(dto.Email, EmailDomainPattern, RegexOptions.IgnoreCase))
        {
            throw new Exception("Email must be a valid address in the 'code.edu.az' domain.");
        }

        if (_studentRepo.IsExists(s => s.Email.ToLower() == dto.Email.ToLower() && s.Id != dto.Id))
        {
            throw new Exception("Student with the same email already exists.");
        }

        if (dto.GroupId != 0 && dto.GroupId != existingStudent.GroupId)
        {
            var targetGroup = _groupRepo.Get(dto.GroupId);
            if (targetGroup is null)
                throw new Exception("Target group not found.");

            var currentCount = _studentRepo.GetAll(s => s.GroupId == dto.GroupId).Count();
            if (currentCount >= targetGroup.Limit)
                throw new Exception("Target group is full.");
        }

        existingStudent.FirstName = dto.FirstName;
        existingStudent.LastName = dto.LastName;
        existingStudent.Email = dto.Email;
        existingStudent.DateOfBirth = dto.DateOfBirth;
        existingStudent.GroupId = dto.GroupId;

        _studentRepo.SaveChanges();
    }

    public async Task UpdateStudentAsync(StudentUpdateDto dto)
    {
        var existingStudent = await _studentRepo.GetAsync(dto.Id);
        if (existingStudent is null)
        {
            throw new Exception("Student not found.");
        }

        if (string.IsNullOrWhiteSpace(dto.Email) ||
            !Regex.IsMatch(dto.Email, EmailDomainPattern, RegexOptions.IgnoreCase))
        {
            throw new Exception("Email must be a valid address in the 'code.edu.az' domain.");
        }

        if (await _studentRepo.IsExistsAsync(s => s.Email.ToLower() == dto.Email.ToLower() && s.Id != dto.Id))
        {
            throw new Exception("Student with the same email already exists.");
        }

        if (dto.GroupId != 0 && dto.GroupId != existingStudent.GroupId)
        {
            var targetGroup = await _groupRepo.GetAsync(dto.GroupId);
            if (targetGroup is null)
                throw new Exception("Target group not found.");

            var currentCount = await _studentRepo.GetAll(s => s.GroupId == dto.GroupId).CountAsync();
            if (currentCount >= targetGroup.Limit)
                throw new Exception("Target group is full.");
        }

        existingStudent.FirstName = dto.FirstName;
        existingStudent.LastName = dto.LastName;
        existingStudent.Email = dto.Email;
        existingStudent.DateOfBirth = dto.DateOfBirth;
        existingStudent.GroupId = dto.GroupId;

        await _studentRepo.SaveChangesAsync();
    }

    public void DeleteStudent(int id)
    {
        var student = _studentRepo.Get(id);
        if (student is null)
        {
            throw new Exception("Student not found.");
        }

        _studentRepo.Delete(student);
        _studentRepo.SaveChanges();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _studentRepo.GetAsync(id);
        if (student is null)
        {
            throw new Exception("Student not found.");
        }

        _studentRepo.Delete(student);
        await _studentRepo.SaveChangesAsync();
    }
}