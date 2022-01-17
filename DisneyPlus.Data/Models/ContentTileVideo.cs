namespace DisneyPlus.Data.Models
{
    public class ContentTileVideo : ContentTile
    {
        public ContentTileVideo(string id, string title, string imageUri, string videoUri)
            : base(id, title, imageUri)
        {
            VideoUri = videoUri;
        }
        public string VideoUri { get; set; }
    }
}
