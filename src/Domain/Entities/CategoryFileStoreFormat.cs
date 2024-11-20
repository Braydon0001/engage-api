namespace Engage.Domain.Entities;

public class CategoryFileStoreFormat : CategoryFileTarget
{
    public int StoreFormatId { get; set; }

    public StoreFormat StoreFormat { get; set; }
}
