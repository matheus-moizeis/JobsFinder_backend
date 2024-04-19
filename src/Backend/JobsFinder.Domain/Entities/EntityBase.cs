namespace JobsFinder.Domain.Entities;
public class EntityBase
{
    public long Id { get; set; }
    public DateTime CreateOn { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; } = true;
}
