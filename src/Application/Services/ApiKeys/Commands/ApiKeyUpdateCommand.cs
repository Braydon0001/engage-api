namespace Engage.Application.Services.ApiKeys.Commands;

public class ApiKeyUpdateCommand : IMapTo<ApiKey>, IRequest<ApiKey>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Value { get; init; }
    public string AssignedTo { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApiKeyUpdateCommand, ApiKey>();
    }
}

public record ApiKeyUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ApiKeyUpdateCommand, ApiKey>
{
    public async Task<ApiKey> Handle(ApiKeyUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ApiKeys.SingleOrDefaultAsync(e => e.ApiKeyId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateApiKeyValidator : AbstractValidator<ApiKeyUpdateCommand>
{
    public UpdateApiKeyValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Value).NotEmpty().MaximumLength(200);
        RuleFor(e => e.AssignedTo).NotEmpty().MaximumLength(200);
    }
}