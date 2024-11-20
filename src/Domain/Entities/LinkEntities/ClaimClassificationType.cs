namespace Engage.Domain.Entities.LinkEntities;

public class ClaimClassificationType
{
    public int ClaimClassificationId { get; set; }
    public int ClaimTypeId { get; set; }

    // Navigation Properties 
    public ClaimClassification ClaimClassification { get; set; }
    public ClaimType ClaimType { get; set; }
}
