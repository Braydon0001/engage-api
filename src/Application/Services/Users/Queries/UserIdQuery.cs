namespace Engage.Application.Services.Users.Queries;

public class UserIdQuery : IRequest<int>
{
    public string Email { get; set; }
}

public class UserIdQueryHandler : IRequestHandler<UserIdQuery, int>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UserIdQueryHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<int> Handle(UserIdQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_user.UserName))
        {
            return await _context.Users.Where(e => e.Email == _user.UserName)
                                   .Select(e => e.UserId)
                                   .FirstOrDefaultAsync(cancellationToken);
        }
        return 0;
    }
}
