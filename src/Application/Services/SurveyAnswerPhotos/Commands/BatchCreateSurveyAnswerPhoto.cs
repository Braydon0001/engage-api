using Azure.Storage;
using Azure.Storage.Blobs;

namespace Engage.Application.Services.SurveyAnswerPhotos.Commands
{
    public class BatchCreateSurveyAnswerPhoto
    {
        public string FileName { get; set; }
        public string Folder { get; set; }
        public string Base64String { get; set; }
        public bool IsNewApp { get; set; }
    }

    public class BatchCreateSurveyAnswerPhotoCommand : IRequest<OperationStatus>
    {
        public int SurveyAnswerId { get; set; }
        public string SurveyPhotoFolder { get; set; }
        public string SurveyPhotoFolderPath { get; set; }
        public List<BatchCreateSurveyAnswerPhoto> Photos { get; set; }
    }

    public class BatchCreateSurveyAnswerPhotoCommandHandler : BaseCreateCommandHandler, IRequestHandler<BatchCreateSurveyAnswerPhotoCommand, OperationStatus>
    {
        private readonly IImageService _imageService;
        private readonly AzureBlobStorageOptions _options;
        public BatchCreateSurveyAnswerPhotoCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IImageService imageService, IOptions<AzureBlobStorageOptions> options) : base(context, mapper, mediator)
        {
            _imageService = imageService;
            _options = options.Value;
        }

        public async Task<OperationStatus> Handle(BatchCreateSurveyAnswerPhotoCommand command, CancellationToken cancellationToken)
        {
            foreach (var photo in command.Photos)
            {
                await _mediator.Send(new CreateSurveyAnswerPhotoCommand()
                {
                    SurveyAnswerId = command.SurveyAnswerId,
                    FileName = photo.FileName,
                    Folder = command.SurveyPhotoFolder + photo.Folder,
                    SaveChanges = false
                });

                if (photo.IsNewApp)
                {
                    var image = new Photo
                    {
                        Base64String = photo.Base64String,
                        Folder = photo.Folder,
                        Id = photo.FileName.Split("/").Last(),
                    };

                    byte[] bytes = Convert.FromBase64String(image.Base64String);
                    MemoryStream stream = new MemoryStream(bytes);

                    var uri = new Uri($"https://{_options.AccountName}.blob.core.windows.net/surveyphoto{image.Folder + "/" + image.Id}");
                    var client = new BlobClient(uri, CreateStorageSharedKeyCredential());

                    await client.UploadAsync(stream, overwrite: true, cancellationToken);
                    //await _imageService.CreatePhoto(command.SurveyPhotoFolderPath, image, cancellationToken);
                }


            }

            return new OperationStatus()
            {
                Status = true
            };
        }
        private StorageSharedKeyCredential CreateStorageSharedKeyCredential()
        {
            return new StorageSharedKeyCredential(_options.AccountName, _options.AccountKey);
        }
    }
}
