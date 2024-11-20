namespace Engage.Application.Services.EmailTemplates.Queries;

public class EmailTemplateOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class EmailTemplateOptionsQueryHandler : IRequestHandler<EmailTemplateOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmailTemplateOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmailTemplateOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmailTemplates.Where(e => e.Disabled == false)
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.EmailTemplateId, e.Name))
                       .ToList();
    }
}
