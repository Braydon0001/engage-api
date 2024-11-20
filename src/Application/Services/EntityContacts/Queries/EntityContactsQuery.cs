using Engage.Application.Services.EntityContacts.Models;

namespace Engage.Application.Services.EntityContacts.Queries;

// Queries   

public class EngageRegionContactsQuery : GetQuery, IRequest<ListResult<EngageRegionContactDto>>
{
    public int EngageRegionId { get; set; }
    public int? EntityContactTypeId { get; set; }
}

public class StoreContactsQuery : GetQuery, IRequest<ListResult<StoreContactDto>>
{
    public int StoreId { get; set; }
    public int? EntityContactTypeId { get; set; }
}

public class StoreContactOptionQuery : GetQuery, IRequest<List<StoreContactOption>>
{
    public int? StoreId { get; set; }
    public int? ClaimId { get; set; }
}

public class StoreContactOptionByProjectQuery : GetQuery, IRequest<List<StoreContactOption>>
{
    public int? ProjectId { get; set; }
}

public class StoreContactEmailOptionQuery : GetQuery, IRequest<ListResult<StoreContactEmailOption>>
{
    public int StoreId { get; set; }
}

public class StoreContactEmailMobileOptionQuery : GetQuery, IRequest<ListResult<OptionDto>>
{
    public int StoreId { get; set; }
}

public class SupplierContactsQuery : GetQuery, IRequest<ListResult<SupplierContactDto>>
{
    public int SupplierId { get; set; }
    public int? EntityContactTypeId { get; set; }
}

public class SupplierContactOptionByProjectQuery : GetQuery, IRequest<List<SupplierContactOption>>
{
    public int ProjectId { get; set; }
}

// Handlers   
public class EngageRegionContactsQueryHandler : BaseQueryHandler, IRequestHandler<EngageRegionContactsQuery, ListResult<EngageRegionContactDto>>
{
    public EngageRegionContactsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageRegionContactDto>> Handle(EngageRegionContactsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageRegionContacts.Where(e => e.EngageRegionId == request.EngageRegionId);

        if (request.EntityContactTypeId.HasValue)
        {
            queryable = queryable.Where(e => e.EntityContactTypeId == request.EntityContactTypeId.Value);
        }

        var entities = await queryable.OrderBy(e => e.FullName)
                                      .ProjectTo<EngageRegionContactDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EngageRegionContactDto>(entities);
    }
}

public class StoreContactsQueryHandler : BaseQueryHandler, IRequestHandler<StoreContactsQuery, ListResult<StoreContactDto>>
{
    public StoreContactsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreContactDto>> Handle(StoreContactsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreContacts.Where(e => e.StoreId == request.StoreId);

        if (request.EntityContactTypeId.HasValue)
        {
            queryable = queryable.Where(e => e.EntityContactTypeId == request.EntityContactTypeId.Value);
        }

        var entities = await queryable.OrderBy(e => e.FullName)
                                      .ProjectTo<StoreContactDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<StoreContactDto>(entities);
    }
}

public class StoreContactOptionHandler : BaseQueryHandler, IRequestHandler<StoreContactOptionQuery, List<StoreContactOption>>
{
    public StoreContactOptionHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreContactOption>> Handle(StoreContactOptionQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreContacts.AsQueryable();

        if (request.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == request.StoreId.Value);
        }

        if (request.ClaimId.HasValue)
        {
            var claim = await _context.Claims.SingleOrDefaultAsync(e => e.ClaimId == request.ClaimId.Value, cancellationToken);
            queryable = queryable.Where(e => e.StoreId == claim.StoreId);
        }

        return await queryable.OrderBy(e => e.FullName)
                              .ProjectTo<StoreContactOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}

public class StoreContactOptionByProjectHandler : BaseQueryHandler, IRequestHandler<StoreContactOptionByProjectQuery, List<StoreContactOption>>
{
    public StoreContactOptionByProjectHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreContactOption>> Handle(StoreContactOptionByProjectQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreContacts.AsQueryable();

        if (request.ProjectId.HasValue)
        {
            var storeIds = new List<int>();

            var project = await _context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == request.ProjectId.Value, cancellationToken);

            var projectStoreTagIds = await _context.ProjectProjectTagStores.Where(e => e.ProjectId == request.ProjectId.Value)
                                                                        .Select(e => e.StoreId)
                                                                        .ToListAsync(cancellationToken);

            if (project != null)
            {
                storeIds.Add(project.StoreId);
            }

            if (projectStoreTagIds.Count > 0)
            {
                storeIds.AddRange(projectStoreTagIds);
            }

            if (storeIds.Count > 0)
            {
                queryable = queryable.Where(e => storeIds.Contains(e.StoreId));

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    queryable = queryable.Where(e => EF.Functions.Like(e.FullName, $"%{request.Search}%") ||
                                                     EF.Functions.Like(e.EmailAddress1, $"%{request.Search}%"));

                    return await queryable.OrderBy(e => e.FullName)
                                          .ProjectTo<StoreContactOption>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);
                }
            }
        }

        return new List<StoreContactOption>();

    }
}

