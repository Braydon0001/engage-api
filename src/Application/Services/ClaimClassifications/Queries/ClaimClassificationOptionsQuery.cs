using Engage.Application.Services.ClaimClassifications.Models;

namespace Engage.Application.Services.ClaimClassifications.Queries;

public class ClaimClassificationOptionsQuery : GetQuery, IRequest<List<ClaimClassificationVm>>
{
    public bool IsSupplier { get; set; }
}

public class ClaimClassificationOptionsQueryHandler : BaseQueryHandler, IRequestHandler<ClaimClassificationOptionsQuery, List<ClaimClassificationVm>>
{
    public ClaimClassificationOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ClaimClassificationVm>> Handle(ClaimClassificationOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClaimClassifications.Where(e => e.Disabled == false)
                                                  .OrderBy(e => e.Name)
                                                  .ProjectTo<ClaimClassificationVm>(_mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);
        if (request.IsSupplier)
        {
            entities = entities.Where(e => e.IsSupplierProcess == true).ToList();
        }

        return entities;
    }
}
