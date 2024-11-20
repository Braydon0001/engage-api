using Engage.Application.Services.AssetImages.Queries;
using Engage.Application.Services.StoreAssets.Models;

namespace Engage.Application.Services.Mobile.StoreAsset.Queries
{
    public class GetMobileStoreAssetSubTypeQuery : GetQuery, IRequest<ListResult<StoreAssetDto>>
    {
        public int StoreId { get; set; }
        public int AssetId { get; set; }
    }
    public class GetMobileStoreAssetSubTypeQueryHandler : BaseQueryHandler, IRequestHandler<GetMobileStoreAssetSubTypeQuery, ListResult<StoreAssetDto>>
    {
        private readonly IMediator _IMediator;
        public GetMobileStoreAssetSubTypeQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
        {
            _IMediator = mediator;
        }

        public async Task<ListResult<StoreAssetDto>> Handle(GetMobileStoreAssetSubTypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.StoreAssets.Where(e => e.StoreAssetTypeId == request.AssetId && e.StoreId == request.StoreId)
                                                     .OrderBy(e => e.Name)
                                                     .ProjectTo<StoreAssetDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);


            foreach (var asset in entities)
            {
                var files = await _IMediator.Send(new StoreAssetBlobsQuery { StoreAssetId = asset.Id }, cancellationToken);

                asset.Files = new List<JsonFile>();
                foreach (var file in files.Data)
                {
                    JsonFile _fileImage = new JsonFile(file.FileName, file.Url);

                    asset.Files.Add(_fileImage);
                }
            }


            return new ListResult<StoreAssetDto>(entities);
        }
    }

}
