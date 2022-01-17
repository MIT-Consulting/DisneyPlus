namespace DisneyPlus.Data.Models
{
    public class ContentTileHeader : ContentTile
    {
        public ContentTileHeader(string id, string title, string imageUri, string videoUri)
            : base(id, title, imageUri)
        {
            ForegroundUri = videoUri;
        }
        public string ForegroundUri { get; set; }
    }
}
