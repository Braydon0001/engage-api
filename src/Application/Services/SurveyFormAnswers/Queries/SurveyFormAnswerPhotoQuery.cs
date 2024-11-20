namespace Engage.Application.Services.SurveyFormAnswers.Queries;

public class SurveyFormAnswerPhotoQuery : PaginatedQuery, IRequest<List<SurveyFormAnswerPhotoDto>>
{
    public int surveyQuestionId { get; set; }
    public int SurveyFormQuestionTypeId { get; set; }
    public string Search { get; set; }
    public int? RegionId { get; set; }
    public int? StoreFormatId { get; set; }
    public int? StoreId { get; set; }
    public int? StoreClusterId { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? EmployeeId { get; set; }
};

public record SurveyFormAnswerPhotoQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerPhotoQuery, List<SurveyFormAnswerPhotoDto>>
{
    public async Task<List<SurveyFormAnswerPhotoDto>> Handle(SurveyFormAnswerPhotoQuery query, CancellationToken cancellationToken)
    {
        var emptyFilters = SurveyFormAnswerPhotoFilters.CreateEmpty();
        var filters = SurveyFormAnswerPhotoFilters.Create();

        var queryable = Context.SurveyFormAnswers.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SurveyFormSubmission);

        queryable = queryable.Where(e => e.SurveyFormQuestionId == query.surveyQuestionId
                                      && e.SurveyFormQuestion.SurveyFormQuestionType.SurveyFormQuestionTypeId == query.SurveyFormQuestionTypeId);

        if (!query.Search.IsNullOrEmpty())
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.SurveyFormSubmission.Employee.FirstName, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.SurveyFormSubmission.Employee.LastName, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.SurveyFormSubmission.Employee.Code, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.SurveyFormSubmission.Store.StoreFormat.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.SurveyFormSubmission.Store.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.SurveyFormSubmission.Store.EngageRegion.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.SurveyFormQuestion.QuestionText, $"%{query.Search}%"))
                                       );
        }

        if (query.RegionId.HasValue)
        {
            queryable = queryable.Where(e => e.SurveyFormSubmission.Store.EngageRegionId == query.RegionId.Value);
        }

        if (query.StoreFormatId.HasValue)
        {
            queryable = queryable.Where(e => e.SurveyFormSubmission.Store.StoreFormatId == query.StoreFormatId.Value);
        }

        if (query.StoreClusterId.HasValue)
        {
            queryable = queryable.Where(e => e.SurveyFormSubmission.Store.StoreClusterId == query.StoreClusterId.Value);
        }

        if (query.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.SurveyFormSubmission.StoreId == query.StoreId.Value);
        }

        if (query.Year.HasValue)
        {
            queryable = queryable.Where(e => e.AnswerDate.HasValue && e.AnswerDate.Value.Year == query.Year.Value);
        }

        if (query.Month.HasValue)
        {
            queryable = queryable.Where(e => e.AnswerDate.HasValue && e.AnswerDate.Value.Month == query.Month.Value);
        }

        if (query.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.SurveyFormSubmission.EmployeeId == query.EmployeeId.Value);
        }

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.AnswerDate.Value.Date);
        }
        else
        {
            var answerDateSort = query.SortModel.SingleOrDefault(x => x.ColId == "answerDate");
            var storeSort = query.SortModel.SingleOrDefault(x => x.ColId == "storeName");
            var employeeNameCodeSort = query.SortModel.SingleOrDefault(x => x.ColId == "employeeNameCode");

            if (answerDateSort != null)
            {
                queryable = answerDateSort.Sort == "asc"
                    ? queryable.OrderBy(e => e.AnswerDate.Value.Date)
                    : queryable.OrderByDescending(e => e.AnswerDate.Value.Date);
            }
            else
            {
                queryable = queryable.OrderByDescending(e => e.AnswerDate.Value.Date);
            }

            // since store and employee are on the same level, you can only sort by one or the other
            if (storeSort != null)
            {
                queryable = storeSort.Sort == "asc"
                    ? ((IOrderedQueryable<SurveyFormAnswer>)queryable).ThenBy(f => f.SurveyFormSubmission.Store.Name)
                    : ((IOrderedQueryable<SurveyFormAnswer>)queryable).ThenByDescending(f => f.SurveyFormSubmission.Store.Name);

            }
            else if (employeeNameCodeSort != null)
            {
                queryable = employeeNameCodeSort.Sort == "asc"
                    ? ((IOrderedQueryable<SurveyFormAnswer>)queryable).ThenBy(f => f.SurveyFormSubmission.Employee.FirstName)
                    : ((IOrderedQueryable<SurveyFormAnswer>)queryable).ThenByDescending(f => f.SurveyFormSubmission.Employee.FirstName);
            }
            else
            {
                queryable = ((IOrderedQueryable<SurveyFormAnswer>)queryable).ThenBy(f => f.SurveyFormSubmission.Store.Name);
            }
        }

        var entities = await queryable.Paginate(query, emptyFilters)
                              .ProjectTo<SurveyFormAnswerPhotoDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        return entities;
    }
}
