namespace Engage.Application.Services.SurveyFormAnswers.Queries;

public class SurveyAnswerPhotoDownloadQuery
{
    public bool HasCaption { get; set; }
    public string ZipName { get; set; }
    public List<ImageData> Images { get; set; }
}

public class ImageData
{
    public Metadata Metadata { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}

public class Metadata
{
    public string StoreName { get; set; }
    public string AnswerDate { get; set; }
    public string UserName { get; set; }
    public string RegionName { get; set; }
}