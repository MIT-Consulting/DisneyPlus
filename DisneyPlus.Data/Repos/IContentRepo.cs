using DisneyPlus.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisneyPlus.Data.Repos
{
    public interface IContentRepo
    {
        Task<List<ContentCollection>> GetCollections();
        Task<List<ContentTile>> GetSetItems(string refId);
        Task<List<ContentTileHeader>> GetHeaders();
        Task<List<ContentTileVideo>> GetCategories();
    }
}