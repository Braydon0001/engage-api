namespace Engage.Domain.Entities;

public class ProjectProjectTagDCProduct : ProjectProjectTag
{
    public int DCProductId { get; set; }
    public DCProduct DCProduct { get; set; }
}
