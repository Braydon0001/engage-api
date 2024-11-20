namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarEmailReportCommand : GetQuery, IRequest<bool>
{
    //public int[] SurveyInstanceIds { get; set; }
    public int[] Ids { get; set; }
    public string EmailAddress { get; set; }
}
public class EmployeeStoreCalendarEmailReportCommandHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreCalendarEmailReportCommand, bool>
{
    public EmployeeStoreCalendarEmailReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public Task<bool> Handle(EmployeeStoreCalendarEmailReportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}