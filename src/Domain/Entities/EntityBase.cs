namespace EasyRh.Domain.Entities;

public class EntityBase
{
    public long Id { get; set; }
    public bool Active { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime UpdateOn { get; set; } = DateTime.UtcNow;
}
