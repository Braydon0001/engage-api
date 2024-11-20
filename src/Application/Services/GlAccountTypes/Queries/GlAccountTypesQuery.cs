using Engage.Application.Services.GlAccountTypes.Models;

namespace Engage.Application.Services.GlAccountTypes.Queries;

public class GlAccountTypesQuery : GetQuery, IRequest<ListResult<GlAccountTypeDto>>
{

}

public class GlAccountTypesHandler : BaseQueryHandler, IRequestHandler<GlAccountTypesQuery, ListResult<GlAccountTypeDto>>
{
    public GlAccountTypesHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<GlAccountTypeDto>> Handle(GlAccountTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.GLAccountTypes.ProjectTo<GlAccountTypeDto>(_mapper.ConfigurationProvider)
                                                    .ToListAsync(cancellationToken);

        return new ListResult<GlAccountTypeDto>(entities);
    }
}
