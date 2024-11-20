namespace Engage.Application.Services.CategoryFiles.Queries;

public class CategoryFileStoreQuery : GetQuery, IRequest<ListResult<CategoryFileDto>>
{
    public int StoreId { get; set; }
    public int EmployeeId { get; set; }

    public List<int> FileTypeId { get; set; }


}

public class CategoryFileStoreHandler : BaseQueryHandler, IRequestHandler<CategoryFileStoreQuery, ListResult<CategoryFileDto>>
{
    public CategoryFileStoreHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<CategoryFileDto>> Handle(CategoryFileStoreQuery query, CancellationToken cancellationToken)
    {
        if (query.StoreId == 0)
        {
            throw new Exception("StoreId not found");
        }


        var categoryFileIds = await _context.CategoryFileEmployees
            .Where(e => e.EmployeeId == query.EmployeeId)
            .Select(e => e.CategoryFileId)
            .Distinct()
            .ToListAsync(cancellationToken);

        var categoryFileJobTitleIds = await _context.EmployeeEmployeeJobTitles
            .Where(e => e.EmployeeId == query.EmployeeId)
             .Select(e => e.EmployeeJobTitleId)
            .ToListAsync(cancellationToken);

        var categoryFileEmployeeRegionIds = await _context.EmployeeRegions
                                .Where(e => e.EmployeeId == query.EmployeeId)
                                .Select(s => s.EngageRegionId)
                                .ToListAsync(cancellationToken);

        var categoryFileStoreIds = await _context.CategoryFileStores
            .Where(e => e.StoreId == query.StoreId)
            .Select(e => e.StoreId)
            .ToListAsync(cancellationToken);

        var categoryFileStoreFormatIds = await _context.Stores
            .Where(e => e.StoreId == query.StoreId)
            .Select(e => e.StoreFormatId)
            .FirstOrDefaultAsync(cancellationToken);

        var employeeSubGroupIds = await _context.EmployeeStores
            .Where(e => e.StoreId == query.StoreId && e.EmployeeId == query.EmployeeId)
            .Select(e => e.EngageSubGroupId)
            .ToListAsync(cancellationToken);

        var categoryFileSubGroupIds = await _context.CategoryFileEngageSubGroups
            .Where(e => employeeSubGroupIds.Contains(e.EngageSubGroupId))
            .Select(e => e.EngageSubGroupId)
            .ToListAsync(cancellationToken);

        var categoryFileStoreGroupIds = await _context.CategoryStoreGroups
            .Where(e => e.StoreId == query.StoreId)
            .Select(e => e.CategoryGroupId)
            .ToListAsync(cancellationToken);

        var categoryFileCategoryGroupIds = await _context.CategoryFileCategoryGroups
            .Where(e => categoryFileStoreGroupIds.Contains(e.CategoryGroupId))
            .Select(e => e.CategoryGroupId)
            .ToListAsync(cancellationToken);


        categoryFileIds.AddRange(await _context.CategoryFileEmployeeJobTitles
            .Where(e => categoryFileJobTitleIds.Contains(e.EmployeeJobTitleId))
            .Select(e => e.CategoryFileId)
            .ToListAsync(cancellationToken));

        categoryFileIds.AddRange(await _context.CategoryFileEngageRegions
            .Where(e => categoryFileEmployeeRegionIds.Contains(e.EngageRegionId))
            .Select(e => e.CategoryFileId)
            .ToListAsync(cancellationToken));

        categoryFileIds.AddRange(await _context.CategoryFileStores
            .Where(e => categoryFileStoreIds.Contains(e.StoreId))
             .Select(e => e.CategoryFileId)
            .ToListAsync(cancellationToken));

        categoryFileIds.AddRange(await _context.CategoryFileStoreFormats
            .Where(e => categoryFileStoreFormatIds == e.StoreFormatId)
             .Select(e => e.CategoryFileId)
            .ToListAsync(cancellationToken));

        categoryFileIds.AddRange(await _context.CategoryFileCategoryGroups
            .Where(e => categoryFileCategoryGroupIds.Contains(e.CategoryGroupId))
            .Select(e => e.CategoryFileId)
            .ToListAsync(cancellationToken));

        categoryFileIds.AddRange(await _context.CategoryFileEngageSubGroups
            .Where(e => categoryFileSubGroupIds.Contains(e.EngageSubGroupId))
            .Select(e => e.CategoryFileId)
            .ToListAsync(cancellationToken));



        categoryFileIds = categoryFileIds.Distinct().ToList();



        if (categoryFileIds.Count == 0)
        {
            return new ListResult<CategoryFileDto>
            {
                Data = new List<CategoryFileDto>(),
                Count = 0
            };
        }


        var queryable = _context.CategoryFiles.AsNoTracking().AsQueryable().Where(e => categoryFileIds.Contains(e.CategoryFileId));

        if (query.FileTypeId != null && query.FileTypeId.Any())
        {
            queryable = queryable.Where(e => query.FileTypeId.Contains(e.CategoryFileTypeId));
        }

        if (!string.IsNullOrEmpty(query.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{query.Search}%"));
        }


        var entities = await queryable.ProjectTo<CategoryFileDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);


        return new ListResult<CategoryFileDto>
        {
            Data = entities,
            Count = entities.Count
        };





    }
}


