namespace Engage.Application.Services.ExternalUserTypes.Commands;

public class ExternalUserTypeUpdateCommand : IMapTo<ExternalUserType>, IRequest<ExternalUserType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExternalUserTypeUpdateCommand, ExternalUserType>();
    }
}

public record ExternalUserTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExternalUserTypeUpdateCommand, ExternalUserType>
{
    public async Task<ExternalUserType> Handle(ExternalUserTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ExternalUserTypes.SingleOrDefaultAsync(e => e.ExternalUserTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateExternalUserTypeValidator : AbstractValidator<ExternalUserTypeUpdateCommand>
{
    public UpdateExternalUserTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}