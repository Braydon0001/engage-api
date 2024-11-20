namespace Engage.Application.Services.CommunicationTemplateTypes.Queries;

public record CommunicationTemplateTypeVmQuery(int Id) : IRequest<CommunicationTemplateTypeVm>;

public record CommunicationTemplateTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateTypeVmQuery, CommunicationTemplateTypeVm>
{
    public async Task<CommunicationTemplateTypeVm> Handle(CommunicationTemplateTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTemplateTypes.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.CommunicationTemplateTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CommunicationTemplateTypeVm>(entity);
    }
}