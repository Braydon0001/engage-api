namespace Engage.Application.Services.EntityContacts.Commands;

// Commands
public class CreateEngageRegionContactCommand : EntityContactCommand, IRequest<OperationStatus>, IMapTo<EngageRegionContact>
{
    public int EngageRegionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateEngageRegionContactCommand, EngageRegionContact>();
    }
}

public class CreateStoreContactCommand : EntityContactCommand, IRequest<OperationStatus>, IMapTo<StoreContact>
{
    public int StoreId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateStoreContactCommand, StoreContact>();
    }
}

public class CreateSupplierContactCommand : EntityContactCommand, IRequest<OperationStatus>, IMapTo<SupplierContact>
{
    public int SupplierId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateSupplierContactCommand, SupplierContact>();
    }
}

// Handlers

public class CreateEngageRegionContactCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEngageRegionContactCommand, OperationStatus>
{
    public CreateEngageRegionContactCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateEngageRegionContactCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEngageRegionContactCommand, EngageRegionContact>(command);
        _context.EngageRegionContacts.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EntityContactId;
        return opStatus;
    }
}

public class CreateStoreContactCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateStoreContactCommand, OperationStatus>
{
    public CreateStoreContactCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateStoreContactCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateStoreContactCommand, StoreContact>(command);
        _context.StoreContacts.Add(entity);

        if (command.CommunicationTypeIds != null && command.CommunicationTypeIds.Count > 0)
        {
            foreach (var communicationTypeId in command.CommunicationTypeIds)
            {
                var entityContactCommunicationType = new EntityContactCommunicationType
                {
                    CommunicationTypeId = communicationTypeId,
                    EntityContact = entity,
                };

                _context.EntityContactCommunicationTypes.Add(entityContactCommunicationType);
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EntityContactId;
        return opStatus;
    }
}

public class CreateSupplierContactCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSupplierContactCommand, OperationStatus>
{
    public CreateSupplierContactCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateSupplierContactCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSupplierContactCommand, SupplierContact>(command);
        _context.SupplierContacts.Add(entity);

        if (command.CommunicationTypeIds != null && command.CommunicationTypeIds.Count > 0)
        {
            foreach (var communicationTypeId in command.CommunicationTypeIds)
            {
                var entityContactCommunicationType = new EntityContactCommunicationType
                {
                    CommunicationTypeId = communicationTypeId,
                    EntityContact = entity,
                };

                _context.EntityContactCommunicationTypes.Add(entityContactCommunicationType);
            }
        }

        if (command.EngageRegionIds != null && command.EngageRegionIds.Count > 0)
        {
            foreach (var regionId in command.EngageRegionIds)
            {
                var contactRegion = new EntityContactRegion
                {
                    EngageRegionId = regionId,
                    EntityContact = entity,
                };

                _context.EntityContactRegions.Add(contactRegion);
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EntityContactId;
        return opStatus;
    }
}
