using AcademyApp.BLL.Dtos;
using AcademyApp.BLL.Interfaces;
using AcademyApp.BLL.Profiles;
using AcademyApp.Core.Models;
using AcademyApp.DLL.Data;
using AcademyApp.DLL.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.BLL.Sevices;

public class GroupService : IGroupService
{
    private readonly IRepository<Group> _repo;
    public GroupService(IRepository<Group> repo)
    {
        _repo = repo;
    }

    public void AddGroup(GroupCreateDto groupCreateDto)
    {
        if (_repo.IsExists(g => g.Name.ToLower() == groupCreateDto.Name.ToLower()))
        {
            throw new Exception("Group with the same name already exists.");
            
        }
        _repo.Add(MapProfile.GroupCreateDtoToGroup(groupCreateDto));
        _repo.SaveChanges();
    }
    
    public async Task AddGroupAsync(GroupCreateDto groupCreateDto)
    {
        if (await _repo.IsExistsAsync(g => g.Name.ToLower() == groupCreateDto.Name.ToLower()))
        {
            throw new Exception("Group with the same name already exists.");
            
        }
        await _repo.AddAsync(MapProfile.GroupCreateDtoToGroup(groupCreateDto));
        await _repo.SaveChangesAsync();
    }
    
    public List<Group> GetAllGroups() =>
         _repo.GetAll().ToList();
    
    public async Task<List<Group>> GetAllGroupsAsync() =>
        await _repo.GetAll().ToListAsync();
    
    public GroupReturnDto GetGroupById(int id)
    {
        var group = _repo.Get(id);
        if (group is null)
        {
            throw new Exception("Group not found.");
        }

       
        return MapProfile.GroupToGroupReturnDto(group);
    }
    
    public async Task<GroupReturnDto> GetGroupByIdAsync(int id)
    {
        var group =  await _repo.GetAsync(id);
        if (group is null)
        {
            throw new Exception("Group not found.");
        }
      
        return MapProfile.GroupToGroupReturnDto(group);
    }
    
    public List<Group> GetGroupsByName(string name) =>
        _repo
            .GetAll(g => g.Name.ToLower().Contains(name.ToLower()) || g.Description.ToLower().Contains(name.ToLower()))
            .ToList();
    
    public async Task<List<Group>> GetGroupsByNameAsync(string name) =>
        await _repo.GetAll(g => g.Name.ToLower().Contains(name.ToLower()) || g.Description.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    
    public List<Group> GetGroupsByLimit(int limit) =>
        _repo.GetAll(g => g.Limit == limit)
            .ToList();
    
    public async Task<List<Group>> GetGroupsByLimitAsync(int limit) =>
        await _repo.GetAll(g => g.Limit == limit)
            .ToListAsync(); 
    
    public List<Group> GetGroupsByLimit(int minLimit,int maxLimit) =>
        _repo.GetAll(g => g.Limit >= minLimit && g.Limit <= maxLimit)
            .ToList();
        
    public async Task<List<Group>> GetGroupsByLimitAsync(int minLimit,int maxLimit) =>
        await _repo
            .GetAll(g => g.Limit >= minLimit && g.Limit <= maxLimit)
            .ToListAsync();
    
    public void UpdateGroup(Group group)
    {
        var existingGroup = _repo.Get(group.Id);
        if (existingGroup is null)
        {
            throw new Exception("Group not found.");
        }
        if(_repo.IsExists(g => g.Name.ToLower() == group.Name.ToLower() && g.Id != group.Id))
        {
            throw new Exception("Group with the same name already exists.");
        }
        existingGroup.Name = group.Name;
        existingGroup.Description = group.Description;
        existingGroup.Limit = group.Limit;
        _repo.SaveChanges();
    }
    
    public async Task UpdateGroupAsync(Group group)
    {
        var existingGroup = await _repo.GetAsync(group.Id);
        if (existingGroup is null)
        {
            throw new Exception("Group not found.");
        }
        if(await _repo.IsExistsAsync(g => g.Name.ToLower() == group.Name.ToLower() && g.Id != group.Id))
        {
            throw new Exception("Group with the same name already exists.");
        }
        existingGroup.Name = group.Name;
        existingGroup.Description = group.Description;
        existingGroup.Limit = group.Limit;
        await  _repo.SaveChangesAsync();
    }
    
    public List<Group> GetAllGroups(bool isTracking, int page, int take, params string[] includes)
    => _repo.GetAll(isTracking, page, take, includes).ToList();
}

