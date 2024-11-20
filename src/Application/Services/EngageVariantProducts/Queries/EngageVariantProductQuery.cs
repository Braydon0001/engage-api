using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductQuery : GetByIdQuery, IRequest<EngageVariantProductDto>
{ 
}

public class EngageVariantProductQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductQuery, EngageVariantProductDto>
{
    public EngageVariantProductQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<EngageVariantProductDto> Handle(EngageVariantProductQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageVariantProducts.SingleAsync(x => x.EngageVariantProductId == request.Id, cancellationToken);

        return _mapper.Map<EngageVariantProduct, EngageVariantProductDto>(entity);

    }
}
