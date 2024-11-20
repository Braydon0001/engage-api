namespace Engage.Application.Services.SurveyFormQuestionTypes.Queries;

public class SurveyFormQuestionTypeDto : IMapFrom<SurveyFormQuestionType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionType, SurveyFormQuestionTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionTypeId));
    }
}
