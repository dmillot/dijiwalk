using DijiWalk.Entities;
using DijiWalk.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services
{
    public class GameService : IGameService
    {
        public async Task<Game> GetGameById(int id)
        {

            using (HttpClient client = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => {  return true; },}, false))
            {
                try
                {
                    var response = await client.GetAsync("https://10.0.2.2:5001/api/game/1");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseStream = await response.Content.ReadAsStringAsync();
                        Game game = JsonConvert.DeserializeObject<Game>(responseStream);
                        return game;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"     Error {0}", ex.Message);
                    Console.WriteLine(@"     Error {0}", ex.InnerException?.Message);
                }
                return null;
            }
        }


    }
}

