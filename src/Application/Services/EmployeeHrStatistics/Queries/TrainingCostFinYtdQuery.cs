using Engage.Application.Services.EmployeeHrStatistics.Model;

namespace Engage.Application.Services.EmployeeHrStatistic.Queries;

public class TrainingCostFinYtdQuery : IRequest<StatCardDto>
{
    public int? RegionId { get; set; }
}

public record TrainingCostFinYtdQueryHandler(IAppDbContext Context, IMapper Mapper): IRequestHandler<TrainingCostFinYtdQuery, StatCardDto>
{
    public async Task<StatCardDto> Handle(TrainingCostFinYtdQuery query, CancellationToken cancellationToken)
    {
        var isBeforeOctober = DateTime.Now.Month < 10;
        var ytdStart = isBeforeOctober ? new DateTime(DateTime.Now.Year - 1, 10, 1) : new DateTime(DateTime.Now.Year, 10, 1);

        var trainingCosts = await Context.Trainings.Where(t => !t.Disabled && !t.Deleted 
                                                               && (!query.RegionId.HasValue || t.EngageRegionId == query.RegionId)
                                                               && t.EndDate > ytdStart
                                                               )
                                                   .AsNoTracking()
                                                   .ToListAsync(cancellationToken);

        var totalTrainingCost = trainingCosts.Sum(tc => tc.TotalCost);
        var trainingCostStatCard = new StatCardDto
        {
            Label = "Training Cost YTD",
            Value = (double)Math.Round(totalTrainingCost, 2)
        };

        return trainingCostStatCard;
    }
}
