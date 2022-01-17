namespace DisneyPlus.Data.Models
{
    public abstract class  ContentBase
    {
        public ContentBase(string id, string title)
        {
            Id = id;
            Title = title;
        }
        public string Id { get; set; }
        public string Title { get; set; }

    }
}
