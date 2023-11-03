namespace Healthcare.Application.Core.Abstractions.Data;

public interface IDbContext
{

      Task<TEntity?> GetBydIdAsync<TEntity>(Guid id)
            where TEntity : class;

        void Insert<TEntity>(TEntity entity)
            where TEntity : class;

        void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
            where TEntity : class;
        
        void Remove<TEntity>(TEntity entity)
            where TEntity : class;
}