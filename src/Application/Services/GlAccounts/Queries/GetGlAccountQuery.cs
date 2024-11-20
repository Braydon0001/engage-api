using Engage.Application.Services.GLAccounts.Models;

namespace Engage.Application.Services.GLAccounts.Queries;

public class GetGLAccountQuery : GetByIdQuery, IRequest<GLAccountDto>
{
}

public class GetGLAccountQueryHandler : BaseQueryHandler, IRequestHandler<GetGLAccountQuery, GLAccountDto>
{
    public GetGLAccountQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<GLAccountDto> Handle(GetGLAccountQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GLAccounts.FirstOrDefaultAsync(x => x.GLAccountId == request.Id, cancellationToken);

        return _mapper.Map<GLAccount, GLAccountDto>(entity);
    }
}
