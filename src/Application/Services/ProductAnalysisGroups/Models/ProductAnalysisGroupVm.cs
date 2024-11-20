namespace Engage.Application.Services.ProductAnalysisGroups.Models;

public class ProductAnalysisGroupVm : IMapFrom<ProductAnalysisGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysisGroup, ProductAnalysisGroupVm>();
    }
}
