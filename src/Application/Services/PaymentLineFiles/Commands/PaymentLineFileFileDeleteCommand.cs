namespace Engage.Application.Services.PaymentLineFiles.Commands;

public class PaymentLineFileFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record PaymentLineFileFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<PaymentLineFileFileDeleteCommand, bool>
{
    public async Task<bool> Handle(PaymentLineFileFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentLineFiles.SingleOrDefaultAsync(e => e.PaymentLineFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(PaymentLineFile), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class PaymentLineFileFileDeleteValidator : FileDeleteValidator<PaymentLineFileFileDeleteCommand>
{
    public PaymentLineFileFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}