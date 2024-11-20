using Engage.Application.Services.ClaimFloatReports.Models;
using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.ClaimFloatReports.Commands;

public class GenerateClaimFloatTopUpHistoryReportCommand : GetQuery, IRequest<ReportListVM<ClaimFloatTopUpHistoryReportDto>>
{
    public List<int> EngageRegionIds { get; set; }
}

public class GenerateClaimFloatTopUpHistoryReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateClaimFloatTopUpHistoryReportCommand, ReportListVM<ClaimFloatTopUpHistoryReportDto>>
{
    public GenerateClaimFloatTopUpHistoryReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ReportListVM<ClaimFloatTopUpHistoryReportDto>> Handle(GenerateClaimFloatTopUpHistoryReportCommand command, CancellationToken cancellationToken)
    {
        var query = _context.ClaimFloatTopUpHistories.AsQueryable();

        if (command.EngageRegionIds != null && command.EngageRegionIds.Any())
        {
            query = query.Where(t => command.EngageRegionIds.Contains(t.ClaimFloat.EngageRegionId));
        }

        var data = await query
                            .ProjectTo<ClaimFloatTopUpHistoryReportDto>(_mapper.ConfigurationProvider)
                            .OrderByDescending(c => c.Id)
                            .ToListAsync();

        string reportName = "Claim Float Top Up History Report - " + DateTime.Now.ToString();

        return new ReportListVM<ClaimFloatTopUpHistoryReportDto>
        {
            Count = data.Count,
            ReportName = reportName,
            Data = data,
        };
    }
}
