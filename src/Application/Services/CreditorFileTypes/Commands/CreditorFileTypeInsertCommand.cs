namespace Engage.Application.Services.CreditorFileTypes.Commands;

public class CreditorFileTypeInsertCommand : IMapTo<CreditorFileType>, IRequest<CreditorFileType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFileTypeInsertCommand, CreditorFileType>();
    }
}

public record CreditorFileTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileTypeInsertCommand, CreditorFileType>
{
    public async Task<CreditorFileType> Handle(CreditorFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CreditorFileTypeInsertCommand, CreditorFileType>(command);
        
        Context.CreditorFileTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CreditorFileTypeInsertValidator : AbstractValidator<CreditorFileTypeInsertCommand>
{
    public CreditorFileTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}