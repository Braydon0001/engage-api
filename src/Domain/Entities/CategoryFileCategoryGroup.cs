namespace Engage.Domain.Entities
{
    public class CategoryFileCategoryGroup : CategoryFileTarget
    {
        public int CategoryGroupId { get; set; }
        public CategoryGroup CategoryGroup { get; set; }
    }
}
