using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Engage.Infrastructure.Services;

public class AzureBlobService : IBlobService
{
    private readonly AzureBlobOptions _options;

    public AzureBlobService(IOptions<AzureBlobOptions> options)
    {
        _options = options.Value;
    }

    public string CreateFileName(int id, string fileName)
    {
        return $"{id}/{fileName}";
    }

    public async Task<Uri> UploadAsync(Stream fileStream, string folderName, string fileName, CancellationToken cancellationToken)
    {
        try
        {
            fileStream.ThrowIfNull(nameof(fileStream));
            folderName.ThrowIfNullOrWhiteSpace(nameof(folderName));
            fileName.ThrowIfNullOrWhiteSpace(nameof(fileName));

            var uri = new Uri($"https://{_options.AccountName}.blob.core.windows.net/{folderName}/{fileName}");
            var client = new BlobClient(uri, CreateCredentials());

            await client.UploadAsync(fileStream, overwrite: true, cancellationToken);

            return uri;
        }
        catch (RequestFailedException ex)
        {
            throw new BlobException("Error Uploading Blob", ex);
        }
    }

    public async Task<bool> DeleteAsync(string folderName, string fileName, CancellationToken cancellationToken)
    {
        try
        {
            folderName.ThrowIfNullOrWhiteSpace(nameof(folderName));
            fileName.ThrowIfNullOrWhiteSpace(nameof(fileName));

            var uri = new Uri($"https://{_options.AccountName}.blob.core.windows.net/{folderName}");
            var client = new BlobContainerClient(uri, CreateCredentials());

            var response = await client.DeleteBlobIfExistsAsync(fileName, DeleteSnapshotsOption.IncludeSnapshots, null, cancellationToken);

            return response.Value;
        }
        catch (RequestFailedException ex)
        {
            throw new BlobException("Error Deleting Blob", ex);
        }
    }

    private StorageSharedKeyCredential CreateCredentials()
    {
        return new StorageSharedKeyCredential(_options.AccountName, _options.AccountKey);
    }


}
