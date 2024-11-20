namespace Engage.Application.Services.ProductAnalysisDivisions.Models;

public class ProductAnalysisDivisionDto : IMapFrom<ProductAnalysisDivision>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysisDivision, ProductAnalysisDivisionDto>();
    }
}
