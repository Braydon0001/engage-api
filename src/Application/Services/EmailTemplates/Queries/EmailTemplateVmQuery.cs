using Engage.Application.Services.EmailTemplates.Models;

namespace Engage.Application.Services.EmailTemplates.Queries
{
    public class EmailTemplateVmQuery : GetByIdQuery, IRequest<EmailTemplateVm>
    {
    }

    public class GetEmailTemplateVmQueryHandler : BaseQueryHandler, IRequestHandler<EmailTemplateVmQuery, EmailTemplateVm>
    {
        public GetEmailTemplateVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<EmailTemplateVm> Handle(EmailTemplateVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.EmailTemplates.Include(e => e.EmailTemplateType)
                                                      .Include(e => e.EmailType)
                                                      .SingleOrDefaultAsync(x => x.EmailTemplateId == request.Id, cancellationToken);

            return _mapper.Map<EmailTemplate, EmailTemplateVm>(entity);
        }
    }
}
