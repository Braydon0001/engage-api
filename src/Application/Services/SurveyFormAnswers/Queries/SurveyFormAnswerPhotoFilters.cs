namespace Engage.Application.Services.SurveyFormAnswers.Queries;

public class SurveyFormAnswerPhotoFilters
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "answerDate", new("AnswerDate") },
            { "employeeNameCode", new("EmployeeNameCode") },
        };
    }

    public static Dictionary<string, PaginationProperty> CreateEmpty()
    {
        return new()
        {
        };
    }
}
