using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace Engage.Infrastructure.Services;

class OktaService : IOktaService
{
    private readonly OktaClient _client;
    private readonly JwtOptions _options;

    public OktaService(IOptions<JwtOptions> options)
    {
        _options = options.Value;

        _client = new OktaClient(new OktaClientConfiguration
        {
            OktaDomain = _options.Authority,
            Token = _options.ApiToken
        });
    }

    public async void CreateUser(string login, string firstName, string lastName, string email, string employeeNumber, string mobileNumber, string supplier)
    {
        // Create a user with the specified password
        var user = await _client.Users.CreateUserAsync(new CreateUserWithPasswordOptions
        {
            // User profile object
            Profile = new UserProfile
            {
                Login = login,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                EmployeeNumber = employeeNumber,
                MobilePhone = mobileNumber,
            },
            Activate = false,
        });

        // Add the custom fields
        user.Profile["supplier"] = supplier;
        await user.UpdateAsync();

        // Send the activation email
        await user.ActivateAsync();
    }

    public async void UpdateUser(string login, string firstName, string lastName, string mobilePhone, string employeeNumber, string newEmail)
    {
        // Retrieving the user
        var user = await _client.Users.GetUserAsync(login);

        // Set the fields in the user's profile
        user.Profile["login"] = newEmail;
        user.Profile["firstName"] = firstName;
        user.Profile["lastName"] = lastName;
        user.Profile["mobilePhone"] = mobilePhone;
        user.Profile["employeeNumber"] = employeeNumber;
        user.Profile["email"] = newEmail;

        // Then, save the user
        await user.UpdateAsync();
    }
}