public class SupplierContactOptionByProjectHandler : BaseQueryHandler, IRequestHandler<SupplierContactOptionByProjectQuery, List<SupplierContactOption>>
{
    public SupplierContactOptionByProjectHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContactOption>> Handle(SupplierContactOptionByProjectQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContacts.AsQueryable();

        if (request.ProjectId != 0)
        {
            var supplierIds = new List<int>();

            //var projectSupplier = await _context.ProjectSuppliers.FirstOrDefaultAsync(e => e.ProjectId == request.ProjectId, cancellationToken);

            //var projectSupplierIds = await _context.ProjectProjectTagSuppliers.Where(e => e.ProjectId == request.ProjectId).Select(e => e.SupplierId).ToListAsync(cancellationToken);
            //if (projectSupplierIds.Count > 0)
            //{
            //    supplierIds.AddRange(projectSupplierIds);
            //}

            //if (projectSupplier != null)
            //{
            //    supplierIds.Add(projectSupplier.SupplierId);
            //}

            supplierIds = supplierIds.Distinct().ToList();

            if (supplierIds.Count > 0)
            {
                queryable = queryable.Where(e => supplierIds.Contains(e.SupplierId));
            }
            else
            {
                return new List<SupplierContactOption>();
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                queryable = queryable.Where(e => EF.Functions.Like(e.FullName, $"%{request.Search}%") ||
                                                 EF.Functions.Like(e.EmailAddress1, $"%{request.Search}%"));
            }

            return await queryable.OrderBy(e => e.FullName)
                          .ProjectTo<SupplierContactOption>(_mapper.ConfigurationProvider)
                          .ToListAsync(cancellationToken);
        }

        return new List<SupplierContactOption>();

    }
}

public class StoreContactEmailOptionHandler : BaseQueryHandler, IRequestHandler<StoreContactEmailOptionQuery, ListResult<StoreContactEmailOption>>
{
    public StoreContactEmailOptionHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreContactEmailOption>> Handle(StoreContactEmailOptionQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreContacts.Where(e => e.StoreId == request.StoreId);

        var entities = await queryable.OrderBy(e => e.EmailAddress1)
                                      .ProjectTo<StoreContactEmailOption>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<StoreContactEmailOption>(entities);
    }
}

public class StoreContactEmailMobileOptionHandler : BaseQueryHandler, IRequestHandler<StoreContactEmailMobileOptionQuery, ListResult<OptionDto>>
{
    public StoreContactEmailMobileOptionHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<OptionDto>> Handle(StoreContactEmailMobileOptionQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreContacts.Where(e => e.StoreId == request.StoreId);

        var entities = await queryable.OrderBy(e => e.EmailAddress1)
                                    .Select(e => new OptionDto { Id = e.EntityContactId, Name = $"{e.FullName} - {e.EmailAddress1}" })
                                      .ToListAsync(cancellationToken);

        return new ListResult<OptionDto>(entities);
    }
}

public class SupplierContactsQueryHandler : BaseQueryHandler, IRequestHandler<SupplierContactsQuery, ListResult<SupplierContactDto>>
{
    public SupplierContactsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<SupplierContactDto>> Handle(SupplierContactsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContacts.Where(e => e.SupplierId == request.SupplierId);

        if (request.EntityContactTypeId.HasValue)
        {
            queryable = queryable.Where(e => e.EntityContactTypeId == request.EntityContactTypeId.Value);
        }

        var entities = await queryable.OrderBy(e => e.FullName)
                                      .ProjectTo<SupplierContactDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<SupplierContactDto>(entities);
    }
}

public class SupplierContactOptionsQuery : GetQuery, IRequest<List<SupplierOption>>
{
    public int SupplierId { get; set; }
}

public class SupplierContactOptionQueryHandler : BaseQueryHandler, IRequestHandler<SupplierContactOptionsQuery, List<SupplierOption>>
{
    public SupplierContactOptionQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierOption>> Handle(SupplierContactOptionsQuery query, CancellationToken cancellationToken)
    {
        return await _context.SupplierContacts.AsNoTracking()
                                .Where(e => e.SupplierId == query.SupplierId)
                                .OrderBy(e => e.FullName)
                                .ProjectTo<SupplierOption>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);
    }
}