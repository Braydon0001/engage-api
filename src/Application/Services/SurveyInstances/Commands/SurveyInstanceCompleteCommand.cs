using Engage.Application.Services.EntityContacts.Models;

namespace Engage.Application.Services.SurveyInstances.Commands;

public class SurveyInstanceCompleteCommand : IRequest<SurveyInstance>
{
    public int Id { get; set; }
    public string Note { get; set; }
    public List<StoreContactEmailOption> EmailAddresses { get; set; }
}
public class SurveyInstanceCompleteHandler : UpdateHandler, IRequestHandler<SurveyInstanceCompleteCommand, SurveyInstance>
{
    public SurveyInstanceCompleteHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyInstance> Handle(SurveyInstanceCompleteCommand request, CancellationToken cancellationToken)
    {
        var survey = await _context.SurveyInstances
            .FirstOrDefaultAsync(e => e.SurveyInstanceId == request.Id, cancellationToken);

        if (survey == null)
        {
            return null;
        }

        survey.Note = request.Note;
        survey.IsCompleted = true;

        var employeeStoreCalendar = await _context.EmployeeStoreCalendars
                                                  .Where(e => e.SurveyInstanceId == request.Id)
                                                  .FirstOrDefaultAsync(cancellationToken);

        employeeStoreCalendar.CompletionDate = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return survey;
    }
}
public class SurveyInstanceCompleteValidator : AbstractValidator<SurveyInstanceCompleteCommand>
{
    public SurveyInstanceCompleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note);
    }
}