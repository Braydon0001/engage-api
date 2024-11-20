using Engage.Application.Services.EmailTemplates.Models;

namespace Engage.Application.Services.EmailTemplates.Queries
{
    public class EmailTemplatesQuery : GetQuery, IRequest<ListResult<EmailTemplateDto>>
    {
        public int? EmailTemplateTypeId { get; set; }
    }

    public class GetEmailTemplatesQueryHandler : BaseQueryHandler, IRequestHandler<EmailTemplatesQuery, ListResult<EmailTemplateDto>>
    {
        public GetEmailTemplatesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmailTemplateDto>> Handle(EmailTemplatesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.EmailTemplates.AsQueryable();

            if (request.EmailTemplateTypeId.HasValue)
            {
                query = query.Where(e => e.EmailTemplateTypeId == request.EmailTemplateTypeId);
            }

            var entities = await query.OrderBy(e => e.EmailTemplateId)
                                      .ProjectTo<EmailTemplateDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

            return new ListResult<EmailTemplateDto>(entities);
        }
    }
}
