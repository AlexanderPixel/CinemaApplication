using Cinema.BLL.Config;
using Cinema.BLL.DTO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.BLL.Managers
{
    public class ImdbManager
    {
        private NetworkManager networkManager;

        public ImdbManager()
        {
            networkManager = new NetworkManager();
        }

        public List<FilmDTO> GetTop250Films(int offset = 0, int limit = 50)
        {
            var apiKey = ImdbConfig.ApiKey;
            var baseUrl = ImdbConfig.BaseUrl;
            var url = $"{baseUrl}Top250Movies/{apiKey}";

            var json = networkManager.GetJson(url);
            var response = JObject.Parse(json);
            var results = response["items"].Children().ToList();

            var films = new List<FilmDTO>();
            foreach (var item in results)
            {
                var film = new FilmDTO
                {
                    FilmTitle = item["title"].ToString(),
                    FilmImage = item["image"].ToString(),
                    FilmReleaseYear = int.Parse(item["year"].ToString()),
                };
                films.Add(film);
            }

            var resultFilms = new List<FilmDTO>();
            int lastIndex = offset + limit;
            if (lastIndex <= films.Count)
            {
                for (int i = offset; i < lastIndex; i++)
                {
                    resultFilms.Add(films[i]);
                }
                return resultFilms;
            }

            return films;
        }
    }
}
