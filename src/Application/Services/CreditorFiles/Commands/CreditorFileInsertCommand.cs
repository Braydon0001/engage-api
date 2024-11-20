namespace Engage.Application.Services.CreditorFiles.Commands;

public class CreditorFileInsertCommand : IMapTo<CreditorFile>, IRequest<CreditorFile>
{
    public int CreditorId { get; init; }
    public int CreditorFileTypeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFileInsertCommand, CreditorFile>();
    }
}

public record CreditorFileInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileInsertCommand, CreditorFile>
{
    public async Task<CreditorFile> Handle(CreditorFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CreditorFileInsertCommand, CreditorFile>(command);
        
        Context.CreditorFiles.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CreditorFileInsertValidator : AbstractValidator<CreditorFileInsertCommand>
{
    public CreditorFileInsertValidator()
    {
        RuleFor(e => e.CreditorId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorFileTypeId).NotEmpty().GreaterThan(0);
    }
}