using Engage.Application.Services.SurveyAnswers.Models;

namespace Engage.Application.Services.SurveyInstances.Models
{
    public class SurveyInstanceVM
    {
        public SurveyInstanceDto SurveyInstance { get; set; }
        public ICollection<OptionDto> Employees { get; set; }
        public ICollection<OptionDto> Stores { get; set; }
        public ICollection<OptionDto> Surveys { get; set; }        
        public ListResult<SurveyAnswerListItemDto> Answers { get; set; }
    }
}
