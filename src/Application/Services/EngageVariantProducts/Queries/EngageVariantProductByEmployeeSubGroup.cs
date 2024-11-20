using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductByEmployeeSubGroupQuery : IRequest<List<EngageVariantProductMobileDto>>
{
    public int EmployeeId { get; set; }
}

public class EngageVariantProductByEmployeeSubGroupQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductByEmployeeSubGroupQuery, List<EngageVariantProductMobileDto>>
{
    private readonly OrderDefaultsOptions _options;

    public EngageVariantProductByEmployeeSubGroupQueryHandler(IAppDbContext context, IMapper mapper, IOptions<OrderDefaultsOptions> options) : base(context, mapper)
    {
        _options = options.Value;
    }

    public async Task<List<EngageVariantProductMobileDto>> Handle(EngageVariantProductByEmployeeSubGroupQuery query, CancellationToken cancellationToken)
    {
        var EngageSubGroupIds = await _context.EmployeeStores
                                    .Where(e => e.EmployeeId == query.EmployeeId)
                                    .Select(e => e.EngageSubGroupId)
                                    .Distinct()
                                    .ToListAsync(cancellationToken);


        var results = await (from variants in _context.EngageVariantProducts
                             join master in _context.EngageMasterProducts
                                 on variants.EngageMasterProductId equals master.EngageMasterProductId
                             join subCat in _context.EngageSubCategories
                                 on master.EngageSubCategoryId equals subCat.Id
                             join cat in _context.EngageCategories
                                 on subCat.EngageCategoryId equals cat.Id
                             join subGrp in _context.EngageSubGroups
                                 on cat.EngageSubGroupId equals subGrp.Id
                             where
                                 variants.Disabled == false &&
                                 variants.Deleted == false &&
                                 master.Deleted == false &&
                                 master.Disabled == false &&
                                 // variants.Created > lastDate &&
                                 EngageSubGroupIds.Contains(subGrp.Id)
                             select new EngageVariantProductMobileDto
                             {
                                 Id = variants.EngageVariantProductId,
                                 Name = variants.Name,
                                 Code = variants.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().Name,
                                 Size = variants.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().Size,
                                 UnitType = variants.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().UnitType.Name,
                                 Warehouse = variants.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().SubWarehouse
                             }).ToListAsync(cancellationToken);

        return results;

    }
}
