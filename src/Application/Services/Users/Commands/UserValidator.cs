namespace Engage.Application.Services.Users.Commands;

public class UserValidator<T> : AbstractValidator<T> where T : UserCommand
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.LastName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Email).MaximumLength(100).NotEmpty();
        RuleFor(x => x.MobilePhone).MaximumLength(30);
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
    }
}

public class CreateUserValidator : UserValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
    }
}

public class UpdateUserValidator : UserValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
