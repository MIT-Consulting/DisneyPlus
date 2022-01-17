using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DisneyPlus.Data.Models
{
    public class ContentCollection : ContentBase
    {
        public ContentCollection(string id, string title)
            : base(id, title)  {}

        public ObservableCollection<ContentTile> Items { get; set; } = new ObservableCollection<ContentTile>();
    }
}
