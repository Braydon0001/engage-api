namespace Engage.Application.Services.SurveyAnswers.Commands;

public class SurveyAnswerUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{

}
public class SurveyAnswerUploadFileHandler : FileUploadHandler, IRequestHandler<SurveyAnswerUploadFileCommand, OperationStatus>
{
    public SurveyAnswerUploadFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(SurveyAnswerUploadFileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyAnswers.SingleOrDefaultAsync(e => e.SurveyAnswerId == request.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SurveyAnswer),
            EntityFiles = entity.Files,
            MaxFiles = 5,
            OverwriteType = false
        };

        var file = await _file.UploadAsync(request, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = request.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}
public class SurveyAnswerUploadFileValidator : FileUploadValidator<SurveyAnswerUploadFileCommand>
{
    public SurveyAnswerUploadFileValidator()
    {

    }
}

