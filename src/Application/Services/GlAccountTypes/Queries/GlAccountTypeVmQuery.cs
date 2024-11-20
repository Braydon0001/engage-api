using Engage.Application.Services.GlAccountTypes.Models;

namespace Engage.Application.Services.GlAccountTypes.Queries;

public class GlAccountTypeVmQuery : GetByIdQuery, IRequest<GlAccountTypeDto>
{

}

public class GlAccountTypeQueryHandler : BaseQueryHandler, IRequestHandler<GlAccountTypeVmQuery, GlAccountTypeDto>
{
    public GlAccountTypeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<GlAccountTypeDto> Handle(GlAccountTypeVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GLAccountTypes.SingleAsync(x => x.GLAccountTypeId == request.Id, cancellationToken);
        return _mapper.Map<GLAccountType, GlAccountTypeDto>(entity);
    }
}
