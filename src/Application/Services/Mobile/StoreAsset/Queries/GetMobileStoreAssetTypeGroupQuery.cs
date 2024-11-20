namespace Engage.Application.Services.Mobile.StoreAsset.Queries
{
    public class GetMobileStoreAssetTypeGroupQuery : GetQuery, IRequest<ListResult<OptionDto>>
    {
        public int StoreId { get; set; }
    }

    public class GetMobileStoreAssetTypeGroupQueryHandler : BaseQueryHandler, IRequestHandler<GetMobileStoreAssetTypeGroupQuery, ListResult<OptionDto>>
    {
        public GetMobileStoreAssetTypeGroupQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<OptionDto>> Handle(GetMobileStoreAssetTypeGroupQuery request, CancellationToken cancellationToken)
        {

            var assetTypeIds = await _context.StoreAssets
                .Where(e => e.StoreId == request.StoreId)
                    .Select(s => s.StoreAssetTypeId)
                    .ToListAsync(cancellationToken);

            if (assetTypeIds.Any())
            {
                assetTypeIds = assetTypeIds.Distinct().ToList();

            }
            var queryable = _context.StoreAssetTypes.Where(e => assetTypeIds.Contains(e.Id));

            var entities = await queryable.OrderBy(o => o.Name).Select(e => new OptionDto(e.Id, e.Name))
                                .ToListAsync(cancellationToken);

            return new ListResult<OptionDto>(entities);
        }
    }


}
