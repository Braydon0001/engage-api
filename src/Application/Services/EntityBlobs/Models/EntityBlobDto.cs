namespace Engage.Application.Services.EntityBlobs.Models;

public class EntityBlobDto
{
    public EntityBlobDto()
    {
    }

    public EntityBlobDto(EntityBlob entityBlob)
    {
        Id = entityBlob.EntityBlobId;
        FolderName = entityBlob.FolderName;
        OriginalFileName = entityBlob.OriginalFileName;
        FileName = entityBlob.FileName;
        Url = entityBlob.Url;
        Extension = StringUtils.GetFileExtension(FileName);
    }

    public EntityBlobDto(StoreAssetBlob assetImage)
    {
        Id = assetImage.StoreAssetBlobId;
        Url = assetImage.ImageUrl;
        FolderName = "assets";
        OriginalFileName = StringUtils.GetFileName(assetImage.ImageUrl);
        Extension = StringUtils.GetFileExtension(assetImage.ImageUrl);
        FileName = $"{Id}/{OriginalFileName}.{Extension}";
    }

    public EntityBlobDto(int id, string folderName, string originalFileName, string fileName, string url)
    {
        Id = id;
        FolderName = folderName;
        OriginalFileName = originalFileName;
        FileName = fileName;
        Url = url;
        Extension = StringUtils.GetFileExtension(fileName);
    }

    public int Id { get; set; }
    public string FolderName { get; set; }
    public string OriginalFileName { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public string Url { get; set; }
}


