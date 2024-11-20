namespace Engage.Application.Services.PaymentProofs.Commands;

public class PaymentProofFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record PaymentProofFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<PaymentProofFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(PaymentProofFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentProofs.SingleOrDefaultAsync(e => e.PaymentProofId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(PaymentProof),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class PaymentProofFileUploadValidator : FileUploadValidator<PaymentProofFileUploadCommand>
{
    public PaymentProofFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}