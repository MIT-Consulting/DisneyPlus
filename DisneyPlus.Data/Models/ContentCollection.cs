using System.Collections.Generic;

namespace DisneyPlus.Data.Models
{
    public class ContentCollection : ContentBase
    {
        public ContentCollection(string id, string title)
            : base(id, title)  {}

        public List<ContentTile> Items { get; set; } = new List<ContentTile>();
    }
}
