namespace Engage.Application.Interfaces;

public interface IHandlebarsService
{
    string RenderTemplate(string templateContent, object data);
}
