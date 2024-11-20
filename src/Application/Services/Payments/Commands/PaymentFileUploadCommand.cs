namespace Engage.Application.Services.Payments.Commands;

public class PaymentFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record PaymentFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<PaymentFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(PaymentFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Payments.SingleOrDefaultAsync(e => e.PaymentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Payment),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class PaymentFileUploadValidator : FileUploadValidator<PaymentFileUploadCommand>
{
    public PaymentFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}