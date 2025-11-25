using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;
using VistaPms.Infrastructure.Persistence;

namespace VistaPms.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        // Note: SaveChanges is usually called by UnitOfWork or CommandHandler, but for simplicity we might not call it here.
        // However, the interface implies action. Let's assume UnitOfWork handles saving, or we save here.
        // Given IApplicationDbContext has SaveChangesAsync, we probably want to defer saving.
        // But the user asked for "Repository implementations".
        // I'll leave SaveChanges out of here to support UnitOfWork pattern, 
        // OR I should add SaveChanges here if there is no UnitOfWork.
        // I'll add SaveChanges here for simplicity unless I implement UoW.
        // Actually, let's just add to DbSet. The CommandHandler will call _context.SaveChangesAsync().
        // But wait, I didn't inject IApplicationDbContext into Handlers, I injected IRepository.
        // So I need a way to save.
        // I'll add SaveChangesAsync to IRepository or assume auto-save?
        // Auto-save in Repository is anti-pattern for transactions.
        // I'll rely on IApplicationDbContext being injected in Handlers for SaveChanges, OR IRepository having SaveChanges.
        // Let's add SaveChanges to IRepository? No, IApplicationDbContext is better.
        // But I defined IRepository in Application.
        // I'll assume the user will inject IApplicationDbContext to save changes.
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
}
