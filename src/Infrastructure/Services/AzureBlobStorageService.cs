using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Engage.Domain.Entities.Json;

namespace Engage.Infrastructure.Services;

public class AzureBlobStorageService : IFileService
{
    private readonly AzureBlobStorageOptions _options;

    public AzureBlobStorageService(IOptions<AzureBlobStorageOptions> options)
    {
        _options = options.Value;
    }

    public async Task<JsonFile> UploadAsync(FileUploadCommand command, FileUploadOptions options, CancellationToken cancellationToken)
    {
        try
        {
            var fileName = GetFileName(command);

            if (!string.IsNullOrWhiteSpace(command.FileType)
                 && options.EntityFiles != null
                 && options.EntityFiles.Where(e => e.Type != null).Any(e => e.Type.ToLower() == command.FileType.ToLower())
                 && options.OverwriteType == true)
            {
                var deleteFileName = options.EntityFiles.First(e => e.Type.ToLower() == command.FileType.ToLower()).Name;
                await DeleteAsync(new FileDeleteCommand { FileName = deleteFileName }, options.ContainerName, cancellationToken);
            }
            else if (options.MaxFiles > 0 &&
                     options.EntityFiles != null &&
                     options.EntityFiles.Count >= options.MaxFiles &&
                     options.EntityFiles.Any(e => e.Name.ToLower() == fileName.ToLower()) == false)
            {
                throw new ApplicationException($"Too many files. Only {options.MaxFiles} files allowed.");
            }

            var uri = new Uri($"https://{_options.AccountName}.blob.core.windows.net/{options.ContainerName.ToLower()}/{fileName.ToLower()}");
            var client = new BlobClient(uri, CreateStorageSharedKeyCredential());

            await client.UploadAsync(command.File.OpenReadStream(), overwrite: true, cancellationToken);

            if (fileName.ToLower().EndsWith("pdf"))
            {
                await client.SetHttpHeadersAsync(new BlobHttpHeaders
                {
                    ContentType = "application/pdf"
                }, null, cancellationToken);
            }

            if (options.SetHeaders)
            {
                await client.SetHttpHeadersAsync(new BlobHttpHeaders
                {
                    ContentDisposition = "inline"
                }, null, cancellationToken);
            }

            return string.IsNullOrWhiteSpace(command.FileType)
                ? new JsonFile(fileName, uri.AbsoluteUri)
                : new JsonFile(fileName, uri.AbsoluteUri, command.FileType);

        }
        catch (RequestFailedException ex)
        {
            throw new FileStorageException(GetRequestFailedMessage(ex), ex);
        }
    }

    public async Task<bool> DeleteAsync(FileDeleteCommand command, string fileContainerName, CancellationToken cancellationToken)
    {
        var client = CreateBlobContainerClient(fileContainerName);

        try
        {
            var response = await client.DeleteBlobIfExistsAsync(command.FileName, DeleteSnapshotsOption.IncludeSnapshots, null, cancellationToken);
            return response.Value;
        }
        catch (RequestFailedException ex)
        {
            throw new FileStorageException(GetRequestFailedMessage(ex), ex);
        }
    }

    public async Task<List<JsonFile>> UpdateAsync(FileUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var resubmittedExistingFiles = command.Files != null ? command.Files.Where(f => f.FileName.ToLower().EndsWith("jsonfile")).ToList() : [];

            var newFiles = command.Files != null ? command.Files.Where(f => !f.FileName.ToLower().EndsWith("jsonfile")).ToList() : [];

            var existingFiles = new List<JsonFile>();
            var otherFiles = new List<JsonFile>();
            if (command.FileType != null)
            {
                existingFiles = command.EntityFiles != null ? command.EntityFiles.Where(f => f.Type.Equals(command.FileType, StringComparison.CurrentCultureIgnoreCase)).ToList() : [];
                otherFiles = command.EntityFiles != null ? command.EntityFiles.Where(f => !f.Type.Equals(command.FileType, StringComparison.CurrentCultureIgnoreCase)).ToList() : [];
            }
            else
            {
                existingFiles = command.EntityFiles != null ? [.. command.EntityFiles] : [];
                otherFiles = [];
            }

            var filesToDelete = existingFiles.Where(e => !resubmittedExistingFiles.Any(f => e.Name.ToLower().EndsWith(f.FileName[..f.FileName.IndexOf(" - jsonfile", StringComparison.CurrentCultureIgnoreCase)].ToLower()))).ToList();

            if (filesToDelete.Count > 0)
            {
                foreach (var file in filesToDelete)
                {
                    await DeleteAsync(new FileDeleteCommand { FileName = file.Name }, command.ContainerName, cancellationToken);
                    existingFiles.Remove(file);
                }
            }

            // Upload new files
            foreach (var newFile in newFiles)
            {
                var fileUploadCommand = new FileUploadCommand
                {
                    File = newFile,
                    Id = command.Id,
                    FileType = command.FileType
                };

                var options = new FileUploadOptions
                {
                    ContainerName = command.ContainerName,
                    EntityFiles = otherFiles,
                    MaxFiles = command.MaxFiles,
                    OverwriteType = command.OverwriteType,
                    SetHeaders = command.SetHeaders,
                };

                var uploadedFile = await UploadAsync(fileUploadCommand, options, cancellationToken);
                existingFiles.Add(uploadedFile);
            }

            var updatedFiles = existingFiles.Concat(otherFiles).ToList();

            return updatedFiles;
        }
        catch (RequestFailedException ex)
        {
            throw new FileStorageException(GetRequestFailedMessage(ex), ex);
        }
    }

    public string GetFileName(FileUploadCommand command)
    {
        var fileType = string.IsNullOrWhiteSpace(command.FileType) ? "" : "/" + command.FileType;

        return $"{command.Id}{fileType}/{command.File.FileName}".ToLower();
    }

    public string GetSas()
    {
        // Create an account Sas (Shared Access Signature) that is valid for one minute.
        var accountSasBuilder = new AccountSasBuilder()
        {
            Services = AccountSasServices.Blobs,
            ResourceTypes = AccountSasResourceTypes.Object,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1),
            Protocol = SasProtocol.Https,
        };
        accountSasBuilder.SetPermissions(AccountSasPermissions.Read);

        return accountSasBuilder.ToSasQueryParameters(CreateStorageSharedKeyCredential()).ToString();
    }

    private BlobContainerClient CreateBlobContainerClient(string fileContainerName)
    {
        var uri = new Uri($"https://{_options.AccountName}.blob.core.windows.net/{fileContainerName.ToLower()}");
        return new BlobContainerClient(uri, CreateStorageSharedKeyCredential());
    }

    private StorageSharedKeyCredential CreateStorageSharedKeyCredential()
    {
        return new StorageSharedKeyCredential(_options.AccountName, _options.AccountKey);
    }

    private static string GetRequestFailedMessage(RequestFailedException ex)
    {
        return ex?.Message?.Split('\n')[0];
    }
}

//public async Task<MemoryStream> DownloadAsync(int fileContainerId, string fileName, CancellationToken cancellationToken)
//{
//    var client = await CreateBlobContainerClient(fileContainerId, cancellationToken);
//    var blob = client.GetAppendBlobClient(fileName);

//    try
//    {
//        var stream = new MemoryStream();
//        await blob.DownloadToAsync(stream, cancellationToken);
//        return stream;
//    }
//    catch (RequestFailedException ex)
//    {
//        throw new FileStorageException(GetRequestFailedMessage(ex), ex);
//    }
//}