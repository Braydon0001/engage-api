using Engage.Application.Services.SurveyQuestionOptions.Models;

namespace Engage.Application.Services.SurveyQuestions.Models;

public class SurveyQuestionDto : IMapFrom<SurveyQuestion>
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public int QuestionTypeId { get; set; }
    public string Question { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsRequired { get; set; }
    public ICollection<OptionDto> QuestionFalseReasons { get; set; }
    public ICollection<SurveyQuestionOptionListItemDto> SurveyQuestionOptions { get; set; }
    public int? OptionId1 { get; set; }
    public int? OptionId2 { get; set; }
    public int? OptionId3 { get; set; }
    public int? OptionId4 { get; set; }
    public int? OptionId5 { get; set; }
    public int? OptionId6 { get; set; }
    public int? OptionId7 { get; set; }
    public int? OptionId8 { get; set; }
    public int? OptionId9 { get; set; }
    public int? OptionId10 { get; set; }

    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string Option4 { get; set; }
    public string Option5 { get; set; }
    public string Option6 { get; set; }
    public string Option7 { get; set; }
    public string Option8 { get; set; }
    public string Option9 { get; set; }
    public string Option10 { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyQuestion, SurveyQuestionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(d => d.SurveyQuestionId))
            .ForMember(d => d.QuestionFalseReasons, opt => opt.MapFrom(d => d.SurveyQuestionFalseReasons
                                                                                .Select(s => s.QuestionFalseReason)
                                                                                .Select(s => new OptionDto()
                                                                                {
                                                                                    Id = s.Id,
                                                                                    Name = s.Name
                                                                                })))
            .ForMember(d => d.SurveyQuestionOptions, opt => opt.MapFrom(d => d.SurveyQuestionOptions
                                                                                .Select(s => new SurveyQuestionOptionListItemDto()
                                                                                {
                                                                                    Id = s.SurveyQuestionOptionId,
                                                                                    DisplayOrder = s.DisplayOrder,
                                                                                    Option = s.Option
                                                                                })));
    }
}
