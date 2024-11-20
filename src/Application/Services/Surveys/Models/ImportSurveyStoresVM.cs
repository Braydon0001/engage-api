using Engage.Application.Services.ImportFiles;

namespace Engage.Application.Services.Surveys.Models;

public class ImportSurveyStoresVM
{
    public SurveyDto Survey { get; set; }
    public ICollection<OptionDto> StoreFormats { get; set; }
    public ICollection<ImportFileListDto> FileUploads { get; set; }
}
