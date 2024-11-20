namespace Domain.Learning.Common;

public class BaseLearningAuditableEntity: BaseLearningEntity
{
    public string CreatedBy { get; set; }

    public DateTime Created { get; set; }

    public string LastModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }
}
