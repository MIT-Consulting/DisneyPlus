using DisneyPlus.Data.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DisneyPlus.Data.Repos
{
    public class ContentRepo : IContentRepo
    {
        const string url_base = "https://cd-static.bamgrid.com/dp-117731241344/";
        const string jpath_title = "['text']['title']['full']....['content']";
        const string jpath_tile_url = "['image']['tile']['1.78']....['url']";

        public async Task<List<ContentCollection>> GetCollections()
        {
            var collections = new List<ContentCollection>();
            var json = await GetHome();
            var containers = json["data"]["StandardCollection"]["containers"];
            foreach (var c in containers)
            {
                var set = c["set"];
                var setId = (string)set["setId"];
                var title = (string)set.SelectToken(jpath_title);
                var collection = new ContentCollection(setId, title);
                var items = set["items"];
                if (items != null)
                    collection.Items.AddRange(ParseItems(items));
                else
                {
                    var refid = (string)set["refId"];
                    collection.Items.AddRange(await GetSetItems(refid));
                }
                collections.Add(collection);
            }
            return collections;
        }

        public async Task<List<ContentTile>> GetSetItems(string refId)
        {
            var tiles = new List<ContentTile>();
            var refSet = await GetSet(refId);
            tiles.AddRange(ParseItems(refSet.SelectToken("['data']..['items']")));
            return tiles;
        }

        private List<ContentTile> ParseItems(JToken items)
        {
            var tiles = new List<ContentTile>();
            if (items != null)
            {
                foreach (var i in items)
                    tiles.Add(
                        new ContentTile(
                            (string)i["contentId"],
                            (string)i.SelectToken(jpath_title),
                            (string)i.SelectToken(jpath_tile_url)));
            }
            return tiles;
        }

        public async Task<List<ContentTileHeader>> GetHeaders()
        {
            var headers = new List<ContentTileHeader>();

            await Task.Run(() =>
            {
                headers.Add(new ContentTileHeader("1h", "Eternals", "/Assets/Headers/eternals-background.png", "/Assets/Headers/eternals-foreground.png"));
                headers.Add(new ContentTileHeader("2h", "Encanto", "/Assets/Headers/encanto-background.png", "/Assets/Headers/encanto-foreground.png"));
                headers.Add(new ContentTileHeader("3h", "ron", "/Assets/Headers/ron-background.png", "/Assets/Headers/ron-foreground.png"));
                headers.Add(new ContentTileHeader("4h", "Boba Fett", "/Assets/Headers/boba-background.png", "/Assets/Headers/boba-foreground.png"));
            });

            return headers;
        }

        public async Task<List<ContentTileVideo>> GetCategories()
        {
            var categories = new List<ContentTileVideo>();

            await Task.Run(() =>
            {
                categories.Add(new ContentTileVideo("1", "Disney", 
                    "https://prod-ripcut-delivery.disney-plus.net/v1/variant/disney/FFA0BEBAC1406D88929497501C84019EBBA1B018D3F7C4C3C829F1810A24AD6E/scale?width=400&amp;aspectRatio=1.78&amp;format=png",
                    "https://vod-bgc-na-east-1.media.dssott.com/bgui/ps01/disney/bgui/2019/08/01/1564674844-disney.mp4"));
                categories.Add(new ContentTileVideo("2", "Pixar",
                    "https://prod-ripcut-delivery.disney-plus.net/v1/variant/disney/7F4E1A299763030A0A8527227AD2812C049CE3E02822F7EDEFCFA1CFB703DDA5/scale?width=400&amp;aspectRatio=1.78&amp;format=png",
                    "https://vod-bgc-na-east-1.media.dssott.com/bgui/ps01/disney/bgui/2019/08/01/1564676714-pixar.mp4"));
                categories.Add(new ContentTileVideo("3", "Marvel",
                    "https://prod-ripcut-delivery.disney-plus.net/v1/variant/disney/C90088DCAB7EA558159C0A79E4839D46B5302B5521BAB1F76D2E807D9E2C6D9A/scale?width=400&amp;aspectRatio=1.78&amp;format=png",
                    "https://vod-bgc-na-east-1.media.dssott.com/bgui/ps01/disney/bgui/2019/08/01/1564676115-marvel.mp4"));
                categories.Add(new ContentTileVideo("4", "Star Wars",
                    "https://prod-ripcut-delivery.disney-plus.net/v1/variant/disney/5A9416D67DC9595496B2666087596EE64DE379272051BB854157C0D938BE2C26/scale?width=400&amp;aspectRatio=1.78&amp;format=png",
                    "https://vod-bgc-na-east-1.media.dssott.com/bgui/ps01/disney/bgui/2020/12/17/1608229455-star-wars.mp4"));
                categories.Add(new ContentTileVideo("5", "National Geographic",
                    "https://prod-ripcut-delivery.disney-plus.net/v1/variant/disney/2EF24AA0A1E648E6D1A3B26491F516632137ED87AB22969D153316F8BD670FB5/scale?width=400&amp;aspectRatio=1.78&amp;format=png",
                    "https://vod-bgc-na-east-1.media.dssott.com/bgui/ps01/disney/bgui/2019/08/01/1564676296-national-geographic.mp4"));
            });

            return categories;
        }

        private async Task<JObject> GetHome() => Parse(await GetJson("home.json"));
        private async Task<JObject> GetSet(string refId) => Parse(await GetJson($"sets/{refId}.json"));
        private JObject Parse(string json) => JObject.Parse(json);
        private async Task<string> GetJson(string url)
        {
            using (var wc = new WebClient())
                return await wc.DownloadStringTaskAsync($"{url_base}{url}");
        }
    }
}
