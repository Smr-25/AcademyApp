using AcademyApp.Core.Models;

namespace AcademyApp.BLL.Interfaces;

public interface IGroupService
{
    void AddGroup(Group group);
    Task AddGroupAsync(Group group);
    List<Group> GetAllGroups();
    Task<List<Group>> GetAllGroupsAsync();
    Group GetGroupById(int id);
    Task<Group> GetGroupByIdAsync(int id);
    List<Group> GetGroupsByName(string name);
    Task<List<Group>> GetGroupsByNameAsync(string name);
    List<Group> GetGroupsByLimit(int limit);
    Task<List<Group>> GetGroupsByLimitAsync(int limit);
    List<Group> GetGroupsByLimit(int minLimit, int maxLimit);
    Task<List<Group>> GetGroupsByLimitAsync(int minLimit, int maxLimit);
    
    void UpdateGroup(Group group);
    
    Task UpdateGroupAsync(Group group);
}