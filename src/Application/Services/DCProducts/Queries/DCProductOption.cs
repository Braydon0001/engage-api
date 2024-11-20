namespace Engage.Application.Services.DCProducts.Queries;

public class DCProductOption : IMapFrom<DCProduct>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCProduct, DCProductOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DCProductId));
    }
}
