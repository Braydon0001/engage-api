namespace Engage.Application.Services.Users.Queries;

public record UserEmployee(int EmployeeId, string FullName, string PhotoUrl);

public class UserEmployeeIdQuery : IRequest<UserEmployee>
{
}

public class UserEmployeeHandler : IRequestHandler<UserEmployeeIdQuery, UserEmployee>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UserEmployeeHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<UserEmployee> Handle(UserEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.Disabled == false &&
                                                                 e.Email == _user.UserName,
                                                                 cancellationToken);

        if (user != null)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Disabled == false &&
                                                                             e.UserId == user.UserId,
                                                                             cancellationToken);
            if (employee != null)
            {
                var fullName = $"{employee.FirstName + employee.LastName}";
                var photoUrl = employee.Files?.Where(e => e.Type.ToLower() == "photo").FirstOrDefault()?.Url;

                return new UserEmployee(employee.EmployeeId, fullName, photoUrl);
            }
        }

        return null;
    }
}
