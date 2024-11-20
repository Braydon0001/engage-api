namespace Engage.Application.Services.PaymentLineFiles.Commands;

public class PaymentLineFileUpdateCommand : IMapTo<PaymentLineFile>, IRequest<PaymentLineFile>
{
    public int Id { get; set; }
    public int PaymentLineId { get; init; }
    public int PaymentLineFileTypeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFileUpdateCommand, PaymentLineFile>();
    }
}

public record PaymentLineFileUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileUpdateCommand, PaymentLineFile>
{
    public async Task<PaymentLineFile> Handle(PaymentLineFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentLineFiles.SingleOrDefaultAsync(e => e.PaymentLineFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentLineFileValidator : AbstractValidator<PaymentLineFileUpdateCommand>
{
    public UpdatePaymentLineFileValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentLineId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentLineFileTypeId).NotEmpty().GreaterThan(0);
    }
}