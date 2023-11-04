using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Infrastructure.Repositories;

public sealed class TestRepository : AsyncRepository<Test> , ITestRepository
{
    public TestRepository(HealthcareContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Test>> GetAll(CancellationToken cancellationToken)
    {
        var response = await context.Tests.ToListAsync(cancellationToken);
        return response.AsReadOnly();
    }

}