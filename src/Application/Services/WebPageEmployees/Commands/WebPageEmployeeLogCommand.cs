// auto-generated
using Engage.Application.Services.WebPages.Commands;

namespace Engage.Application.Services.WebPageEmployees.Commands;

public class WebPageEmployeeLogCommand : IMapTo<WebPageEmployee>, IRequest<OperationStatus>
{
    public string Email { get; set; }
    public string Path { get; set; }
}

public class WebPageEmployeeLogHandler : BaseCreateCommandHandler, IRequestHandler<WebPageEmployeeLogCommand, OperationStatus>
{
    public WebPageEmployeeLogHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(WebPageEmployeeLogCommand command, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Where(x => x.EmailAddress1 == command.Email).FirstOrDefaultAsync(cancellationToken);

        if (employee == null)
        {
            throw new Exception("No Employee Found");
        }

        var webpage = await _context.WebPages.Where(x => x.Name == command.Path).FirstOrDefaultAsync(cancellationToken);

        if (webpage == null)
        {
            webpage = await _mediator.Send(new WebPageInsertCommand { Name = command.Path }, cancellationToken);
        }

        var webPageEmployee = await _mediator.Send(new WebPageEmployeeInsertCommand
        {
            EmployeeId = employee.EmployeeId,
            WebPageId = webpage.WebPageId,
            ViewDate = DateTime.Now
        }, cancellationToken);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = webPageEmployee.WebPageEmployeeId;
        return opStatus;
    }
}