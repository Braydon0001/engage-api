namespace Engage.Application.Services.SurveyForms.Queries;
public static class SurveyFormPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new ("SurveyFormId") },
            {"title", new ("Title") },
            {"surveyFormTypeName", new ("SurveyFormTypeId", sortProperty: "SurveyFormType.Name") },
            {"engageSubGroupName", new ("EngageSubGroupId", sortProperty: "EngageSubGroup.Name") },
            {"supplierName", new ("Supplier.Name") },
            {"engageBrandName", new ("EngageBrandId.Name") },
            {"isEmployeeSurvey", new ("IsEmployeeSurvey") },
            {"isStoreRecurring", new ("IsStoreRecurring") },
            {"isRequired", new ("IsRequired") },
            {"isRecurring", new ("IsRecurring") },
            {"startDate", new ("StartDate.Value.Date") },
            {"endDate", new ("EndDate.Value.Date") },
        };
    }
}

