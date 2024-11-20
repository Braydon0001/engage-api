namespace Engage.Application.Services.SparSubProductStatuses.Commands;

public class SparSubProductStatusUpdateCommand : IMapTo<SparSubProductStatus>, IRequest<SparSubProductStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSubProductStatusUpdateCommand, SparSubProductStatus>();
    }
}

public record SparSubProductStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductStatusUpdateCommand, SparSubProductStatus>
{
    public async Task<SparSubProductStatus> Handle(SparSubProductStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparSubProductStatuses.SingleOrDefaultAsync(e => e.SparSubProductStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSparSubProductStatusValidator : AbstractValidator<SparSubProductStatusUpdateCommand>
{
    public UpdateSparSubProductStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
    }
}