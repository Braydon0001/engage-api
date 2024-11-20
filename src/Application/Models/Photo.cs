namespace Engage.Application.Models
{
    public class Photo
    {
        public string Id { get; set; }
        public string Folder { get; set; }
        public string Base64String { get; set; }
    }

    public class PhotoList
    {
        public List<Photo> Photos { get; set; }

    }
}
