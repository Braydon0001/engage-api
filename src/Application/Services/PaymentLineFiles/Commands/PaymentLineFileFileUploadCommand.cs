namespace Engage.Application.Services.PaymentLineFiles.Commands;

public class PaymentLineFileFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record PaymentLineFileFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<PaymentLineFileFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(PaymentLineFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentLineFiles.SingleOrDefaultAsync(e => e.PaymentLineFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(PaymentLineFile),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class PaymentLineFileFileUploadValidator : FileUploadValidator<PaymentLineFileFileUploadCommand>
{
    public PaymentLineFileFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}