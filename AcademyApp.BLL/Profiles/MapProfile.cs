using AcademyApp.BLL.Dtos;
using AcademyApp.Core.Models;

namespace AcademyApp.BLL.Profiles;

public class MapProfile
{
    public static GroupReturnDto GroupToGroupReturnDto(Group group)
    {
        return new GroupReturnDto
        {
            Name = group.Name,
            Description = group.Description,
            Limit = group.Limit
        };
    }
    
    public static Group GroupCreateDtoToGroup(GroupCreateDto groupCreateDto)
    {
        return new Group
        {
            Name = groupCreateDto.Name,
            Description = groupCreateDto.Description,
            Limit = groupCreateDto.Limit
        };
    }


    public static StudentReturnDto StudentToStudentReturnDto(Student student)
    {
        return new StudentReturnDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            DateOfBirth = student.DateOfBirth,
            GroupId = student.GroupId,
            GroupName = student.Group?.Name ?? ""
        };
    }

    public static Student StudentCreateDtoToStudent(StudentCreateDto dto)
    {
        return new Student
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            GroupId = dto.GroupId
        };
    }
    
    public static Student StudentUpdateDtoToStudent(StudentUpdateDto dto)
    {
        return new Student
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            GroupId = dto.GroupId
        };
    }
}