using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Surveys.Commands;

public class BatchAssignSurveyEngageRegionCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
    public List<int> EngageRegions { get; set; }
}

public class BatchAssignSurveyEngageRegionHandler : BaseCreateCommandHandler, IRequestHandler<BatchAssignSurveyEngageRegionCommand, OperationStatus>
{
    public BatchAssignSurveyEngageRegionHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(BatchAssignSurveyEngageRegionCommand command, CancellationToken cancellationToken)
    {
        var regionIds = await _context.SurveyEngageRegions
                                .Where(e => e.SurveyId == command.SurveyId)
                                .Select(e => e.EngageRegionId)
                                .ToListAsync(cancellationToken);

        await _mediator.Send(new BatchAssignCommand(
                AssignDesc.REGION_SURVEY, command.SurveyId, command.EngageRegions));

        if (regionIds != null && regionIds.Count > 0)
        {
            // If a region is removed, also remove any stores for the region 
            foreach (int regionId in regionIds)
            {
                if (!command.EngageRegions.Contains(regionId))
                {
                    var storeIds = await _context.SurveyStores
                                     .Where(e =>
                                            e.SurveyId == command.SurveyId &&
                                            e.Store.EngageRegionId == regionId)
                                     .Select(e => e.StoreId)
                                     .ToListAsync(cancellationToken);

                    if (storeIds != null && storeIds.Count > 0)
                    {
                        foreach (int storeId in storeIds)
                        {
                            await _mediator.Send(new UnassignCommand(
                                    AssignDesc.STORE_SURVEY,
                                    command.SurveyId,
                                    storeId,
                                    saveChanges: false));
                        }
                    }

                }
            }
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new OperationStatus()
        {
            Status = true
        };
    }
}
