namespace Engage.Application.Files
{
    public class FileUploadValidator<T> : AbstractValidator<T> where T : FileUploadCommand
    {
        public FileUploadValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
            RuleFor(e => e.File).NotNull();
        }
    }
}
