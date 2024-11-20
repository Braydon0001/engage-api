namespace Engage.Application.Services.SurveyAnswerOptions.Models;

public class SurveyAnswerOptionDto : IMapFrom<SurveyAnswerOption>
{
    public int Id { get; set; }
    public int SurveyAnswerId { get; set; }
    public int SurveyQuestionOptionId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyAnswerOption, SurveyAnswerOptionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyAnswerOptionId));
    }
}

public class SurveyAnswerOptionDto2 : IMapFrom<SurveyAnswerOption>
{
    public int EmployeeStoreSurveyOptionId { get; set; }
    public int SurveyQuestionOptionId { get; set; }
    public string Option { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyAnswerOption, SurveyAnswerOptionDto2>()
            .ForMember(d => d.Option, opt => opt.MapFrom(s => s.SurveyQuestionOption.Option));
    }
}
