using Engage.Application.Services.GLAccounts.Models;

namespace Engage.Application.Services.GLAccounts.Queries;

public class GetGLAccountListQuery : GetQuery, IRequest<ListResult<GLAccountListDto>>
{

}

public class GetGLAccountQueryListHandler : BaseQueryHandler, IRequestHandler<GetGLAccountListQuery, ListResult<GLAccountListDto>>
{
    public GetGLAccountQueryListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<GLAccountListDto>> Handle(GetGLAccountListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.GLAccounts.OrderBy(e => e.GLAccountId)
                                                .ProjectTo<GLAccountListDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<GLAccountListDto>(entities);
    }
}
