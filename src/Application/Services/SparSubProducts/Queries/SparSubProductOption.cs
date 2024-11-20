namespace Engage.Application.Services.SparSubProducts.Queries;

public class SparSubProductOption : IMapFrom<SparSubProduct>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSubProduct, SparSubProductOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSubProductId));
    }
}