namespace Engage.Application.Services.SurveyAnswers.Commands;

public class SurveyAnswerDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{

}
public class SurveyAnswerDeleteFileHandler : FileDeleteHandler, IRequestHandler<SurveyAnswerDeleteFileCommand, OperationStatus>
{
    public SurveyAnswerDeleteFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(SurveyAnswerDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyAnswers.SingleOrDefaultAsync(e => e.SurveyAnswerId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(SurveyAnswer), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}
public class SurveyAnswerDeleteFileValdiator : FileDeleteValidator<SurveyAnswerDeleteFileCommand>
{
    public SurveyAnswerDeleteFileValdiator()
    {
    }
}