using DijiWalk.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services
{
    public class GameService
    {
        HttpClient _client;

        public GameService()
        {
            _client = new HttpClient();
        }

        public async Task<Game> GetGameById(int id)
        {

            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44374/api/game/" + id);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("User-Agent", "DijiWalk");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();
                    Game game = JsonConvert.DeserializeObject<Game>(responseStream);
                    return game;
                }
                return null;
            }
        }

      
    }
}

