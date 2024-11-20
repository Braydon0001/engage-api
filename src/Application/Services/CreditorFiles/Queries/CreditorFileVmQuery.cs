namespace Engage.Application.Services.CreditorFiles.Queries;

public record CreditorFileVmQuery(int Id) : IRequest<CreditorFileVm>;

public record CreditorFileVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileVmQuery, CreditorFileVm>
{
    public async Task<CreditorFileVm> Handle(CreditorFileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorFiles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Creditor)
                             .Include(e => e.CreditorFileType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CreditorFileId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CreditorFileVm>(entity);
    }
}