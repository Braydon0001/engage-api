namespace Engage.Application.Services.EntityContacts.Commands;

// Commands

public class UpdateEngageRegionContactCommand : EntityContactCommand, IRequest<OperationStatus>, IMapTo<EngageRegionContact>
{
    public int Id { get; set; }
    public int EngageRegionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateEngageRegionContactCommand, EngageRegionContact>();
    }
}

public class UpdateStoreContactCommand : EntityContactCommand, IRequest<OperationStatus>, IMapTo<StoreContact>
{
    public int Id { get; set; }
    public int StoreId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateStoreContactCommand, StoreContact>();
    }
}

public class UpdateSupplierContactCommand : EntityContactCommand, IRequest<OperationStatus>, IMapTo<SupplierContact>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateSupplierContactCommand, SupplierContact>();
    }
}

// Handlers

public class UpdateEngageRegionContactCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEngageRegionContactCommand, OperationStatus>
{
    public UpdateEngageRegionContactCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEngageRegionContactCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageRegionContacts.SingleAsync(x => x.EntityContactId == command.Id);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EntityContactId;
        return opStatus;
    }
}

public class UpdateStoreContactCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateStoreContactCommand, OperationStatus>
{
    public UpdateStoreContactCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateStoreContactCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreContacts.SingleAsync(x => x.EntityContactId == command.Id);
        _mapper.Map(command, entity);

        var entityContactCommunicationTypes = await _context.EntityContactCommunicationTypes.Where(e => e.EntityContactId == command.Id)
                                                                                            .ToListAsync(cancellationToken);
        if (command.CommunicationTypeIds != null && command.CommunicationTypeIds.Count > 0)
        {
            var communicationTypeIds = entityContactCommunicationTypes.Select(e => e.CommunicationTypeId)
                                                                      .ToList();
            var communicationTypesToRemove = entityContactCommunicationTypes.Where(e => !command.CommunicationTypeIds.Contains(e.CommunicationTypeId))
                                                                            .ToList();
            if (communicationTypesToRemove.Count > 0)
            {
                _context.EntityContactCommunicationTypes.RemoveRange(communicationTypesToRemove);
            }

            var communicationTypesToAdd = command.CommunicationTypeIds.Where(e => !communicationTypeIds.Contains(e))
                                                                      .ToList();
            if (communicationTypesToAdd.Count > 0)
            {
                foreach (var communicationTypeId in communicationTypesToAdd)
                {
                    var entityContactCommunicationType = new EntityContactCommunicationType
                    {
                        EntityContactId = command.Id,
                        CommunicationTypeId = communicationTypeId,
                    };
                    _context.EntityContactCommunicationTypes.Add(entityContactCommunicationType);
                }
            }
        }
        else
        {
            if (entityContactCommunicationTypes.Count > 0)
            {
                _context.EntityContactCommunicationTypes.RemoveRange(entityContactCommunicationTypes);
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EntityContactId;
        return opStatus;
    }
}

public class UpdateSupplierContactCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSupplierContactCommand, OperationStatus>
{
    public UpdateSupplierContactCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateSupplierContactCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContacts.SingleAsync(x => x.EntityContactId == command.Id);
        _mapper.Map(command, entity);

        var entityContactCommunicationTypes = await _context.EntityContactCommunicationTypes.Where(e => e.EntityContactId == command.Id)
                                                                                            .ToListAsync(cancellationToken);
        if (command.CommunicationTypeIds != null && command.CommunicationTypeIds.Count > 0)
        {
            var communicationTypeIds = entityContactCommunicationTypes.Select(e => e.CommunicationTypeId)
                                                                      .ToList();
            var communicationTypesToRemove = entityContactCommunicationTypes.Where(e => !command.CommunicationTypeIds.Contains(e.CommunicationTypeId))
                                                                            .ToList();
            if (communicationTypesToRemove.Count > 0)
            {
                _context.EntityContactCommunicationTypes.RemoveRange(communicationTypesToRemove);
            }

            var communicationTypesToAdd = command.CommunicationTypeIds.Where(e => !communicationTypeIds.Contains(e))
                                                                      .ToList();
            if (communicationTypesToAdd.Count > 0)
            {
                foreach (var communicationTypeId in communicationTypesToAdd)
                {
                    var entityContactCommunicationType = new EntityContactCommunicationType
                    {
                        EntityContactId = command.Id,
                        CommunicationTypeId = communicationTypeId,
                    };
                    _context.EntityContactCommunicationTypes.Add(entityContactCommunicationType);
                }
            }
        }
        else
        {
            if (entityContactCommunicationTypes.Count > 0)
            {
                _context.EntityContactCommunicationTypes.RemoveRange(entityContactCommunicationTypes);
            }
        }

        var entityContactRegions = await _context.EntityContactRegions.Where(e => e.EntityContactId == command.Id)
                                                                      .ToListAsync(cancellationToken);
        if (command.EngageRegionIds != null && command.EngageRegionIds.Count > 0)
        {
            var regionIds = entityContactRegions.Select(e => e.EngageRegionId)
                                                .ToList();
            var regionsToRemove = entityContactRegions.Where(e => !command.EngageRegionIds.Contains(e.EngageRegionId))
                                                      .ToList();
            if (regionsToRemove.Count > 0)
            {
                _context.EntityContactRegions.RemoveRange(regionsToRemove);
            }

            var regionsToAdd = command.EngageRegionIds.Where(e => !regionIds.Contains(e))
                                                                 .ToList();
            if (regionsToAdd.Count > 0)
            {
                foreach (var regionId in regionsToAdd)
                {
                    var entityContactRegion = new EntityContactRegion
                    {
                        EntityContactId = command.Id,
                        EngageRegionId = regionId,
                    };
                    _context.EntityContactRegions.Add(entityContactRegion);
                }
            }
        }
        else
        {
            if (entityContactRegions.Count > 0)
            {
                _context.EntityContactRegions.RemoveRange(entityContactRegions);
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EntityContactId;
        return opStatus;
    }
}
