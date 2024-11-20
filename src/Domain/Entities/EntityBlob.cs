namespace Engage.Domain.Entities;

// See table-per-hierarchy (TPH) 
// https://docs.microsoft.com/en-us/ef/core/modeling/inheritance

public class EntityBlob : BaseAuditableEntity
{
    public int EntityBlobId { get; set; }
    public string FolderName { get; set; }
    public string OriginalFileName { get; set; }
    public string FileName { get; set; }
    public string Url { get; set; }
}

public class ClaimBlob : EntityBlob
{
    public int ClaimId { get; set; }

    // NavigationProperties
    public Claim Claim { get; set; }
}
