namespace Engage.Application.Services.Surveys.Queries;

public static class SurveyPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new ("SurveyId") },
            {"title", new ("Title") },
            {"surveyTypeName", new ("SurveyTypeId", sortProperty: "SurveyType.Name") },
            {"engageSubGroupName", new ("EngageSubGroupId", sortProperty: "EngageSubGroup.Name") },
            {"supplierName", new ("Supplier.Name") },
            {"engageBrandName", new ("EngageBrandId") },
            {"startDate", new ("StartDate") },
            {"endDate", new ("EndDate") },
        };
    }
}
