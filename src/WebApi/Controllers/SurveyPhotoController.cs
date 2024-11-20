using Engage.Application.Interfaces;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyPhotoController : BaseController
{
    private readonly IImageService _imageService;
    private readonly IOptions<ImageOptions> _imageSettings;

    public SurveyPhotoController(IImageService imageService, IOptions<ImageOptions> imageSettings)
    {
        _imageService = imageService;
        _imageSettings = imageSettings;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Photo photo, CancellationToken token)
    {
        return Ok(await _imageService.CreatePhoto(_imageSettings.Value.SurveyPhotoFolderPath, photo, token));
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("[Action]")]
    public async Task<IActionResult> BatchCreate(PhotoList photolist, CancellationToken token)
    {
        return Ok(await _imageService.BatchCreatePhoto(_imageSettings.Value.SurveyPhotoFolderPath, photolist.Photos, token));
    }
}
