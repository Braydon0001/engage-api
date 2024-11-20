namespace Engage.Application.Services.UserStores.Commands;

public class UserStoreInsertCommand : IMapTo<UserStore>, IRequest<OperationStatus>
{
    public int UserId { get; init; }
    public List<int> StoreIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserStoreInsertCommand, UserStore>();
    }
}

public record UserStoreInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserStoreInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(UserStoreInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.StoreIds == null || command.StoreIds.Count == 0)
        {
            throw new Exception("StoreIds is required");
        }

        foreach (var storeId in command.StoreIds)
        {
            var existing = await Context.UserStores
                .Where(e => e.UserId == command.UserId && e.StoreId == storeId)
                .FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                var entity = new UserStore
                {
                    UserId = command.UserId,
                    StoreId = storeId,
                };

                Context.UserStores.Add(entity);
            }
        }

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class UserStoreInsertValidator : AbstractValidator<UserStoreInsertCommand>
{
    public UserStoreInsertValidator()
    {
        RuleFor(e => e.UserId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreIds).NotEmpty();
    }
}