namespace Engage.Application.Services.Users.Queries;

public static class UserPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new ("UserId") },
            { "firstName", new ("FirstName") },
            { "lastName", new ("LastName") },
            { "fullName", new("FullName") },
            { "email", new ("Email") },
            { "supplierName", new ("Supplier.Name") },
        };
    }
}
