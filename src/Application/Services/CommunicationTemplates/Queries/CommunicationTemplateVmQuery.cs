namespace Engage.Application.Services.CommunicationTemplates.Queries;

public record CommunicationTemplateVmQuery(int Id) : IRequest<CommunicationTemplateVm>;

public record CommunicationTemplateVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateVmQuery, CommunicationTemplateVm>
{
    public async Task<CommunicationTemplateVm> Handle(CommunicationTemplateVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTemplates.Include(e => e.CommunicationTemplateType)
                                                      .Include(e => e.CommunicationType)
                                                      .AsQueryable()
                                                      .AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.CommunicationTemplateId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CommunicationTemplateVm>(entity);
    }
}