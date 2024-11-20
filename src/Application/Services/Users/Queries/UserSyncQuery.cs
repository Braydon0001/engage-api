using Engage.Application.Services.Users.Models;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using User = Engage.Domain.Entities.User;

namespace Engage.Application.Services.Users.Queries
{
    public class UserSyncQuery : IRequest<List<UserDto>>
    {
    }

    public class UserSyncQueryHandler : BaseQueryHandler, IRequestHandler<UserSyncQuery, List<UserDto>>
    {
        private readonly IOptions<JwtOptions> _options;
        public UserSyncQueryHandler(IAppDbContext context, IMapper mapper, IOptions<JwtOptions> options) : base(context, mapper)
        {
            _options = options;
        }

        public async Task<List<UserDto>> Handle(UserSyncQuery request, CancellationToken cancellationToken)
        {
            var client = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = _options.Value.Authority,
                Token = _options.Value.UsersApiToken
            });

            var dbUsers = _context.Users.AsQueryable();

            var people = await client.Users.ToListAsync();

            var notInDb = new List<IUserProfile>();

            var inDb = new List<User>();

            var duplicates = new List<List<User>>();

            foreach (var person in people)
            {
                var diff = dbUsers.Where(e => e.Email.ToLower() == person.Profile.Email.ToLower()).ToList();
                if (diff.Count > 1)
                {
                    duplicates.Add(diff);
                }
                else if (diff.Count == 1)
                {
                    inDb.Add(diff[0]);
                }
                else if (diff.Count == 0)
                {
                    notInDb.Add(person.Profile);
                }
                else
                {
                    throw new Exception("Comparison Error");
                }
            }

            var newUsers = new List<User>();

            var zwUsers = new List<IUserProfile>();

            var suppliers = new Dictionary<string, int>()
            {
                { "engage", 126  },
                { "distell", 18 },
                { "heineken", 49},
                { "encore", 59 },
                { "unbxd", 1326 }
            };

            foreach (var person in notInDb)
            {
                if (person.Organization == "sparzw")
                {
                    zwUsers.Add(person);
                }
                else
                {
                    var supplier = 0;
                    if (person["supplier"] != null)
                    {
                        if (suppliers.TryGetValue(person["supplier"].ToString(), out var supp))
                        {
                            supplier = supp;
                        }
                        else
                        {
                            supplier = 126;
                        }
                    }
                    else if (person["supplier"] == null)
                    {
                        supplier = 126;
                    }
                    else
                    {
                        throw new Exception("Okta creation error");
                    }

                    var user = new User
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Email = person.Email,
                        FullName = person.FirstName + " " + person.LastName,
                        MobilePhone = person.MobilePhone,
                        IgnoreOrderProductFilters = false,
                        SupplierId = supplier,
                    };

                    newUsers.Add(user);
                    _context.Users.Add(user);
                }

            }

            //var inOkta = new List<IUserProfile>();

            //var notInOkta = new List<User>();

            //foreach (var dbUser in dbUsers)
            //{
            //    var oktaUser = people.Where(e => e.Profile.Email.ToLower() == dbUser.Email.ToLower()).FirstOrDefault();
            //    if (oktaUser != null)
            //    {
            //        inOkta.Add(oktaUser.Profile);
            //    }
            //    else if (oktaUser == null)
            //    {
            //        notInOkta.Add(dbUser);
            //    }
            //    else
            //    {
            //        throw new Exception("Something Broke 2");
            //    }
            //}

            var add = await _context.SaveChangesAsync(cancellationToken);

            var queryable = _context.Users.AsQueryable();

            var entities = await queryable.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<UserDto>(entities);
        }
    }
}