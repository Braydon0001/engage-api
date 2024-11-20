using System.Net.Mail;

namespace Engage.Application.Services.Users.Commands;

public class RemoveBulkGroupsFromJobTitlesCommand : IRequest<OperationStatus>
{
}

public class RemoveBulkGroupsFromJobTitlesCommandHandler : BaseCreateCommandHandler, IRequestHandler<RemoveBulkGroupsFromJobTitlesCommand, OperationStatus>
{
    private readonly IOptions<JwtOptions> _options;
    public RemoveBulkGroupsFromJobTitlesCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IOptions<JwtOptions> options) :
        base(context, mapper, mediator)
    {
        _options = options;
    }

    public Task<OperationStatus> Handle(RemoveBulkGroupsFromJobTitlesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //int GroupId = 56;
        //var query = _context.Employees.Where(e => e.Disabled == false && e.Deleted == false && e.UserId.HasValue);

        //var userIds = await query.Select(e => e.UserId.Value).ToListAsync(cancellationToken);

        //if (userIds.Count > 0)
        //{
        //    foreach (var userId in userIds)
        //    {
        //        var user = await _context.Users.Include(u => u.UserGroups)
        //                                       .ThenInclude(g => g.UserGroup)
        //                                       .Where(e => e.UserId == userId && e.Deleted == false && e.Disabled == false)
        //                                       .FirstOrDefaultAsync(cancellationToken);

        //        if (user != null)
        //        {
        //            var userGroupIds = user.UserGroups.Where(u => u.UserGroup.Name != "Everyone")
        //                                              .Select(e => e.UserGroupId)
        //                                              .ToList();

        //            if (userGroupIds.Count > 0)
        //            {
        //                if (!userGroupIds.Contains(GroupId))
        //                {
        //                    userGroupIds.Add(GroupId);

        //                    userGroupIds = userGroupIds.Distinct().ToList();

        //                    var updateUser = await _mediator.Send(new UpdateUserCommand
        //                    {
        //                        Id = user.UserId,
        //                        Groups = userGroupIds,
        //                        Email = user.Email,
        //                        FirstName = user.FirstName,
        //                        LastName = user.LastName,
        //                        IgnoreOrderProductFilters = user.IgnoreOrderProductFilters,
        //                        MobilePhone = user.MobilePhone,
        //                        SupplierId = user.SupplierId,
        //                        IsEmployeeEmailUpdate = false,
        //                        RoleId = user.RoleId,
        //                    });
        //                }
        //            }
        //        }
        //    }
        //}
        //List<int> userGroupIds = new List<int> { 45 };

        //var client = new OktaClient(new OktaClientConfiguration
        //{
        //    OktaDomain = _options.Value.Authority,
        //    Token = _options.Value.UsersApiToken
        //});
        //var oktaUsers = await client.Users.ToListAsync(cancellationToken);

        //var users = await _context.Users.Include(u => u.UserGroups).Where(u => u.Disabled == false
        //                                        && u.Deleted == false
        //                                        && u.SupplierId == 126
        //                                 && !u.UserGroups.Any(u => u.UserGroupId == 45)
        //                                 )
        //                                .Select(u => u.Email.ToLower())
        //                                .ToListAsync(cancellationToken);

        //var userEmails = await _context.Employees.Where(e => users.Contains(e.EmailAddress1.ToLower()))
        //                                         .Select(e => e.EmailAddress1.ToLower())
        //                                         .OrderBy(e => e)
        //                                         .ToListAsync(cancellationToken);

        //if (userEmails.Any())
        //{
        //    var usersToUpdate = oktaUsers.Where(u => userEmails.Contains(u.Profile.Email.ToLower()))
        //                                 .Select(u => u.Profile.Email.ToLower())
        //                                 .ToList();

        //    usersToUpdate = usersToUpdate.Distinct().ToList();

        //    if (usersToUpdate.Any())
        //    {
        //        foreach (var email in usersToUpdate)
        //        {
        //            var user = await _context.Users.Include(u => u.UserGroups).ThenInclude(g => g.UserGroup)
        //                                           .Where(u => u.Email.ToLower() == email.ToLower())
        //                                           .FirstOrDefaultAsync(cancellationToken);

        //            var userGroupIdsToUpdate = user.UserGroups.Where(u => u.User.Email.ToLower() == email.ToLower() && u.UserGroup.Name != "Everyone")
        //                                                                    .Select(u => u.UserGroupId)
        //                                                                    .ToList();

        //            userGroupIdsToUpdate.AddRange(userGroupIds);

        //            userGroupIdsToUpdate = userGroupIdsToUpdate.Distinct().ToList();

        //            var updateUser = await _mediator.Send(new UpdateUserCommand
        //            {
        //                Id = user.UserId,
        //                Groups = userGroupIdsToUpdate,
        //                Email = user.Email,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                IgnoreOrderProductFilters = user.IgnoreOrderProductFilters,
        //                MobilePhone = user.MobilePhone,
        //                SupplierId = user.SupplierId
        //            });
        //        }
        //    }
        //}

        //List<int> jobTitleIds = new List<int> { 4, 26, 33 };
        //List<int> userGroupIds = new List<int> { 26, 27 };

        //var client = new OktaClient(new OktaClientConfiguration
        //{
        //    OktaDomain = _options.Value.Authority,
        //    Token = _options.Value.UsersApiToken
        //});
        //var oktaUsers = await client.Users.ToListAsync(cancellationToken);

        //var employees = await _context.Employees.Where(e => e.Disabled == false && e.Deleted == false)
        //                            .Join(_context.EmployeeEmployeeJobTitles.Where(c => !jobTitleIds.Contains(c.EmployeeJobTitleId)),
        //                                  employee => employee.EmployeeId,
        //                                  jobTitle => jobTitle.EmployeeId,
        //                                  (employee, jobTitle) => employee.EmailAddress1.ToLower().Trim())
        //                            .ToListAsync(cancellationToken);

        //employees = employees.Distinct().ToList();

        //var users = await _context.Users.Include(u => u.UserGroups).Where(e => e.Disabled == false && e.Deleted == false && employees.Contains(e.Email.Trim()))
        //                            //.Join(_context.UserUserGroups.Where(c => !userGroupIds.Contains(c.UserGroupId)),
        //                            //      user => user.UserId,
        //                            //      userGroup => userGroup.UserId,
        //                            //      (user, userGroup) => user)
        //                            .ToListAsync(cancellationToken);

        ////var usersWithoutUserGroups = await _context.UserUserGroups.Where(u => !userGroupIds.Contains(u.UserGroupId))
        ////                                                          .ToListAsync(cancellationToken);
        //List<User> usersToUpdate = new List<User>();
        //if (users.Any())
        //{
        //    var dist = users.Distinct().ToList();
        //    foreach (var user in dist)
        //    {
        //        foreach (var groupId in userGroupIds)
        //        {
        //            if (!user.UserGroups.Select(u => u.UserGroupId).Contains(groupId))
        //            {
        //                usersToUpdate.Add(user);
        //            }
        //        }
        //    }
        //}

        //if (employees.Any())
        //{
        //    //var usersToUpdate = users.Where(u => employees.Distinct().Contains(u.Email.ToLower())).ToList();

        //    usersToUpdate = usersToUpdate.Distinct().Take(50).ToList();



        //    if (usersToUpdate.Any())
        //    {
        //        foreach (var user in usersToUpdate)
        //        {
        //            var oktaUser = oktaUsers.Where(u => u.Profile.Email.ToLower() == user.Email.ToLower())
        //                                             .FirstOrDefault();

        //            if (oktaUser == null)
        //            {
        //                var newUser = await client.Users.CreateUserAsync(new CreateUserWithoutCredentialsOptions
        //                {
        //                    Profile = new UserProfile //create the Okta user profile
        //                    {
        //                        FirstName = user.FirstName,
        //                        LastName = user.LastName,
        //                        Email = user.Email,
        //                        Login = user.Email,
        //                        MobilePhone = user.MobilePhone,
        //                    },
        //                    Activate = false,
        //                }, cancellationToken);

        //                newUser.Profile["supplier"] = "engage";
        //                await newUser.UpdateAsync(cancellationToken);
        //                await newUser.ActivateAsync();

        //                var userUserGroupEntry = new UserUserGroup //Add the user to the 'Everyone' group in the DB to mirror Okta.
        //                {
        //                    UserId = user.UserId,
        //                    UserGroupId = await _context.UserGroups.Where(e => e.Name == "Everyone").Select(e => e.UserGroupId).SingleAsync()
        //                };

        //                _context.UserUserGroups.Add(userUserGroupEntry);
        //            }

        //            var userGroupIdsToUpdate = await _context.UserUserGroups
        //                                                        .Where(u => u.UserId == user.UserId && u.UserGroup.Name != "Everyone")
        //                                                        .Select(u => u.UserGroupId)
        //                                                        .ToListAsync(cancellationToken);

        //            userGroupIdsToUpdate.AddRange(userGroupIds);

        //            userGroupIdsToUpdate = userGroupIdsToUpdate.Distinct().ToList();

        //            var updateUser = await _mediator.Send(new UpdateUserCommand
        //            {
        //                Id = user.UserId,
        //                Groups = userGroupIdsToUpdate,
        //                Email = user.Email,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                IgnoreOrderProductFilters = user.IgnoreOrderProductFilters,
        //                MobilePhone = user.MobilePhone,
        //                SupplierId = user.SupplierId
        //            });
        //        }
        //    }
        //}

        //return new OperationStatus { Status = true };

    }

    //public async Task<OperationStatus> Handle(RemoveBulkGroupsFromJobTitlesCommand request, CancellationToken cancellationToken)
    //{
    //    //throw new NotImplementedException();

    //    List<int> userIds = new List<int> { 2998, 3070, 3081, 3084, 3139, 3145, 3190, 3204, 3219, 3274, 3332 };

    //    var suppliers = new Dictionary<int, string>()
    //        {
    //            { 18, "distell" },
    //            { 49, "heineken" },
    //            { 59, "encore" },
    //            { 97, "signalhill" },
    //            { 126, "engage" },
    //            { 230,"halewood" },
    //            { 236, "chipkins" },
    //            { 237,"stetsons" },
    //            { 238,"j&m" },
    //            { 263,"vegeworth" },
    //            { 1326, "unbxd" },
    //        };

    //    var client = new OktaClient(new OktaClientConfiguration
    //    {
    //        OktaDomain = _options.Value.Authority,
    //        Token = _options.Value.UsersApiToken
    //    });

    //    foreach (var userId in userIds)
    //    {
    //        var entity = await _context.Users.Include(u => u.UserGroups).ThenInclude(g => g.UserGroup)
    //                                         .Where(u => u.UserId == userId)
    //                                         .FirstOrDefaultAsync(cancellationToken);

    //        var user = await client.Users.CreateUserAsync(new CreateUserWithoutCredentialsOptions
    //        {
    //            Profile = new UserProfile
    //            {
    //                FirstName = entity.FirstName,
    //                LastName = entity.LastName,
    //                Email = entity.Email,
    //                Login = entity.Email,
    //                MobilePhone = entity.MobilePhone,
    //            },
    //            Activate = false,
    //        }, cancellationToken);

    //        var supplierSetting = await _context.SupplierSettings.Where(s => s.SupplierId == entity.SupplierId && s.SettingId == (int)SupplierSettingId.OktaSupplier)
    //                                                             .FirstOrDefaultAsync(cancellationToken);

    //        if (supplierSetting != null)
    //        {
    //            user.Profile["supplier"] = supplierSetting.Value;  //add the supplier custom profile field
    //            if (supplierSetting.Value == "unbxd")
    //            {
    //                user.Profile.Organization = "sparzw";
    //            }
    //            await user.UpdateAsync(cancellationToken);
    //        }
    //        else
    //        {
    //            if (suppliers.TryGetValue(entity.SupplierId, out var supplier))
    //            {
    //                user.Profile["supplier"] = supplier; //add the supplier custom profile field
    //                if (supplier == "unbxd")
    //                {
    //                    user.Profile.Organization = "sparzw";
    //                }
    //                await user.UpdateAsync(cancellationToken);
    //            }
    //        }

    //        await user.ActivateAsync(true, cancellationToken);

    //        var userGroups = entity.UserGroups.Where(u => u.UserGroup.Name != "Everyone")
    //                                          .Select(u => u.UserGroupId)
    //                                          .ToList();

    //        if (userGroups != null) //Groups will be null if the user is being created from the employee creation process
    //        {
    //            if (userGroups.Count != 0) //Groups will have no entries if they are created in the groups screen with no groups selected.
    //            {
    //                foreach (var group in userGroups) //if there are any groups chosen, add each entry into the DB to match Okta.
    //                {
    //                    var groupId = _context.UserGroups.Where(e => e.UserGroupId == group).Select(e => e.VendorId).FirstOrDefault();
    //                    if (groupId != null)
    //                    {
    //                        await client.Groups.AddUserToGroupAsync(groupId, user.Id); //add User to Okta Groups
    //                    }
    //                    else
    //                    {
    //                        throw new Exception("Okta Group Not Found");
    //                    }
    //                }
    //            }
    //        }

    //        var employee = await _context.Employees.Where(e => e.Disabled == false && e.Deleted == false)
    //                                               .SingleOrDefaultAsync(e => e.EmailAddress1 == entity.Email, cancellationToken);
    //        if (employee != null)
    //        {
    //            employee.UserId = entity.UserId;
    //            await _context.SaveChangesAsync(cancellationToken);
    //        }
    //    }

    //    return new OperationStatus { Status = true };

    //}


    //public OperationStatus Handle(RemoveBulkGroupsFromJobTitlesCommand command, CancellationToken cancellationToken)
    //{
    //List<int> userGroupIds = new List<int> { 45 };

    //var client = new OktaClient(new OktaClientConfiguration
    //{
    //    OktaDomain = _options.Value.Authority,
    //    Token = _options.Value.UsersApiToken
    //});
    //var oktaUsers = await client.Users.ToListAsync(cancellationToken);

    //var users = await _context.Users.Include(u => u.UserGroups).Where(u => u.Disabled == false
    //                                        && u.Deleted == false
    //                                        && u.SupplierId == 126
    //                                 && !u.UserGroups.Any(u => u.UserGroupId == 45)
    //                                 )
    //                                .Select(u => u.Email.ToLower())
    //                                .ToListAsync(cancellationToken);

    //var userEmails = await _context.Employees.Where(e => users.Contains(e.EmailAddress1.ToLower()))
    //                                         .Select(e => e.EmailAddress1.ToLower())
    //                                         .OrderBy(e => e)
    //                                         .ToListAsync(cancellationToken);

    //if (userEmails.Any())
    //{
    //    var usersToUpdate = oktaUsers.Where(u => userEmails.Contains(u.Profile.Email.ToLower()))
    //                                 .Select(u => u.Profile.Email.ToLower())
    //                                 .ToList();

    //    usersToUpdate = usersToUpdate.Distinct().ToList();

    //    if (usersToUpdate.Any())
    //    {
    //        foreach (var email in usersToUpdate)
    //        {
    //            var user = await _context.Users.Include(u => u.UserGroups).ThenInclude(g => g.UserGroup)
    //                                           .Where(u => u.Email.ToLower() == email.ToLower())
    //                                           .FirstOrDefaultAsync(cancellationToken);

    //            var userGroupIdsToUpdate = user.UserGroups.Where(u => u.User.Email.ToLower() == email.ToLower() && u.UserGroup.Name != "Everyone")
    //                                                                    .Select(u => u.UserGroupId)
    //                                                                    .ToList();

    //            userGroupIdsToUpdate.AddRange(userGroupIds);

    //            userGroupIdsToUpdate = userGroupIdsToUpdate.Distinct().ToList();

    //            var updateUser = await _mediator.Send(new UpdateUserCommand
    //            {
    //                Id = user.UserId,
    //                Groups = userGroupIdsToUpdate,
    //                Email = user.Email,
    //                FirstName = user.FirstName,
    //                LastName = user.LastName,
    //                IgnoreOrderProductFilters = user.IgnoreOrderProductFilters,
    //                MobilePhone = user.MobilePhone,
    //                SupplierId = user.SupplierId
    //            });
    //        }
    //    }
    //}

    //List<int> jobTitleIds = new List<int> { 4, 26, 33 };
    //List<int> userGroupIds = new List<int> { 26, 27 };

    //var client = new OktaClient(new OktaClientConfiguration
    //{
    //    OktaDomain = _options.Value.Authority,
    //    Token = _options.Value.UsersApiToken
    //});
    //var oktaUsers = await client.Users.ToListAsync(cancellationToken);

    //var employees = await _context.Employees.Where(e => e.Disabled == false && e.Deleted == false)
    //                            .Join(_context.EmployeeEmployeeJobTitles.Where(c => !jobTitleIds.Contains(c.EmployeeJobTitleId)),
    //                                  employee => employee.EmployeeId,
    //                                  jobTitle => jobTitle.EmployeeId,
    //                                  (employee, jobTitle) => employee.EmailAddress1.ToLower().Trim())
    //                            .ToListAsync(cancellationToken);

    //employees = employees.Distinct().ToList();

    //var users = await _context.Users.Include(u => u.UserGroups).Where(e => e.Disabled == false && e.Deleted == false && employees.Contains(e.Email.Trim()))
    //                            //.Join(_context.UserUserGroups.Where(c => !userGroupIds.Contains(c.UserGroupId)),
    //                            //      user => user.UserId,
    //                            //      userGroup => userGroup.UserId,
    //                            //      (user, userGroup) => user)
    //                            .ToListAsync(cancellationToken);

    ////var usersWithoutUserGroups = await _context.UserUserGroups.Where(u => !userGroupIds.Contains(u.UserGroupId))
    ////                                                          .ToListAsync(cancellationToken);
    //List<User> usersToUpdate = new List<User>();
    //if (users.Any())
    //{
    //    var dist = users.Distinct().ToList();
    //    foreach (var user in dist)
    //    {
    //        foreach (var groupId in userGroupIds)
    //        {
    //            if (!user.UserGroups.Select(u => u.UserGroupId).Contains(groupId))
    //            {
    //                usersToUpdate.Add(user);
    //            }
    //        }
    //    }
    //}

    //if (employees.Any())
    //{
    //    //var usersToUpdate = users.Where(u => employees.Distinct().Contains(u.Email.ToLower())).ToList();

    //    usersToUpdate = usersToUpdate.Distinct().Take(50).ToList();



    //    if (usersToUpdate.Any())
    //    {
    //        foreach (var user in usersToUpdate)
    //        {
    //            var oktaUser = oktaUsers.Where(u => u.Profile.Email.ToLower() == user.Email.ToLower())
    //                                             .FirstOrDefault();

    //            if (oktaUser == null)
    //            {
    //                var newUser = await client.Users.CreateUserAsync(new CreateUserWithoutCredentialsOptions
    //                {
    //                    Profile = new UserProfile //create the Okta user profile
    //                    {
    //                        FirstName = user.FirstName,
    //                        LastName = user.LastName,
    //                        Email = user.Email,
    //                        Login = user.Email,
    //                        MobilePhone = user.MobilePhone,
    //                    },
    //                    Activate = false,
    //                }, cancellationToken);

    //                newUser.Profile["supplier"] = "engage";
    //                await newUser.UpdateAsync(cancellationToken);
    //                await newUser.ActivateAsync();

    //                var userUserGroupEntry = new UserUserGroup //Add the user to the 'Everyone' group in the DB to mirror Okta.
    //                {
    //                    UserId = user.UserId,
    //                    UserGroupId = await _context.UserGroups.Where(e => e.Name == "Everyone").Select(e => e.UserGroupId).SingleAsync()
    //                };

    //                _context.UserUserGroups.Add(userUserGroupEntry);
    //            }

    //            var userGroupIdsToUpdate = await _context.UserUserGroups
    //                                                        .Where(u => u.UserId == user.UserId && u.UserGroup.Name != "Everyone")
    //                                                        .Select(u => u.UserGroupId)
    //                                                        .ToListAsync(cancellationToken);

    //            userGroupIdsToUpdate.AddRange(userGroupIds);

    //            userGroupIdsToUpdate = userGroupIdsToUpdate.Distinct().ToList();

    //            var updateUser = await _mediator.Send(new UpdateUserCommand
    //            {
    //                Id = user.UserId,
    //                Groups = userGroupIdsToUpdate,
    //                Email = user.Email,
    //                FirstName = user.FirstName,
    //                LastName = user.LastName,
    //                IgnoreOrderProductFilters = user.IgnoreOrderProductFilters,
    //                MobilePhone = user.MobilePhone,
    //                SupplierId = user.SupplierId
    //            });
    //        }
    //    }
    //}

    // new OperationStatus { Status = true };


    private bool IsValidEmail(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }
}
