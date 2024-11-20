namespace Engage.Application.Services.Vat.Queries;

public class VatOptionQuery : IRequest<List<VatOption>>
{

}

public record VatOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<VatOptionQuery, List<VatOption>>
{
    public async Task<List<VatOption>> Handle(VatOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Vat.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.VatId)
                              .ProjectTo<VatOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}