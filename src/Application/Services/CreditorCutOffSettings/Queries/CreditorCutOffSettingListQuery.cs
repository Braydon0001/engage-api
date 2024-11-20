namespace Engage.Application.Services.CreditorCutOffSettings.Queries;

public class CreditorCutOffSettingListQuery : IRequest<List<CreditorCutOffSettingDto>>
{

}

public record CreditorCutOffSettingListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorCutOffSettingListQuery, List<CreditorCutOffSettingDto>>
{
    public async Task<List<CreditorCutOffSettingDto>> Handle(CreditorCutOffSettingListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorCutOffSettings.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.CreditorCutOffSettingId)
                              .ProjectTo<CreditorCutOffSettingDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}