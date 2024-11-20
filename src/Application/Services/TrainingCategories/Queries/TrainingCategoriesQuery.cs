using Engage.Application.Services.TrainingCategories.Models;

namespace Engage.Application.Services.TrainingCategories.Queries
{
    public class TrainingCategoriesQuery : GetQuery, IRequest<ListResult<TrainingCategoryDto>>
    {
    }

    public class TrainingCategoriesQueryHandler : BaseQueryHandler, IRequestHandler<TrainingCategoriesQuery, ListResult<TrainingCategoryDto>>
    {
        public TrainingCategoriesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<TrainingCategoryDto>> Handle(TrainingCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.TrainingCategories
                                                         .OrderByDescending(e => e.TrainingCategoryId)
                                                         .ProjectTo<TrainingCategoryDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

            return new ListResult<TrainingCategoryDto>(entities);
        }
    }
}
