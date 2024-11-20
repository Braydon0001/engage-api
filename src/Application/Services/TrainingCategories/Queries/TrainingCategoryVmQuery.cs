using Engage.Application.Services.TrainingCategories.Models;

namespace Engage.Application.Services.TrainingCategories.Queries;

public class TrainingCategoryVmQuery : GetByIdQuery, IRequest<TrainingCategoryVm>
{
}

public class TrainingCategoryVmQueryHandler : BaseQueryHandler, IRequestHandler<TrainingCategoryVmQuery, TrainingCategoryVm>
{
    public TrainingCategoryVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingCategoryVm> Handle(TrainingCategoryVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingCategories.SingleAsync(e => e.TrainingCategoryId == request.Id, cancellationToken);

        return _mapper.Map<TrainingCategory, TrainingCategoryVm>(entity);
    }
}
