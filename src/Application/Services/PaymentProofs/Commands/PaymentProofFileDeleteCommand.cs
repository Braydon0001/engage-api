namespace Engage.Application.Services.PaymentProofs.Commands;

public class PaymentProofFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record PaymentProofFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<PaymentProofFileDeleteCommand, bool>
{
    public async Task<bool> Handle(PaymentProofFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentProofs.SingleOrDefaultAsync(e => e.PaymentProofId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(PaymentProof), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class PaymentProofFileDeleteValidator : FileDeleteValidator<PaymentProofFileDeleteCommand>
{
    public PaymentProofFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}