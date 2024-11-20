namespace Engage.Application.Services.Employees.Queries;

public static class EmployeePaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new PaginationProperty("EmployeeId") },
            {"firstName", new PaginationProperty("FirstName") },
            {"lastName", new PaginationProperty("LastName") },
            {"code", new PaginationProperty("Code") },
            {"emailAddress1", new PaginationProperty("EmailAddress1") },
        };
    }
}
