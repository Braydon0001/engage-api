using Engage.Application.Services.SurveyFormAnswers.Commands;
using Engage.Application.Services.SurveyFormAnswers.Queries;
using Engage.Application.Services.SurveyFormSubmissions.Queries;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO.Compression;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormAnswerController : BaseController
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SurveyFormAnswerController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyFormAnswerDto>>> List([FromQuery] SurveyFormAnswerListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SurveyFormAnswerDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyFormAnswerVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormAnswerVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("summary/{id}")]
    public async Task<ActionResult<SurveyFormSubmissionSummaryVm>> GetSurveySummary([FromRoute] SurveyFormSubmissionSummaryQuery query, [FromQuery] bool fullSurvey, CancellationToken cancellationToken)
    {
        query.FullSurvey = fullSurvey;
        var entity = await Mediator.Send(query, cancellationToken);
        return Ok(entity);
    }

    [HttpPost("survey/{id}/photos")]
    public async Task<ActionResult<ListResult<SurveyFormAnswerPhotoDto>>> GetAnswerPhotos([FromBody] SurveyFormAnswerPhotoQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpPost("download-photos")]
    public async Task<IActionResult> DownloadPhotos([FromBody] SurveyAnswerPhotoDownloadQuery query)
    {
        var memoryStream = new MemoryStream();
        var client = _httpClientFactory.CreateClient();

        var validImages = new List<ImageData>();
        var failedImages = new List<string>();

        // Check if image URLs are accessible
        foreach (var image in query.Images)
        {
            try
            {
                var response = await client.GetAsync(image.Url);
                if (response.IsSuccessStatusCode)
                {
                    validImages.Add(image);
                }
                else
                {
                    failedImages.Add($"Image {image.Name} failed to download with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                failedImages.Add($"Image {image.Name} failed to download. Error: {ex.Message}");
            }
        }

        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            foreach (var image in validImages)
            {
                try
                {
                    var response = await client.GetAsync(image.Url);
                    if (response.IsSuccessStatusCode)
                    {
                        if (!query.HasCaption)
                        {
                            var file = archive.CreateEntry(image.Name);
                            using (var entryStream = file.Open())
                            {
                                await response.Content.CopyToAsync(entryStream);
                            }
                        }
                        else
                        {
                            using (var stream = await response.Content.ReadAsStreamAsync())
                            {
                                using (var imageProcessor = Image.Load<Rgba32>(stream))
                                {
                                    var overlay = new Image<Rgba32>(imageProcessor.Width, imageProcessor.Height);

                                    var font = SystemFonts.CreateFont("Arial", 50);
                                    var textPositionX = 25;
                                    var textPositionY1 = imageProcessor.Height - 175;
                                    var textPositionY2 = imageProcessor.Height - 100;
                                    var gradientEndPosition = imageProcessor.Height - 300;

                                    if (imageProcessor.Width < 800)
                                    {
                                        font = SystemFonts.CreateFont("Arial", 24);
                                        textPositionY1 = imageProcessor.Height - 80;
                                        textPositionY2 = imageProcessor.Height - 40;
                                        gradientEndPosition = imageProcessor.Height - 150;
                                    }

                                    if (imageProcessor.Width < 1600)
                                    {
                                        font = SystemFonts.CreateFont("Arial", 28);
                                        textPositionY1 = imageProcessor.Height - 90;
                                        textPositionY2 = imageProcessor.Height - 50;
                                        gradientEndPosition = imageProcessor.Height - 160;
                                    }

                                    overlay.Mutate(ctx => ctx.Fill(
                                       new LinearGradientBrush(
                                           new Point(0, gradientEndPosition),
                                           new Point(0, imageProcessor.Height),
                                           GradientRepetitionMode.None,
                                           new ColorStop(0, Color.Transparent),
                                           new ColorStop(1, Color.Black))));

                                    imageProcessor.Mutate(ctx => ctx.DrawImage(overlay, PixelColorBlendingMode.Normal, PixelAlphaCompositionMode.SrcOver, 1));
                                    imageProcessor.Mutate(ctx => ctx.DrawText(String.Join(" - ", image.Metadata.StoreName, image.Metadata.RegionName.ToUpper()), font, Color.White, new PointF(textPositionX, textPositionY1)));
                                    imageProcessor.Mutate(ctx => ctx.DrawText(String.Join(" - ", image.Metadata.AnswerDate, image.Metadata.UserName), font, Color.White, new PointF(textPositionX, textPositionY2)));

                                    var file = archive.CreateEntry(image.Name);
                                    using (var entryStream = file.Open())
                                    {
                                        imageProcessor.SaveAsJpeg(entryStream);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    failedImages.Add($"Image {image.Name} failed to download. Error: {ex.Message}");
                }
            }

            // Add txt log file with failed image download errors
            if (failedImages.Count > 0)
            {
                var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmm");
                var readmeEntry = archive.CreateEntry($"failed_downloads_{timestamp}.txt");
                using (var entryStream = readmeEntry.Open())
                using (var streamWriter = new StreamWriter(entryStream))
                {
                    streamWriter.WriteLine("The following images failed to download:");
                    foreach (var error in failedImages)
                    {
                        streamWriter.WriteLine(error);
                    }
                }
            }
        }

        memoryStream.Position = 0; // Reset the stream position to the beginning
        return new FileStreamResult(memoryStream, "application/zip") { FileDownloadName = query.ZipName };
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormAnswerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormAnswerId));
    }

    [HttpPost("answer/next")]
    public async Task<IActionResult> PostUpdateNextAnswer([FromForm] SurveyFormAnswerUpdateAnswerNextCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormAnswerId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormAnswerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormAnswerId));
    }

    [HttpPut("answer")]
    public async Task<IActionResult> UpdateAnswer(SurveyFormAnswerUpdateAnswerCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormAnswerId));
    }

    [HttpPut("answer/next")]
    public async Task<IActionResult> UpdateNextAnswer([FromForm] SurveyFormAnswerUpdateAnswerNextCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormAnswerId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] SurveyFormAnswerFileUploadCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.File == null ? NotFound() : Ok(new OperationStatus(result.Id, result.File));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string filename, [FromQuery] string fileType, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SurveyFormAnswerFileDeleteWebCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(filename),
            FileType = !string.IsNullOrWhiteSpace(fileType) ? HttpUtility.UrlDecode(fileType) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
