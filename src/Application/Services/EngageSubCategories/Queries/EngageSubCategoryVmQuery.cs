using Engage.Application.Services.EngageSubCategories.Models;

namespace Engage.Application.Services.EngageSubCategories.Queries;

public class EngageSubCategoryVmQuery : GetByIdQuery, IRequest<EngageSubCategoryVm>
{
}

public class EngageSubCategoryVmQueryHandler : BaseQueryHandler, IRequestHandler<EngageSubCategoryVmQuery, EngageSubCategoryVm>
{
    public EngageSubCategoryVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageSubCategoryVm> Handle(EngageSubCategoryVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageSubCategories.Include(e => e.EngageCategory)
                                                       .SingleAsync(e => e.Id == request.Id);

        return _mapper.Map<EngageSubCategory, EngageSubCategoryVm>(entity);
    }
}
