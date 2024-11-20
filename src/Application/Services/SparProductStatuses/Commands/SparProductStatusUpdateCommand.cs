namespace Engage.Application.Services.SparProductStatuses.Commands;

public class SparProductStatusUpdateCommand : IMapTo<SparProductStatus>, IRequest<SparProductStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparProductStatusUpdateCommand, SparProductStatus>();
    }
}

public record SparProductStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductStatusUpdateCommand, SparProductStatus>
{
    public async Task<SparProductStatus> Handle(SparProductStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparProductStatuses.SingleOrDefaultAsync(e => e.SparProductStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSparProductStatusValidator : AbstractValidator<SparProductStatusUpdateCommand>
{
    public UpdateSparProductStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
    }
}