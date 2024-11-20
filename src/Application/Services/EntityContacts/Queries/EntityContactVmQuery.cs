using Engage.Application.Services.CommunicationTypes.Queries;
using Engage.Application.Services.EntityContacts.Models;

namespace Engage.Application.Services.EntityContacts.Queries;

// Queries

public class EngageRegionContactVmQuery : GetByIdQuery, IRequest<EngageRegionContactVm>
{
}

public class StoreContactVmQuery : GetByIdQuery, IRequest<StoreContactVm>
{
}

public class SupplierContactVmQuery : GetByIdQuery, IRequest<SupplierContactVm>
{
}

// Handlers    

public class EngageRegionContactVmQueryHandler : BaseQueryHandler, IRequestHandler<EngageRegionContactVmQuery, EngageRegionContactVm>
{
    public EngageRegionContactVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageRegionContactVm> Handle(EngageRegionContactVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageRegionContacts.Include(e => e.EntityContactType)
                                                        .Include(e => e.User)
                                                        .Include(e => e.EngageRegion)
                                                        .SingleAsync(e => e.EntityContactId == request.Id, cancellationToken);

        return _mapper.Map<EngageRegionContact, EngageRegionContactVm>(entity);
    }
}

public class StoreContactVmQueryHandler : BaseQueryHandler, IRequestHandler<StoreContactVmQuery, StoreContactVm>
{
    public StoreContactVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreContactVm> Handle(StoreContactVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreContacts.Include(e => e.EntityContactType)
                                                 .Include(e => e.User)
                                                 .Include(e => e.Store)
                                                 .SingleAsync(e => e.EntityContactId == request.Id, cancellationToken);

        //return _mapper.Map<StoreContact, StoreContactVm>(entity);
        var data = _mapper.Map<StoreContact, StoreContactVm>(entity);

        var commTypeIds = await _context.EntityContactCommunicationTypes.Where(e => e.EntityContactId == request.Id)
                                                                        .Select(e => e.CommunicationTypeId)
                                                                        .ToListAsync(cancellationToken);

        if (commTypeIds != null && commTypeIds.Count > 0)
        {
            var commTypes = await _context.CommunicationTypes.Where(e => commTypeIds.Contains(e.CommunicationTypeId))
                                                             .ProjectTo<CommunicationTypeOption>(_mapper.ConfigurationProvider)
                                                             .ToListAsync(cancellationToken);

            data.CommunicationTypeIds = commTypes;
        }

        return data;
    }
}

public class SupplierContactVmQueryHandler : BaseQueryHandler, IRequestHandler<SupplierContactVmQuery, SupplierContactVm>
{
    public SupplierContactVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContactVm> Handle(SupplierContactVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContacts.Include(e => e.EntityContactType)
                                                    .Include(e => e.User)
                                                    .Include(e => e.Supplier)
                                                    .SingleAsync(e => e.EntityContactId == request.Id, cancellationToken);

        //return _mapper.Map<SupplierContact, SupplierContactVm>(entity);
        var data = _mapper.Map<SupplierContact, SupplierContactVm>(entity);

        var commTypeIds = await _context.EntityContactCommunicationTypes.Where(e => e.EntityContactId == request.Id)
                                                                        .Select(e => e.CommunicationTypeId)
                                                                        .ToListAsync(cancellationToken);

        if (commTypeIds != null && commTypeIds.Count > 0)
        {
            var commTypes = await _context.CommunicationTypes.Where(e => commTypeIds.Contains(e.CommunicationTypeId))
                                                             .ProjectTo<CommunicationTypeOption>(_mapper.ConfigurationProvider)
                                                             .ToListAsync(cancellationToken);

            data.CommunicationTypeIds = commTypes;
        }

        var regionIds = await _context.EntityContactRegions.Where(e => e.EntityContactId == request.Id)
                                                           .Select(e => e.EngageRegionId)
                                                           .ToListAsync(cancellationToken);

        if (regionIds != null && regionIds.Count > 0)
        {
            var queryable = _context.EngageRegions.Where(e => regionIds.Contains(e.Id));

            queryable = queryable.OrderBy(e => e.Name);

            var entityContactRegions = await queryable.Select(e => new OptionDto(e.Id, e.Name))
                                                    .ToListAsync(cancellationToken);

            data.EngageRegionIds = entityContactRegions;
        }

        return data;
    }
}
