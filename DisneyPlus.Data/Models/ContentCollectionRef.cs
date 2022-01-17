namespace DisneyPlus.Data.Models
{
    public class ContentCollectionRef : ContentCollection
    {
        public ContentCollectionRef(ContentCollection collection, string refId) 
            : this(collection.Id, collection.Title, refId) {}
        public ContentCollectionRef(string id, string title, string refId)
            : base(id, title) => 
            RefId = refId;

        public string RefId { get; set; }
        public bool IsLoaded { get; set; }
    }
}
