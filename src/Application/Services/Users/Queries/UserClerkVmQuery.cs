using Engage.Application.Services.Users.Models;

namespace Engage.Application.Services.Users.Queries;

public class UserClerkVmQuery : IRequest<UserClerkVm>
{
    public int Id { get; set; }
}
public record UserClerkVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserClerkVmQuery, UserClerkVm>
{
    public async Task<UserClerkVm> Handle(UserClerkVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Users.AsNoTracking();

        queryable = queryable.Include(e => e.Supplier);

        var user = await queryable.FirstOrDefaultAsync(e => e.UserId == query.Id, cancellationToken)
            ?? throw new Exception("User not found");

        return Mapper.Map<UserClerkVm>(user);
    }
}
