namespace Engage.Application.Services.UserGroups.Queries;
public class UserGroupVendorIdQuery : IRequest<VendorIdVm>
{
    public int Id { get; set; }
}

public record UserGroupVendorIdHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserGroupVendorIdQuery, VendorIdVm>
{
    public async Task<VendorIdVm> Handle(UserGroupVendorIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await Context.UserGroups
            .FirstOrDefaultAsync(e => e.UserGroupId == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("UserGroup", request.Id);
        }

        var result = new VendorIdVm() { VendorId = entity.VendorId };

        return result;
    }
}

public class VendorIdVm
{
    public string VendorId { get; set; }
}
