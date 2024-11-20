namespace Engage.Application.Services.CommunicationTypes.Queries;

public record CommunicationTypeVmQuery(int Id) : IRequest<CommunicationTypeVm>;

public record CommunicationTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTypeVmQuery, CommunicationTypeVm>
{
    public async Task<CommunicationTypeVm> Handle(CommunicationTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTypes.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.CommunicationTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CommunicationTypeVm>(entity);
    }
}