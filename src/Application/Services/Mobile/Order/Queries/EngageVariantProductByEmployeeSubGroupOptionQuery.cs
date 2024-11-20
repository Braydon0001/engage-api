namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductByEmployeeSubGroupOptionQuery : IRequest<List<OptionDto>>
{
    public int EmployeeId { get; set; }
}

public class EngageVariantProductByEmployeeSubGroupOptionQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductByEmployeeSubGroupOptionQuery, List<OptionDto>>
{
    private readonly OrderDefaultsOptions _options;

    public EngageVariantProductByEmployeeSubGroupOptionQueryHandler(IAppDbContext context, IMapper mapper, IOptions<OrderDefaultsOptions> options) : base(context, mapper)
    {
        _options = options.Value;
    }

    public async Task<List<OptionDto>> Handle(EngageVariantProductByEmployeeSubGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var EngageSubGroupIds = await _context.EmployeeStores
                                    .Where(e => e.EmployeeId == query.EmployeeId)
                                    .Select(e => e.EngageSubGroupId)
                                    .Distinct()
                                    .ToListAsync(cancellationToken);

        var results = await _context.EngageVariantProducts
                            .Include(v => v.DCProducts).ThenInclude(d => d.UnitType)
                            .Include(v => v.EngageMasterProduct)
                            .ThenInclude(m => m.EngageSubCategory)
                            .ThenInclude(s => s.EngageCategory)
                            .ThenInclude(c => c.EngageSubGroup)
                            .Where(
                                    p => p.Disabled == false && p.Deleted == false
                                    && p.EngageMasterProduct.Disabled == false && p.EngageMasterProduct.Deleted == false
                                    && EngageSubGroupIds.Contains(p.EngageMasterProduct.EngageSubCategory.EngageCategory.EngageSubGroup.Id)
                                    && p.DCProducts.Any(d => d.ProductActiveStatusId == _options.ProductActiveStatusId && d.Disabled == false && string.IsNullOrEmpty(d.Code) == false)
                                  )
                            .ToListAsync(cancellationToken);

        var res = results
                       .Select(v => new OptionDto
                       {
                           Id = v.EngageVariantProductId,
                           Name = v.Name + " / "
                                    + v.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().Code + " / "
                                    + v.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().Size + " "
                                    + v.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().UnitType.Name + " / "
                                    + v.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().SubWarehouse + " / "
                                    + v.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().DistributionCenterId,
                       })
                       .OrderBy(e => e.Name)
                       .ToList();


        //var results = await (from variants in _context.EngageVariantProducts
        //                     join master in _context.EngageMasterProducts
        //                         on variants.EngageMasterProductId equals master.EngageMasterProductId
        //                     join subCat in _context.EngageSubCategories
        //                         on master.EngageSubCategoryId equals subCat.Id
        //                     join cat in _context.EngageCategories
        //                         on subCat.EngageCategoryId equals cat.Id
        //                     join subGrp in _context.EngageSubGroups
        //                         on cat.EngageSubGroupId equals subGrp.Id
        //                     where
        //                         variants.Disabled == false &&
        //                         variants.Deleted == false &&
        //                         master.Deleted == false &&
        //                         master.Disabled == false &&
        //                         // variants.Created > lastDate &&
        //                         EngageSubGroupIds.Contains(subGrp.Id) &&
        //                         variants.DCProducts.First(e => string.IsNullOrEmpty(e.Name) == false) != null
        //                     select new OptionDto
        //                     {
        //                         Id = variants.EngageVariantProductId,
        //                         Name = variants.Name + " / " + variants.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().Code + " / " + variants.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().Size + " " + variants.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().UnitType.Name + " / " + variants.DCProducts.Where(e => e.Disabled == false && e.ProductActiveStatusId == _options.ProductActiveStatusId).FirstOrDefault().SubWarehouse
        //                     })
        //                     .OrderBy(e => e.Name)
        //                     .ToListAsync(cancellationToken);

        //var products = results.Where(i => string.IsNullOrEmpty(i.Name) == false);

        return res.ToList();

    }
}
