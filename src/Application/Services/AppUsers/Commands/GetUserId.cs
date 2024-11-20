namespace Engage.Application.Services.AppUsers.Commands;

public class GetUserIdCommand : IRequest<OperationStatus>
{
    public string Username { get; set; }
}

public class GetUserIdCommandHandler : IRequestHandler<GetUserIdCommand, OperationStatus>
{

    private readonly IAppDbContext _context;

    public GetUserIdCommandHandler(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<OperationStatus> Handle(GetUserIdCommand request, CancellationToken cancellationToken)
    {
 
        var username = request.Username.ToLower().Trim();

        var employee = await _context.Employees
                                .FirstOrDefaultAsync(e =>
                                    e.EmailAddress1.ToLower().Trim() == username ||
                                    e.EmailAddress2.ToLower().Trim() == username, cancellationToken);

        if (employee == null)
            return new OperationStatus { Status = false, Message = "Username not found" };

        return new OperationStatus
        {
            Status = true,
            OperationId = employee.EmployeeId
        };
    }
}
