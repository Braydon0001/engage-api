namespace Engage.Application.Files;

public static class JsonFilesExtensions

{
    public static List<JsonFile> AddFile(this List<JsonFile> files, JsonFile file)
    {
        if (files == null || files.Count == 0)
        {
            return new List<JsonFile> { file };
        }

        // Overwrite the file if the type or name already exists 
        //if (!string.IsNullOrWhiteSpace(file.Type))
        //{
        //    files = files.Where(e => e.Type.ToLower() != file.Type.ToLower()).ToList();
        //}

        if (!string.IsNullOrWhiteSpace(file.Type))
        {
            files = files.Where(e => e.Name.ToLower() != file.Name.ToLower()).ToList();
        }

        //var newFiles = !string.IsNullOrWhiteSpace(file.Type) ?
        // files.Where(e => e.Type.ToLower() != file.Type.ToLower() && e.Name.ToLower() != file.Name.ToLower()).ToList() :
        // files.Where(e => e.Name.ToLower() != file.Name.ToLower()).ToList();

        files.Add(file);

        return files;
    }

    public static bool FileExists(this List<JsonFile> files, FileDeleteCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.FileName))
        {
            throw new ArgumentNullException(nameof(command.FileName));
        }

        if (files == null)
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(command.FileType))
        {
            return files.Any(e => e.Name.ToLower() == command.FileName.ToLower() &&
                                  e.Type.ToLower() == command.FileType.ToLower());
        }

        return files.Any(e => e.Name.ToLower() == command.FileName.ToLower());
    }

    public static List<JsonFile> RemoveFile(this List<JsonFile> files, FileDeleteCommand command)
    {
        if (files == null)
        {
            throw new ArgumentNullException(nameof(files));
        }

        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.FileName))
        {
            throw new ArgumentNullException(nameof(command.FileName));
        }

        if (files.Count == 1)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(command.FileType))
        {
            return files.Where(e => !(e.Name.ToLower() == command.FileName.ToLower() &&
                                    e.Type.ToLower() == command.FileType.ToLower())).ToList();
        }

        return files.Where(e => e.Name != command.FileName).ToList();
    }
}
