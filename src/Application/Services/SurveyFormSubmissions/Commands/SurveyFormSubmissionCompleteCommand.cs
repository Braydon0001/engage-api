using Engage.Application.Services.EmployeeStoreCalendars.Queries;
using Engage.Application.Services.SurveyInstances.Commands;

namespace Engage.Application.Services.SurveyFormSubmissions.Commands;

public class SurveyFormSubmissionCompleteCommand : IMapTo<SurveyFormSubmission>, IRequest<SurveyFormSubmission>
{
    public int? SurveyFormSubmissionId { get; set; }
    public int? EmployeeId { get; init; }
    public int SurveyFormId { get; init; }
    public int? StoreId { get; init; }
    public string SubmissionUuid { get; init; }
    public DateTime CompletedDate { get; init; }
    public string Note { get; init; }
    public List<OptionDto> EmailAddresses { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmissionCompleteCommand, SurveyFormSubmission>();
    }
}

public record SurveyFormSubmissionCompleteHandler(IAppDbContext Context, IMapper Mapper, IHttpClientFactory httpClientFactory, IMediator mediator) : IRequestHandler<SurveyFormSubmissionCompleteCommand, SurveyFormSubmission>
{
    public async Task<SurveyFormSubmission> Handle(SurveyFormSubmissionCompleteCommand command, CancellationToken cancellationToken)
    {


        SurveyFormSubmission entity;
        if (command.SurveyFormSubmissionId == null)
        {
            entity = await Context.SurveyFormSubmissions.SingleOrDefaultAsync(e => e.SubmissionUuid == command.SubmissionUuid, cancellationToken);
        }
        else
        {
            entity = await Context.SurveyFormSubmissions.SingleOrDefaultAsync(e => e.SurveyFormSubmissionId == command.SurveyFormSubmissionId, cancellationToken);
        }

        if (entity == null)
        {
            return null;
        }

        entity.IsComplete = true;
        entity.CompletedDate = command.CompletedDate;
        entity.Note = command.Note;

        //var mappedEntity = Mapper.Map(command, entity);

        var surveyFormType = await Context.SurveyForms.FirstOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);

        if (surveyFormType != null && surveyFormType.SurveyFormTypeId == (int)SurveyFormTypeId.ContactReport)
        {
            var employeeStoreCalendarId = await Context.EmployeeStoreCalendarSurveyFormSubmissions
                    .Where(e => e.SurveyFormSubmissionId == command.SurveyFormSubmissionId)
                .Select(e => e.EmployeeStoreCalendarId)
                    .FirstOrDefaultAsync(cancellationToken);

            var calendar = await Context.EmployeeStoreCalendars
                                    .Include(e => e.SurveyFormSubmissions)
                                    .ThenInclude(e => e.SurveyFormSubmission)
                                    .FirstOrDefaultAsync(e => e.EmployeeStoreCalendarId == employeeStoreCalendarId, cancellationToken);

            if (calendar == null)
            {
                return null;
            }

            var surveysComplete = calendar.SurveyFormSubmissions.Select(e => e.SurveyFormSubmission.IsComplete).ToList();

            if (!surveysComplete.Contains(false))
            {
                calendar.CompletionDate = DateTime.Now;
            }
        }

        await Context.SaveChangesAsync(cancellationToken);

        if (command.EmailAddresses.IsNotNullOrEmpty())
        {
            try
            {
                HttpClient httpClient = httpClientFactory.CreateClient();


                var employeeStoreCalendarId = await Context.EmployeeStoreCalendarSurveyFormSubmissions
                    .Where(e => e.SurveyFormSubmissionId == command.SurveyFormSubmissionId)
                .Select(e => e.EmployeeStoreCalendarId)
                    .FirstOrDefaultAsync(cancellationToken);

                var employeeStoreCalendar = await mediator.Send(new EmployeeStoreCalendarVmQuery { Id = employeeStoreCalendarId }, cancellationToken);
                var file = await mediator.Send(new EmployeeStoreCalendarGenerateSurveyFormPdfReportQuery
                { Id = employeeStoreCalendarId, HttpClient = httpClient, SubmissionIds = [entity.SurveyFormSubmissionId] }, cancellationToken);

                List<string> emails = [];
                if (command.EmailAddresses != null && command.EmailAddresses.Count > 0)
                {




                    emails.AddRange(command.EmailAddresses
                            .Select(e => e.Name.Split('-').Last().Trim())
                            .ToList());
                }

                await mediator.Send(new SurveyInstanceCompleteEmailCommand
                {
                    SurveyInstanceId = null,
                    EmployeeStoreCalendarId = employeeStoreCalendarId,
                    EmployeeEmail = employeeStoreCalendar.EmployeeEmail,
                    EmployeeName = employeeStoreCalendar.EmployeeId.Name,
                    StoreName = employeeStoreCalendar.StoreId.Name,
                    SurveyDate = employeeStoreCalendar.CalendarDate.ToShortDateTimeString(),
                    CompletionDate = DateTime.Now,
                    CcEmails = emails,
                    Attachment = file
                }, cancellationToken);
            }
            catch (Exception)
            {

            }
        }




        return entity;
    }
}

public class SurveyFormSubmissionCompleteValidator : AbstractValidator<SurveyFormSubmissionCompleteCommand>
{
    public SurveyFormSubmissionCompleteValidator()
    {
        RuleFor(x => x.SurveyFormSubmissionId).NotEmpty().GreaterThan(0).Unless(e => e.SubmissionUuid != null);
        RuleFor(e => e.SubmissionUuid).NotEmpty().Unless(e => e.SurveyFormSubmissionId != null);
        RuleFor(e => e.EmployeeId);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId);
    }
}