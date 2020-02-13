using DijiWalk.Entities;
using DijiWalk.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DijiWalk.Common.Exceptions;
using Xamarin.Forms;
using DijiWalk.Mobile.Services.Common;

namespace DijiWalk.Mobile.Services
{
    public class PlayerService : IPlayerService
    {

        /// <summary>
        /// Récupère tous les participants
        /// </summary>
        /// <returns>Retourne toute la liste des participants (attente)</returns>
        public async Task<List<Player>> GetPlayers()
        {
            return JsonConvert.DeserializeObject<List<Player>>(await CommonService.GetAll(typeof(Player)));
        }

        /// <summary>
        /// Récupère un participant spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du participant</param>
        /// <returns>Retourne le participant (attente)</returns>
        public async Task<Player> GetPlayerById(int id)
        {
            return JsonConvert.DeserializeObject<Player>(await CommonService.GetId(id, typeof(Player)));
        }

        public async Task<List<Game>> GetPreviousGames(int playerId)
        {
            var json = "";

            using (HttpClient client = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }, }, false))
            {
                try
                {
                    var response = await client.GetAsync(String.Concat(Application.Current.Properties["url"], typeof(Player).Name.ToLower(), "/previous/", playerId));
                    if (!response.IsSuccessStatusCode)
                        throw new ApiException(JsonConvert.DeserializeObject<ApiException>(await response.Content.ReadAsStringAsync()), response.StatusCode);
                    else
                        json = await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    throw new ApiException(ex, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return JsonConvert.DeserializeObject<List<Game>>(json);
        }
    }
}

