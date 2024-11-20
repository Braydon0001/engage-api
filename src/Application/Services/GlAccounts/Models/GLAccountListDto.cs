namespace Engage.Application.Services.GLAccounts.Models;

public class GLAccountListDto : IMapFrom<GLAccount>
{
    public int Id { get; set; }
    public string GLAccountTypeName { get; set; }
    public string GLAccountTypeDescription { get; set; }
    public string EngageRegionName { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GLAccount, GLAccountListDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.GLAccountId))
            .ForMember(e => e.GLAccountTypeName, opt => opt.MapFrom(d => d.GLAccountType.Name))
            .ForMember(e => e.GLAccountTypeDescription, opt => opt.MapFrom(d => d.GLAccountType.Description))
            .ForMember(e => e.EngageRegionName, opt => opt.MapFrom(d => d.EngageRegion.Name));
    }
}
