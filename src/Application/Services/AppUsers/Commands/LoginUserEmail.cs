namespace Engage.Application.Services.AppUsers.Commands;

public class LoginUserEmailCommand : IRequest<OperationStatus>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginUserEmailCommandHandler : IRequestHandler<LoginUserEmailCommand, OperationStatus>
{

    private readonly IAppDbContext _context;

    public LoginUserEmailCommandHandler(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<OperationStatus> Handle(LoginUserEmailCommand request, CancellationToken cancellationToken)
    {
        var opStatus = new OperationStatus { Status = false, Message = "Username or Password is incorrect" };

        if (request.Password != "engage123")
            return opStatus;

        var username = request.Username.ToLower().Trim();

        var employee = await _context.Employees
                                .FirstOrDefaultAsync(e =>
                                    e.EmailAddress1.ToLower().Trim() == username ||
                                    e.EmailAddress2.ToLower().Trim() == username, cancellationToken);

        if (employee == null)
            return opStatus;

        return new OperationStatus
        {
            Status = true,
            OperationId = employee.EmployeeId
        };
    }
}
