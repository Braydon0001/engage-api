using Engage.Application.Services.UserEntities.Models;

namespace Engage.Application.Services.UserEntitities.Queries;

public class UserEntitiesQuery : IRequest<ListResult<UserEntityDto>>
{
    public bool? FilterUser { get; set; } = true;
    public string Entity { get; set; }
}

public class UserEntitiesHandler : BaseQueryHandler, IRequestHandler<UserEntitiesQuery, ListResult<UserEntityDto>>
{
    private readonly IUserService _user;

    public UserEntitiesHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<ListResult<UserEntityDto>> Handle(UserEntitiesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.UserEntities.AsQueryable();

        if (request.FilterUser.HasValue && request.FilterUser.Value == true)
        {
            queryable = queryable.Where(e => e.User.Email == _user.UserName);
        }

        if (!string.IsNullOrEmpty(request.Entity))
        {
            queryable = queryable.Where(e => e.Entity == request.Entity);
        }

        var entities = await queryable.ProjectTo<UserEntityDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<UserEntityDto>(entities);
    }
}
