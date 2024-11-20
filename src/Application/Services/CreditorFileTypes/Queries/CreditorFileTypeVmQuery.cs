namespace Engage.Application.Services.CreditorFileTypes.Queries;

public record CreditorFileTypeVmQuery(int Id) : IRequest<CreditorFileTypeVm>;

public record CreditorFileTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileTypeVmQuery, CreditorFileTypeVm>
{
    public async Task<CreditorFileTypeVm> Handle(CreditorFileTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorFileTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CreditorFileTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CreditorFileTypeVm>(entity);
    }
}