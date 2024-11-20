namespace Engage.Application.Services.SparProducts.Queries;

public class SparProductOption : IMapFrom<SparProduct>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparProduct, SparProductOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparProductId));
    }
}