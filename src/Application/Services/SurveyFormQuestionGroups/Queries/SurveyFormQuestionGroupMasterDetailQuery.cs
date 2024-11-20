namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public record SurveyFormQuestionGroupMasterDetailQuery(int SurveyId) : IRequest<List<SurveyFormQuestionGroupDto>>;

public record SurveyFormQuestionGroupMasterDetailHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionGroupMasterDetailQuery, List<SurveyFormQuestionGroupDto>>
{
    public async Task<List<SurveyFormQuestionGroupDto>> Handle(SurveyFormQuestionGroupMasterDetailQuery query, CancellationToken cancellationToken)
    {
        //get all groups for the survey
        var groups = await Context.SurveyFormQuestionGroups.Include(e => e.SurveyFormQuestions).Where(e => e.SurveyFormId == query.SurveyId).ToListAsync(cancellationToken);

        //get all virtual groups
        var virtualGroups = groups.Where(e => e.IsVirtualGroup).ToList();

        //get all actual groups
        var realGroups = groups.Where(e => !e.IsVirtualGroup).ToList();






        var entities = await Context.SurveyFormQuestionGroups
                                        .Include(e => e.SurveyForm)
                                        .Where(e => e.SurveyFormId == query.SurveyId)
                                        .OrderBy(e => e.DisplayOrder)
                                        .ProjectTo<SurveyFormQuestionGroupDto>(Mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);

        return entities == null ? null : entities;
    }
}

//public class Question
//{
//    public int SurveyId { get; set; }
//    public int QuestionId { get; set; }
//    public string Question { get; set; }
//    public int SurveyFormQuestionTypeId { get; init; }
//    public string SurveyFormQuestionTypeName { get; init; }
//    public int DisplayOrder { get; set; }
//}

//public class RealGroupMaster
//{
//    public int SurveyId { get; set; }
//    public bool IsVirtualGroup { get; set;}
//    public int GroupId { get; set; }
//    public string GroupName { get; set; }
//    public int DisplayOrder { get; set; }
//    public List<>
//}