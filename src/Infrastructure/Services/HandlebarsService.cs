using HandlebarsDotNet;

namespace Engage.Infrastructure.Services;

internal class HandlebarsService : IHandlebarsService
{
    public string RenderTemplate(string templateContent, object data)
    {
        var template = Handlebars.Compile(templateContent);
        return template(data);
    }
}
