namespace Engage.Application.Services.SurveyFormAnswers.Commands;

public class SurveyFormAnswerFileDeleteCommand : IRequest<BoolResult>
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
}

public record SurveyFormAnswerFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SurveyFormAnswerFileDeleteCommand, BoolResult>
{
    public async Task<BoolResult> Handle(SurveyFormAnswerFileDeleteCommand command, CancellationToken cancellationToken)
    {
        SurveyFormAnswer entity = null;
        var entityId = Int32.TryParse(command.Id, out int n) ? n : (int?)null;
        // if the id is not a number, we might have a guid
        if (entityId == null)
        {
            //try parse the id as a guid to make sure
            var isGuid = Guid.TryParse(command.Id, out Guid g);
            if (isGuid)
            {
                entity = await Context.SurveyFormAnswers.IgnoreQueryFilters().Where(e => e.AnswerUuid == command.Id).SingleOrDefaultAsync(cancellationToken);
            }
        }
        else
        {
            entity = await Context.SurveyFormAnswers.IgnoreQueryFilters().Where(e => e.SurveyFormAnswerId == entityId).FirstOrDefaultAsync(cancellationToken);
        }

        if (entity == null || entity.Files == null || !SurveyFileExists(entity.Files, new FileDeleteCommand { FileName = command.FileName, FileType = command.FileType, Id = entity.SurveyFormAnswerId }))
        {
            return new BoolResult()
            {
                Id = 0,
                Success = false,
            };
        }

        //get the file we are deleting as it is saved in the list of files
        var file = entity.Files.FirstOrDefault(e => e.Name[(e.Name.LastIndexOf("/") + 1)..].ToLower() == command.FileName[(command.FileName.LastIndexOf("/") + 1)..].ToLower());

        if (file == null)
        {
            return new BoolResult()
            {
                Id = 0,
                Success = false,
            };
        }

        await File.DeleteAsync(new FileDeleteCommand()
        {
            FileType = file.Type,
            Id = entity.SurveyFormAnswerId,
            FileName = file.Name,
        }, nameof(SurveyFormAnswer), cancellationToken);

        var currentFiles = new List<JsonFile>(entity.Files);

        var newFiles = currentFiles.RemoveFile(new FileDeleteCommand { FileName = file.Name, FileType = "", Id = entity.SurveyFormAnswerId });

        entity.Files = newFiles;

        await Context.SaveChangesAsync(cancellationToken);

        return new BoolResult()
        {
            Id = entity.SurveyFormAnswerId,
            Success = true,
        };
    }

    public static bool SurveyFileExists(List<JsonFile> files, FileDeleteCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.FileName))
        {
            throw new ArgumentNullException(nameof(command.FileName));
        }

        if (files == null)
        {
            return false;
        }

        var result = command.FileName[(command.FileName.LastIndexOf("/") + 1)..].ToLower();

        return files.Any(e => e.Name[(e.Name.LastIndexOf("/") + 1)..].ToLower() == result);
    }
}

public class SurveyFormAnswerFileDeleteValidator : AbstractValidator<SurveyFormAnswerFileDeleteCommand>
{
    public SurveyFormAnswerFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty();
    }
}

public class BoolResult
{
    public int Id { get; set; }
    public bool Success { get; set; }
}