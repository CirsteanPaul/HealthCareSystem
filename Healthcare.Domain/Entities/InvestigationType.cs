using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.EntityTypes;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Domain.Entities;

public sealed class InvestigationType : Entity, ISoftDelete
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    // TODO: Accept different content formats. Must probably a JSON.
    public string ContentFormat { get; private set; }
    
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedOnUtcTime { get; private set; }
    
    private InvestigationType() { }

    private InvestigationType(string name, decimal price, string contentFormat) : base(Guid.NewGuid())
    {
        Name = name;
        Price = price;
        ContentFormat = contentFormat;
        IsDeleted = false;
        DeletedOnUtcTime = null;
    }

    public static Result<InvestigationType> Create(string name, decimal price, string contentFormat)
    {
        if (string.IsNullOrWhiteSpace(contentFormat))
        {
            return Result.Failure<InvestigationType>(DomainErrors.General.InternalServerError);
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<InvestigationType>(DomainErrors.General.InternalServerError);
        }

        if (price <= 0)
        {
            return Result.Failure<InvestigationType>(DomainErrors.General.InternalServerError);
        }

        InvestigationType newInvestigationType = new(name, price, contentFormat);

        // Raise domain event.
        
        return newInvestigationType;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtcTime = DateTime.UtcNow;
    }

    public Result ChangeFormat(string newFormat)
    {
        if (string.IsNullOrWhiteSpace(newFormat))
        {
            return Result.Failure(DomainErrors.General.InternalServerError);
        }

        ContentFormat = newFormat;

        return Result.Success();
    }
}