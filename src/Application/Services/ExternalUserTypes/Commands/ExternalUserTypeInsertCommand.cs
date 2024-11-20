namespace Engage.Application.Services.ExternalUserTypes.Commands;

public class ExternalUserTypeInsertCommand : IMapTo<ExternalUserType>, IRequest<ExternalUserType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExternalUserTypeInsertCommand, ExternalUserType>();
    }
}

public record ExternalUserTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExternalUserTypeInsertCommand, ExternalUserType>
{
    public async Task<ExternalUserType> Handle(ExternalUserTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ExternalUserTypeInsertCommand, ExternalUserType>(command);

        Context.ExternalUserTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ExternalUserTypeInsertValidator : AbstractValidator<ExternalUserTypeInsertCommand>
{
    public ExternalUserTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}