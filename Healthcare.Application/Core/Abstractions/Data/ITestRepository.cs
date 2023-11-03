namespace Healthcare.Application.Core.Abstractions.Data;

public interface ITestRepository : IAsyncRepository<Domain.Test>
{ 
    Task<IReadOnlyList<Domain.Test>> GetAll(CancellationToken cancellationToken);
}