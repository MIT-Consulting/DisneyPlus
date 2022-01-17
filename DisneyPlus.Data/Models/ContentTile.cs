namespace DisneyPlus.Data.Models
{
    public class ContentTile : ContentBase
    {
        public ContentTile(string id, string title, string imageUri)
            :base(id, title)
        {
            Id = id;
            Title = title;
            ImageUri = imageUri;
        }
        

        public string ImageUri { get; set; }
    }
}
