using Engage.Application.Services.Shared.AssignCommands;
using Engage.Application.Targetings;

namespace Engage.Application.Services.SurveyStores;

// Commands

public class CreateSurveyStoresCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
    public List<int> Stores { get; set; }
}

public class CreateSurveyStoresWithCriteriaCommand : StoreTargetingCommand, IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
}

public class DeleteSurveyStoreCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
    public int StoreId { get; set; }
}

public class BatchDeleteSurveyStoresCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
}

// Handlers

public class CreateSurveyStoresHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyStoresCommand, OperationStatus>
{
    public CreateSurveyStoresHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateSurveyStoresCommand command, CancellationToken cancellationToken)
    {
        var currentStores = await _context.SurveyStores
                                    .Where(e => e.SurveyId == command.SurveyId
                                        && command.Stores.Contains(e.StoreId))
                                    .Select(e => e.StoreId)
                                    .ToListAsync();
        if (currentStores.Any())
        {
            // remove store ids from list of stores to add
            command.Stores = command.Stores.Except(currentStores).ToList();
            if (!command.Stores.Any())
            {
                throw new Exception("Survey already has targeted store(s).");
            }
        }

        var surveyStores = command.Stores.Select(storeId => new SurveyStore
        {
            SurveyId = command.SurveyId,
            StoreId = storeId
        });

        _context.SurveyStores.AddRange(surveyStores);

        return OperationStatus.EnsureTrue(await _context.SaveChangesAsync(cancellationToken));
    }
}

public class CreateSurveyStoresWithCriteriaHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyStoresWithCriteriaCommand, OperationStatus>
{
    private readonly ITargetingService _targeting;

    public CreateSurveyStoresWithCriteriaHandler(IAppDbContext context, IMapper mapper, ITargetingService targeting) : base(context, mapper)
    {
        _targeting = targeting;
    }

    public async Task<OperationStatus> Handle(CreateSurveyStoresWithCriteriaCommand command, CancellationToken cancellationToken)
    {
        // Add targeting criteria
        var targeting = new Targeting
        {
            Criteria = await _targeting.SerializeStoreCriteria(command, cancellationToken)
        };

        _context.Targetings.Add(targeting);

        await _context.SaveChangesAsync(cancellationToken);

        // Ignore stores that have already been added
        var existingStores = await _context.SurveyStores.Where(e => e.SurveyId == command.SurveyId)
                                                        .Select(e => e.StoreId)
                                                        .ToListAsync(cancellationToken);

        var stores = _context.Stores.Where(e => !existingStores.Contains(e.StoreId));

        if (command.EngageRegions != null && command.EngageRegions.Count > 0) { stores = stores.Where(e => command.EngageRegions.Contains(e.EngageRegionId)); }
        if (command.StoreClusters != null && command.StoreClusters.Count > 0) { stores = stores.Where(e => command.StoreClusters.Contains(e.StoreClusterId)); }
        if (command.StoreFormats != null && command.StoreFormats.Count > 0) { stores = stores.Where(e => command.StoreFormats.Contains(e.StoreFormatId)); }
        if (command.StoreLSMs != null && command.StoreLSMs.Count > 0) { stores = stores.Where(e => command.StoreLSMs.Contains(e.StoreLSMId)); }
        if (command.StoreTypes != null && command.StoreTypes.Count > 0) { stores = stores.Where(e => command.StoreTypes.Contains(e.StoreTypeId)); }

        // Add stores
        var surveyStores = await stores.Select(e => new SurveyStore
        {
            SurveyId = command.SurveyId,
            StoreId = e.StoreId,
            TargetingId = targeting.TargetingId
        })
                                       .ToListAsync(cancellationToken);

        _context.SurveyStores.AddRange(surveyStores);

        return OperationStatus.EnsureTrue(await _context.SaveChangesAsync(cancellationToken));
    }
}

public class DeleteSurveyStoreHandler : IRequestHandler<DeleteSurveyStoreCommand, OperationStatus>
{
    private readonly IMediator _mediator;

    public DeleteSurveyStoreHandler(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public async Task<OperationStatus> Handle(DeleteSurveyStoreCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UnassignCommand(AssignDesc.STORE_SURVEY, command.SurveyId, command.StoreId));

        return new OperationStatus()
        {
            Status = true,
            OperationId = command.SurveyId
        };
    }
}

public class BatchDeleteSurveyStoresHandler : IRequestHandler<BatchDeleteSurveyStoresCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public BatchDeleteSurveyStoresHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(BatchDeleteSurveyStoresCommand command, CancellationToken cancellationToken)
    {
        var entities = await _context.SurveyStores.Where(e => e.SurveyId == command.SurveyId)
                                                   .ToListAsync(cancellationToken);

        _context.SurveyStores.RemoveRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return new OperationStatus()
        {
            Status = true,
            OperationId = command.SurveyId
        };
    }
}
