namespace Engage.Application.Services.CreditorFiles.Commands;

public class CreditorFileUpdateCommand : IMapTo<CreditorFile>, IRequest<CreditorFile>
{
    public int Id { get; set; }
    public int CreditorId { get; init; }
    public int CreditorFileTypeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFileUpdateCommand, CreditorFile>();
    }
}

public record CreditorFileUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileUpdateCommand, CreditorFile>
{
    public async Task<CreditorFile> Handle(CreditorFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CreditorFiles.SingleOrDefaultAsync(e => e.CreditorFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCreditorFileValidator : AbstractValidator<CreditorFileUpdateCommand>
{
    public UpdateCreditorFileValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorFileTypeId).NotEmpty().GreaterThan(0);
    }
}