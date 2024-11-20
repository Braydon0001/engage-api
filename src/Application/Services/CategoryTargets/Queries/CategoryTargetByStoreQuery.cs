//namespace Engage.Application.Services.CategoryTargets.Queries;

//public class CategoryTargetByStoreQuery : IRequest<List<CategoryTargetDto>>
//{
//    public int StoreId { get; set; }
//}

//public record CategoryTargetByStoreHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetByStoreQuery, List<CategoryTargetDto>>
//{
//    public async Task<List<CategoryTargetDto>> Handle(CategoryTargetByStoreQuery query, CancellationToken cancellationToken)
//    {
//        if (query.StoreId == 0)
//        {
//            throw new Exception("StoreId not found");
//        }



//        var queryable = Context.CategoryTargets.AsQueryable().AsNoTracking();



//        return await queryable.Where(e => e.StoreId == query.StoreId)
//            .OrderBy(s => s.CategoryTargetId)
//            .ProjectTo<CategoryTargetDto>(Mapper.ConfigurationProvider)
//            .ToListAsync();

//    }
//}


