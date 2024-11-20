namespace Engage.Application.Files
{
    public class FileDeleteValidator<T> : AbstractValidator<T> where T : FileDeleteCommand
    {
        public FileDeleteValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
            RuleFor(e => e.FileName).NotEmpty();
        }
    }
}
