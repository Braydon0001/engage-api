namespace Engage.Application.Services.Payments.Commands;

public class PaymentFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record PaymentFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<PaymentFileDeleteCommand, bool>
{
    public async Task<bool> Handle(PaymentFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Payments.SingleOrDefaultAsync(e => e.PaymentId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(Payment), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class PaymentFileDeleteValidator : FileDeleteValidator<PaymentFileDeleteCommand>
{
    public PaymentFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}