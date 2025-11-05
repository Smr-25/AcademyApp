using System.Linq.Expressions;
using AcademyApp.Core.Models;

namespace AcademyApp.DLL.Repostories.Interfaces;

public interface IRepository<T>where T : BaseEntity
{
    T Get(int id);
    T Get(int id,bool isTracking = false, params string[] includes);
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(bool tracking = false,int page=0,int take=10,params string[] includes);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void SaveChanges();
    
    bool IsExists(Expression<Func<T,bool>>predicate);
    
    Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate);
    
    Task AddAsync(T entity);
    Task SaveChangesAsync();

    Task<T> GetAsync(int id);
    
    IQueryable<T> GetAll(Expression<Func<T,bool>> filter);
}