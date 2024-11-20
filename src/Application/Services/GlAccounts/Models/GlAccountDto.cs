namespace Engage.Application.Services.GLAccounts.Models;

public class GLAccountDto : IMapFrom<GLAccount>
{
    public int Id { get; set; }
    public int GLAccountTypeId { get; set; }
    public int EngageRegionId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GLAccount, GLAccountDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.GLAccountId));
    }
}
