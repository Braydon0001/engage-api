namespace Engage.Application.Services.GLAccounts.Commands;

public class GLAccountCommand : IMapTo<GLAccount>
{
    public int GLAccountTypeId { get; set; }
    public int EngageRegionId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GLAccountCommand, GLAccount>();
    }
}
