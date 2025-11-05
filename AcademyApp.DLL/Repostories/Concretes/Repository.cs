using System.Linq.Expressions;
using AcademyApp.Core.Models;
using AcademyApp.DLL.Data;
using AcademyApp.DLL.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.DLL.Repostories.Concretes;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AcademyDbContext _context;

    public Repository(AcademyDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table { get; set; }

    public T Get(int id)
    {
        return Table.Find(id);
    }

    public T Get(int id,bool isTracking = false, params string[] includes)
    {
       var query = Table.AsQueryable();
       if(!isTracking)
           query = query.AsNoTracking();
       foreach (var include in includes)
           query = query.Include(include);
       return query.FirstOrDefault(e => e.Id == id);
    }

    public IQueryable<T> GetAll()
    {
        return Table.AsQueryable();
    }

    public IQueryable<T> GetAll(bool tracking = false, int page = 0, int take = 10, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        foreach (var include in includes)
            query = query.Include(include);
        return query.Skip((page-1)*take).Take(take);
    }

    public void Add(T entity)
    {
       Table.Add(entity);
       SaveChanges();
    }

    public void Update(T entity)
    {
        Table.Update(entity);
        SaveChanges();
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public bool IsExists(Expression<Func<T, bool>> predicate)
    {
        return Table.Any(predicate);
    }

    public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.AnyAsync(predicate);
    }

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
    {
        return Table.Where(filter);
    }
}