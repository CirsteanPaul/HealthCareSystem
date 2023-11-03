using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Infrastructure.Repositories;

public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
{
    protected AsyncRepository(HealthcareContext context) => this.context = context;
    
    protected HealthcareContext context { get; }
    
    public virtual async Task<Result<TEntity>> AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
        return Result.Success(entity);
    }

    public virtual async Task<Result<TEntity>> DeleteAsync(Guid id)
    {
        var result = await FindByIdAsync(id);
        if (result != null)
        {
            context.Set<TEntity>().Remove(result.Value);
            await context.SaveChangesAsync();
            return result;
        }
        return Result.Failure<TEntity>(new Error("Persistance error", "No id found"));
    }

    public virtual async Task<Result<TEntity>> FindByIdAsync(Guid id)
    {
        var result = await context.Set<TEntity>().FindAsync(id);
        if (result == null)
        {
            return Result.Failure<TEntity>(new Error("Persistance error", "No id found"));
        }
        return result;
    }

    public virtual async Task<Result<IReadOnlyList<TEntity>>> GetPagedReponseAsync(int page, int size)
    {
        var result = await context.Set<TEntity>().Skip(page).Take(size).AsNoTracking().ToListAsync();
        return result.AsReadOnly();
    }

    public virtual async Task<Result<TEntity>> UpdateAsync(TEntity entity)
    {
        context.Entry(entity).State=EntityState.Modified;
        await context.SaveChangesAsync();
        return Result.Success(entity);
    }
}
