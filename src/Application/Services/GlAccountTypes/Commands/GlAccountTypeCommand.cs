namespace Engage.Application.Services.GlAccountTypes.Commands;

public class GLAccountTypeCommand : IMapTo<GLAccountType>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GLAccountTypeCommand, GLAccountType>();
    }
}
