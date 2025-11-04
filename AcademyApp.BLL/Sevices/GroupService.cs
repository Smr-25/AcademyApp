using AcademyApp.BLL.Dtos;
using AcademyApp.BLL.Interfaces;
using AcademyApp.Core.Models;
using AcademyApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.BLL.Sevices;

public class GroupService : IGroupService
{
    private readonly AcademyDbContext _academyDbContext;
    
    public GroupService(AcademyDbContext academyDbContext)
    {
        _academyDbContext = academyDbContext;
    }

    public void AddGroup(GroupCreateDto groupCreateDto)
    {
        if (!_academyDbContext.Groups.Any(g => g.Name.ToLower() == groupCreateDto.Name.ToLower()))
        {
            throw new Exception("Group with the same name already exists.");
            
        }
        _academyDbContext.Add(MapProfile.GroupCreateDtoToGroup(groupCreateDto));
        _academyDbContext.SaveChanges();
    }
    
    public async Task AddGroupAsync(GroupCreateDto groupCreateDto)
    {
        if (await _academyDbContext.Groups.AnyAsync(g => g.Name.ToLower() == groupCreateDto.Name.ToLower()))
        {
            throw new Exception("Group with the same name already exists.");
            
        }
        await _academyDbContext.AddAsync(MapProfile.GroupCreateDtoToGroup(groupCreateDto));
        await _academyDbContext.SaveChangesAsync();
    }
    
    public List<Group> GetAllGroups() =>
         _academyDbContext.Groups.ToList();
    
    public async Task<List<Group>> GetAllGroupsAsync() =>
        await _academyDbContext.Groups.ToListAsync();
    
    public GroupReturnDto GetGroupById(int id)
    {
        var group = _academyDbContext.Groups.Find(id);
        if (group is null)
        {
            throw new Exception("Group not found.");
        }

       
        return MapProfile.GroupToGroupReturnDto(group);
    }
    
    public async Task<GroupReturnDto> GetGroupByIdAsync(int id)
    {
        var group =  await _academyDbContext.Groups.FindAsync(id);
        if (group is null)
        {
            throw new Exception("Group not found.");
        }
      
        return MapProfile.GroupToGroupReturnDto(group);
    }
    
    public List<Group> GetGroupsByName(string name) =>
        _academyDbContext.Groups
            .Where(g => g.Name.ToLower().Contains(name.ToLower()) || g.Description.ToLower().Contains(name.ToLower()))
            .ToList();
    
    public async Task<List<Group>> GetGroupsByNameAsync(string name) =>
        await _academyDbContext.Groups
            .Where(g => g.Name.ToLower().Contains(name.ToLower()) || g.Description.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    
    public List<Group> GetGroupsByLimit(int limit) =>
        _academyDbContext.Groups
            .Where(g => g.Limit == limit)
            .ToList();
    
    public async Task<List<Group>> GetGroupsByLimitAsync(int limit) =>
        await _academyDbContext.Groups
            .Where(g => g.Limit == limit)
            .ToListAsync(); 
    
    public List<Group> GetGroupsByLimit(int minLimit,int maxLimit) =>
        _academyDbContext.Groups
            .Where(g => g.Limit >= minLimit && g.Limit <= maxLimit)
            .ToList();
        
    public async Task<List<Group>> GetGroupsByLimitAsync(int minLimit,int maxLimit) =>
        await _academyDbContext.Groups
            .Where(g => g.Limit >= minLimit && g.Limit <= maxLimit)
            .ToListAsync();
    
    public void UpdateGroup(Group group)
    {
        var existingGroup = _academyDbContext.Groups.Find(group.Id);
        if (existingGroup is null)
        {
            throw new Exception("Group not found.");
        }
        if(_academyDbContext.Groups.Any(g => g.Name.ToLower() == group.Name.ToLower() && g.Id != group.Id))
        {
            throw new Exception("Group with the same name already exists.");
        }
        existingGroup.Name = group.Name;
        existingGroup.Description = group.Description;
        existingGroup.Limit = group.Limit;
        _academyDbContext.SaveChanges();
    }
    
    public async Task UpdateGroupAsync(Group group)
    {
        var existingGroup = await _academyDbContext.Groups.FindAsync(group.Id);
        if (existingGroup is null)
        {
            throw new Exception("Group not found.");
        }
        if(await _academyDbContext.Groups.AnyAsync(g => g.Name.ToLower() == group.Name.ToLower() && g.Id != group.Id))
        {
            throw new Exception("Group with the same name already exists.");
        }
        existingGroup.Name = group.Name;
        existingGroup.Description = group.Description;
        existingGroup.Limit = group.Limit;
        await  _academyDbContext.SaveChangesAsync();
    }
}

