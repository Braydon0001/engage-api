using Engage.Application.Services.UserUserGroups.Models;
using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace Engage.Application.Services.UserUserGroups.Queries
{
    public class UserUserGroupSyncQuery : IRequest<List<UserUserGroupDto>>
    {
    }

    public class UserUserGroupSyncQueryHandler : BaseQueryHandler, IRequestHandler<UserUserGroupSyncQuery, List<UserUserGroupDto>>
    {
        private readonly IOptions<JwtOptions> _options;
        public UserUserGroupSyncQueryHandler(IAppDbContext context, IMapper mapper, IOptions<JwtOptions> options) : base(context, mapper)
        {
            _options = options;
        }

        public async Task<List<UserUserGroupDto>> Handle(UserUserGroupSyncQuery request, CancellationToken cancellationToken)
        {
            var client = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = _options.Value.Authority,
                Token = _options.Value.UsersApiToken
            });

            var dbUsers = _context.Users.AsQueryable(); //get all users in the db

            var dbGroups = _context.UserGroups.AsQueryable(); //get all groups in the db

            var oktaGroups = await client.Groups.ToListAsync(cancellationToken); //get all groups on okta

            var userUserGroups = _context.UserUserGroups.IgnoreQueryFilters().AsQueryable().Where(e => !e.Deleted && !e.Disabled);

            var notInDb = new List<OktaUserGroup>(); //keep a list of okta group members not in the db

            var inDb = new List<UserUserGroup>(); //similarly for those that are

            foreach (var group in oktaGroups)
            {
                var groupPeople = await group.Users.ToListAsync(); //get all users in a group
                var dbGroup = dbGroups.Where(e => e.VendorId == group.Id).Single(); //specify that group in the DB
                foreach (var person in groupPeople)
                {
                    if (person.Profile.Organization != "sparzw") //check if the person belongs to sparzw, if not:
                    {
                        var user = dbUsers.Where(e => e.Email.ToLower() == person.Profile.Email.ToLower()).ToList(); //get the user in the db
                        if (user != null && user.Count == 1) //check that the user exists
                        {
                            var userUserGroup = userUserGroups.Where(e => e.UserId == user[0].UserId && e.UserGroupId == dbGroup.UserGroupId).FirstOrDefault(); //look for the entry in the db
                            if (userUserGroup != null) // if found
                            {
                                inDb.Add(userUserGroup); // add to inDb list
                            }
                            else if (userUserGroup == null) //if not found
                            {
                                var entry = new OktaUserGroup //create new object to add to notInDb list
                                {
                                    UserGroupId = dbGroup.UserGroupId,
                                    UserId = user[0].UserId,
                                };
                                notInDb.Add(entry);
                            }
                        }
                    }
                }
            }

            foreach (var entry in notInDb)
            {
                _context.UserUserGroups.Add(new UserUserGroup
                {
                    UserId = entry.UserId,
                    UserGroupId = entry.UserGroupId,
                });
            }

            //foreach (var person in people)
            //{
            //    var diff = dbUserUserGroups.Where(e => e.Email.ToLower() == person.Profile.Email.ToLower()).ToList();
            //    if (diff.Count > 1)
            //    {
            //        duplicates.Add(diff);
            //    }
            //    else if (diff.Count == 1)
            //    {
            //        inDb.Add(diff[0]);
            //    }
            //    else if (diff.Count == 0)
            //    {
            //        notInDb.Add(person.Profile);
            //    }
            //    else
            //    {
            //        throw new Exception("Something Broke");
            //    }
            //}

            //var newUserUserGroups = new List<UserUserGroup>();

            //var zwUserUserGroups = new List<IUserUserGroupProfile>();

            //var suppliers = new Dictionary<string, int>()
            //{
            //    { "engage", 126  },
            //    { "distell", 18 },
            //    { "heineken", 49},
            //    { "encore", 59 },
            //    { "unbxd", 1326 }
            //};

            //foreach (var person in notInDb)
            //{
            //    if (person.Organization == "sparzw")
            //    {
            //        zwUserUserGroups.Add(person);
            //    }
            //    else
            //    {
            //        var supplier = 0;
            //        if (person["supplier"] != null)
            //        {
            //            if (suppliers.TryGetValue(person["supplier"].ToString(), out var supp))
            //            {
            //                supplier = supp;
            //            }
            //            else
            //            {
            //                supplier = 126;
            //            }
            //        }
            //        else if (person["supplier"] == null)
            //        {
            //            supplier = 126;
            //        }
            //        else
            //        {
            //            throw new Exception("Something Broke: The Return");
            //        }

            //        var user = new UserUserGroup
            //        {
            //            FirstName = person.FirstName,
            //            LastName = person.LastName,
            //            Email = person.Email,
            //            FullName = person.FirstName + " " + person.LastName,
            //            MobilePhone = person.MobilePhone,
            //            IgnoreOrderProductFilters = false,
            //            SupplierId = supplier,
            //        };

            //        newUserUserGroups.Add(user);
            //        _context.UserUserGroups.Add(user);
            //    }

            //}

            //var inOkta = new List<IUserUserGroupProfile>();

            //var notInOkta = new List<UserUserGroup>();

            //foreach (var dbUserUserGroup in dbUserUserGroups)
            //{
            //    var oktaUserUserGroup = people.Where(e => e.Profile.Email.ToLower() == dbUserUserGroup.Email.ToLower()).FirstOrDefault();
            //    if (oktaUserUserGroup != null)
            //    {
            //        inOkta.Add(oktaUserUserGroup.Profile);
            //    }
            //    else if (oktaUserUserGroup == null)
            //    {
            //        notInOkta.Add(dbUserUserGroup);
            //    }
            //    else
            //    {
            //        throw new Exception("Something Broke 2");
            //    }
            //}

            var add = await _context.SaveChangesAsync(cancellationToken);

            var queryable = _context.UserUserGroups.IgnoreQueryFilters().AsQueryable().Where(e => !e.Deleted && !e.Disabled);

            var entities = await queryable.ProjectTo<UserUserGroupDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<UserUserGroupDto>(entities);
        }
    }
}

public class OktaUserGroup
{
    public int UserGroupId { get; set; }
    public int UserId { get; set; }
}