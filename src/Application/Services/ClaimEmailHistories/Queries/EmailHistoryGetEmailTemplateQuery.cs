using Engage.Application.Services.ClaimEmailHistories.Models;

namespace Engage.Application.Services.ClaimEmailHistories.Queries;

public class EmailHistoryGetEmailTemplateQuery : IRequest<List<EmailHistoryGetEmailTemplateDto>>
{
    public int[] EmailHistoryIDs { get; set; }
}
public class EmailHistoryGetEmailTemplateQueryHandler : BaseQueryHandler, IRequestHandler<EmailHistoryGetEmailTemplateQuery, List<EmailHistoryGetEmailTemplateDto>>
{
    public EmailHistoryGetEmailTemplateQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmailHistoryGetEmailTemplateDto>> Handle(EmailHistoryGetEmailTemplateQuery request, CancellationToken cancellationToken)
    {
        List<EmailHistoryGetEmailTemplateDto> emails = new List<EmailHistoryGetEmailTemplateDto>();
        foreach (var emailHistoryId in request.EmailHistoryIDs)
        {
            var emailHistoryEntry = await _context.EmailHistories
                .AsNoTracking()
                .Where(e => emailHistoryId == e.EmailHistoryId)
                .Include(e => e.EmailHistoryTemplateVariables)
                .Include(e => e.EmailTemplate)
                .SingleOrDefaultAsync(cancellationToken);
            var templateVariables = emailHistoryEntry.EmailHistoryTemplateVariables.FirstOrDefault();

            var emailHistory = _mapper.Map<EmailHistoryGetEmailTemplateDto>(emailHistoryEntry);
            emailHistory.EmailHistoryTemplateVariables = templateVariables;

            if (emailHistoryEntry == null)
            {
                throw new NotFoundException("Email History item not found", emailHistoryId);
            }
            emails.Add(emailHistory);
        }
        return emails;
    }
}