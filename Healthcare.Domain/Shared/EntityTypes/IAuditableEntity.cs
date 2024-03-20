namespace Healthcare.Domain.Shared.EntityTypes;

public interface IAuditableEntity
{
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
}
