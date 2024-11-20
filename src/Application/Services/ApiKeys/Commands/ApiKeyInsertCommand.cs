namespace Engage.Application.Services.ApiKeys.Commands;

public class ApiKeyInsertCommand : IMapTo<ApiKey>, IRequest<ApiKey>
{
    public string Name { get; init; }
    public string Value { get; init; }
    public string AssignedTo { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApiKeyInsertCommand, ApiKey>();
    }
}

public record ApiKeyInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ApiKeyInsertCommand, ApiKey>
{
    public async Task<ApiKey> Handle(ApiKeyInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ApiKeyInsertCommand, ApiKey>(command);

        Context.ApiKeys.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ApiKeyInsertValidator : AbstractValidator<ApiKeyInsertCommand>
{
    public ApiKeyInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Value).NotEmpty().MaximumLength(200);
        RuleFor(e => e.AssignedTo).NotEmpty().MaximumLength(200);
    }
}