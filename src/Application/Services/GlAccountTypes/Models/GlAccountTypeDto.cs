namespace Engage.Application.Services.GlAccountTypes.Models;

public class GlAccountTypeDto : IMapFrom<GLAccountType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GLAccountType, GlAccountTypeDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.GLAccountTypeId));
    }
}
