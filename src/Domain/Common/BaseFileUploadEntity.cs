using Engage.Domain.Entities;

namespace Engage.Domain.Common;

public class BaseFileUploadEntity : BaseAuditableEntity
{
    public int FileUploadId { get; set; }
    public int RowNo { get; set; }
    public RowType RowType { get; set; }
    public string RowMessage { get; set; }

    //Navigation Properties
    public FileUpload FileUpload { get; set; }
}
