namespace Engage.Application.Services.PosSurveys.Queries;

public class PosPaginatedSurveyAnswersQuery : IRequest<List<PosSurveyDto>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageSize { get; set; } = 100;
    public int PageNumber { get; set; } = 1;

}
public record PosSurveyAnswersListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PosPaginatedSurveyAnswersQuery, List<PosSurveyDto>>
{
    public async Task<List<PosSurveyDto>> Handle(PosPaginatedSurveyAnswersQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormAnswers.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.SurveyFormSubmission.SurveyForm.SurveyFormTypeId == (int)SurveyFormTypeId.PosUpdate);

        if (query.StartDate.HasValue)
        {
            var startDate = query.StartDate.Value.Date;
            queryable = queryable.Where(e => e.Created.Date >= startDate);
        }

        if (query.EndDate.HasValue)
        {
            var endDate = query.EndDate.Value.Date;
            queryable = queryable.Where(e => e.Created.Date <= endDate);
        }

        int skip = 0;

        if (query.PageNumber > 0)
        {
            skip = (query.PageNumber - 1) * query.PageSize;
        }

        var data = await queryable.OrderBy(e => e.SurveyFormAnswerId)
                                  .Skip(skip)
                                  .Take(query.PageSize)
                                  .ProjectTo<PosSurveyDto>(Mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        return data;
    }
}