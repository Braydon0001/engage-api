namespace Engage.Application.Services.SurveyQuestionOptions.Models;

public class SurveyQuestionOptionDto : IMapFrom<SurveyQuestionOption>
{
    public int Id { get; set; }
    public int SurveyQuestionId { get; set; }
    public string Option { get; set; }
    public int DisplayOrder { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyQuestionOption, SurveyQuestionOptionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(d => d.SurveyQuestionOptionId));
    }
}

public class SurveyQuestionOptionDto2 : IMapFrom<SurveyQuestionOption>
{
    public int SurveyQuestionOptionId { get; set; }
    public string Option { get; set; }
    public int DisplayOrder { get; set; }
    public bool CompleteSurvey { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyQuestionOption, SurveyQuestionOptionDto2>();
    }
}
