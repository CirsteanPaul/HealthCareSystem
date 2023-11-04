namespace Healthcare.Application.Core.Abstractions.Data;

public interface ITestRepository : IAsyncRepository<Domain.Entities.Test>
{ 
    Task<IReadOnlyList<Domain.Entities.Test>> GetAll(CancellationToken cancellationToken);
}