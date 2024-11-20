namespace Engage.Application.Services.Options.Queries;

public class GetCascadingOptionListQuery : GetQuery, IRequest<List<CascadingOptionDto>>
{
    public string Option { get; set; }
    public int? ParentId { get; set; }

    public GetCascadingOptionListQuery() { }
    public GetCascadingOptionListQuery(string option)
    {
        Option = option;
    }
}

public class GetCascadingOptionListByParentQuery : GetCascadingOptionListQuery, IRequest<List<CascadingOptionDto>>
{
}

public class GetCascadingOptionListHandler : IRequestHandler<GetCascadingOptionListQuery, List<CascadingOptionDto>>
{
    private readonly IAppDbContext _context;

    public GetCascadingOptionListHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CascadingOptionDto>> Handle(GetCascadingOptionListQuery request, CancellationToken cancellationToken)
    {
        var optionName = request.Option.ToUpper();

        var query = optionName switch
        {
            CascadingOptionsDesc.BRAND_BY_SUBGROUPS => _context.EngageSubGroupEngageBrands.Select(o => new { Id = o.EngageBrandId, ParentId = o.EngageSubGroupId, o.EngageBrand.Name, o.EngageBrand.Disabled }),
            CascadingOptionsDesc.CLAIMYEAR_BY_CLAIMPERIOD => _context.ClaimPeriods.Select(o => new { Id = o.ClaimPeriodId, ParentId = o.ClaimYearId, o.Name, o.Disabled }),
            CascadingOptionsDesc.CATEGORIES_BY_SUBGROUP => _context.EngageCategories.Select(o => new { o.Id, ParentId = o.EngageSubGroupId, o.Name, o.Disabled }),
            CascadingOptionsDesc.SUBCATEGORIES_BY_CATEGORY => _context.EngageSubCategories.Select(o => new { o.Id, ParentId = o.EngageCategoryId, o.Name, o.Disabled }),
            CascadingOptionsDesc.SUBGROUPS_BY_BRAND => _context.EngageSubGroupEngageBrands.Select(o => new { o.EngageSubGroup.Id, ParentId = o.EngageBrandId, o.EngageSubGroup.Name, o.EngageSubGroup.Disabled }),
            CascadingOptionsDesc.SUBGROUPS_BY_GROUP => _context.EngageSubGroups.Select(o => new { o.Id, ParentId = o.EngageGroupId, o.Name, o.Disabled }),
            CascadingOptionsDesc.SUPPLIERREGIONS_BY_SUPPLIER => _context.SupplierRegions.Select(o => new { o.Id, ParentId = o.SupplierId, o.Name, o.Disabled }),
            CascadingOptionsDesc.VENDORS_BY_DISTRIBUTIONCENTER => _context.Vendors.Select(o => new { Id = o.VendorId, ParentId = o.DistributionCenterId, o.Name, o.Disabled }),
            _ => throw new UnknownFilteredOptionException(optionName),
        };

        return await query.Where(o => o.Disabled == false)
                          .Where(o => o.ParentId == (request.ParentId.HasValue ? request.ParentId : o.ParentId))
                          .Select(o => new CascadingOptionDto { Id = o.Id, ParentId = o.ParentId, Name = o.Name })
                          .OrderBy(o => o.Name)
                          .ToListAsync(cancellationToken);
    }
}

public class GetCascadingOptionListByParentHandler : IRequestHandler<GetCascadingOptionListByParentQuery, List<CascadingOptionDto>>
{
    private readonly IMediator _mediator;

    public GetCascadingOptionListByParentHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<CascadingOptionDto>> Handle(GetCascadingOptionListByParentQuery request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(request.Merge(new GetCascadingOptionListQuery
        {
            Option = request.Option,
            ParentId = request.ParentId
        }));
    }
}

