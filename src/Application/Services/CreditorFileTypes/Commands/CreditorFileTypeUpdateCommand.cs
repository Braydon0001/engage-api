namespace Engage.Application.Services.CreditorFileTypes.Commands;

public class CreditorFileTypeUpdateCommand : IMapTo<CreditorFileType>, IRequest<CreditorFileType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFileTypeUpdateCommand, CreditorFileType>();
    }
}

public record CreditorFileTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileTypeUpdateCommand, CreditorFileType>
{
    public async Task<CreditorFileType> Handle(CreditorFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CreditorFileTypes.SingleOrDefaultAsync(e => e.CreditorFileTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCreditorFileTypeValidator : AbstractValidator<CreditorFileTypeUpdateCommand>
{
    public UpdateCreditorFileTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}