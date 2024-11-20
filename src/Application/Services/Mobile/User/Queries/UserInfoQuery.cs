namespace Engage.Application.Services.Mobile.User.Queries;

public record UserInfo(int EmployeeId, int UserId, string FirstName, string LastName, string PhotoUrl);

public class UserInfoQuery : IRequest<UserInfo>
{
}

public record UserEmployeeHandler(IAppDbContext Context, IUserService User) : IRequestHandler<UserInfoQuery, UserInfo>
{
    public async Task<UserInfo> Handle(UserInfoQuery query, CancellationToken cancellationToken)
    {
        var user = await Context.Users.FirstOrDefaultAsync(e => e.Disabled == false &&
                                                                 e.Email == User.UserName,
                                                                 cancellationToken);

        if (user != null)
        {
            var employee = await Context.Employees.FirstOrDefaultAsync(e => e.Disabled == false &&
                                                                             e.UserId == user.UserId,
                                                                             cancellationToken);
            if (employee != null)
            {
                var photoUrl = employee.Files?.Where(e => e.Type.Equals("photo", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()?.Url;

                return new UserInfo(employee.EmployeeId, user.UserId, employee.FirstName, employee.LastName, photoUrl);
            }
            else
            {
                return new UserInfo(0, user.UserId, user.FirstName, user.LastName, null);
            }
        }

        return null;
    }

}