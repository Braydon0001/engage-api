namespace Engage.Application.Services.ProductSubCategories.Queries;

public class ProductSubCategoryHierarchyQuery : IRequest<ListResult<ProductSubCategoryHierarchyDto>>
{
    public int Level { get; set; }
}
public class ProductSubCategoryHierarchyHandler : BaseQueryHandler, IRequestHandler<ProductSubCategoryHierarchyQuery, ListResult<ProductSubCategoryHierarchyDto>>
{
    public ProductSubCategoryHierarchyHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ProductSubCategoryHierarchyDto>> Handle(ProductSubCategoryHierarchyQuery query, CancellationToken cancellationToken)
    {
        var dtos = new List<ProductSubCategoryHierarchyDto>();

        var groups = _context.ProductGroups.AsQueryable()
                                           .AsNoTracking()
                                           .OrderBy(e => e.Name);

        var subGroups = _context.ProductSubGroups.AsQueryable()
                                           .AsNoTracking()
                                           .Include(e => e.ProductGroup)
                                           .OrderBy(e => e.Name);

        var category = _context.ProductCategories.AsQueryable()
                                           .AsNoTracking()
                                           .Include(e => e.ProductSubGroup)
                                           .ThenInclude(e => e.ProductGroup)
                                           .OrderBy(e => e.Name);

        var subCategory = _context.ProductSubCategories.AsQueryable()
                                           .AsNoTracking()
                                           .Include(e => e.ProductCategory)
                                           .ThenInclude(e => e.ProductSubGroup)
                                           .ThenInclude(e => e.ProductGroup)
                                           .OrderBy(e => e.Name);

        if (query.Level == 1)
        {
            var groupDtos = await groups.Select(e => new ProductSubCategoryHierarchyDto(e.ProductGroupId, 1, new string[] { e.Name }))
                                        .ToListAsync(cancellationToken);

            var subGroupDtos = await subGroups.Select(e => new ProductSubCategoryHierarchyDto(e.ProductSubGroupId, 2, new string[] { e.ProductGroup.Name, e.Name }))
                                        .ToListAsync(cancellationToken);

            var cateogryDtos = await category.Select(e => new ProductSubCategoryHierarchyDto(e.ProductCategoryId, 3, new string[] { e.ProductSubGroup.ProductGroup.Name, e.ProductSubGroup.Name, e.Name }))
                                        .ToListAsync(cancellationToken);

            var subCateogryDtos = await subCategory.Select(e => new ProductSubCategoryHierarchyDto(e.ProductSubCategoryId, 4, new string[] { e.ProductCategory.ProductSubGroup.ProductGroup.Name, e.ProductCategory.ProductSubGroup.Name, e.ProductCategory.Name, e.Name }))
                                        .ToListAsync(cancellationToken);

            dtos.AddRange(groupDtos);
            dtos.AddRange(subGroupDtos);
            dtos.AddRange(cateogryDtos);
            dtos.AddRange(subCateogryDtos);
        }
        if (query.Level == 2)
        {
            var subGroupDtos = await subGroups.Select(e => new ProductSubCategoryHierarchyDto(e.ProductSubGroupId, 2, new string[] { e.Name }))
                                        .ToListAsync(cancellationToken);

            var cateogryDtos = await category.Select(e => new ProductSubCategoryHierarchyDto(e.ProductCategoryId, 3, new string[] { e.ProductSubGroup.Name, e.Name }))
                                        .ToListAsync(cancellationToken);

            var subCateogryDtos = await subCategory.Select(e => new ProductSubCategoryHierarchyDto(e.ProductSubCategoryId, 4, new string[] { e.ProductCategory.ProductSubGroup.Name, e.ProductCategory.Name, e.Name }))
                                        .ToListAsync(cancellationToken);

            dtos.AddRange(subGroupDtos);
            dtos.AddRange(cateogryDtos);
            dtos.AddRange(subCateogryDtos);
        }
        if (query.Level == 3)
        {

            var cateogryDtos = await category.Select(e => new ProductSubCategoryHierarchyDto(e.ProductCategoryId, 3, new string[] { e.Name }))
                                        .ToListAsync(cancellationToken);

            var subCateogryDtos = await subCategory.Select(e => new ProductSubCategoryHierarchyDto(e.ProductSubCategoryId, 4, new string[] { e.ProductCategory.Name, e.Name }))
                                        .ToListAsync(cancellationToken);

            dtos.AddRange(cateogryDtos);
            dtos.AddRange(subCateogryDtos);
        }
        if (query.Level == 4)
        {
            var subCateogryDtos = await subCategory.Select(e => new ProductSubCategoryHierarchyDto(e.ProductSubCategoryId, 4, new string[] { e.Name }))
                                        .ToListAsync(cancellationToken);

            dtos.AddRange(subCateogryDtos);
        }

        return new ListResult<ProductSubCategoryHierarchyDto>(dtos);
    }
}
