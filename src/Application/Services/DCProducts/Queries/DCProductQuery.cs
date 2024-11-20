using Engage.Application.Services.DCProducts.Models;

namespace Engage.Application.Services.DCProducts.Queries;

public class DCProductQuery : GetByIdQuery, IRequest<DCProductDto>
{ 
}

public class GetDCProductQueryHandler : BaseQueryHandler, IRequestHandler<DCProductQuery, DCProductDto>
{
    public GetDCProductQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<DCProductDto> Handle(DCProductQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.DCProducts.SingleAsync(x => x.DCProductId == request.Id, cancellationToken);

        return _mapper.Map<DCProduct, DCProductDto>(entity);
    }
}
