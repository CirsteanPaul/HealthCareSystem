namespace Healthcare.Domain.Shared.EntityTypes;

public interface ISoftDelete
{
    public bool IsDeleted { get; }
    public DateTime? DeletedOnUtcTime { get; }
}