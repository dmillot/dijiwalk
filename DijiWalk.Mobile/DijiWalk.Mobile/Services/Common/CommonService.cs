using DijiWalk.Common.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DijiWalk.Mobile.Services.Common
{
    public static class CommonService
    {


        public static async Task<string> GetAll(System.Type element)
        {
            return await CommonService.Get(String.Concat(Application.Current.Properties["url"], element.Name.ToLower()));
        }

        public static async Task<string> GetId(int id, System.Type element)
        {
            return await CommonService.Get(String.Concat(Application.Current.Properties["url"], element.Name.ToLower(), "/", id));
        }

        public static async Task<string> Get(string query)
        {
            //Autorise le HttpClient avec le SSL et le Https
            using (HttpClient client = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }, }, false))
            {
                try
                {
                    var response = await client.GetAsync(query);
                    if (!response.IsSuccessStatusCode)
                        //Instancie l'API exception avec comme paramètre le message d'error renvoyer par la réponse et le status code (404 error par exemple)
                        throw new ApiException(JsonConvert.DeserializeObject<ApiException>(await response.Content.ReadAsStringAsync()), response.StatusCode);
                    else
                        //Déserialize la réponse en Game
                        return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    throw new ApiException(ex, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                throw new Exception();
            }
        }

        public static async Task<string> Post(string query, object parameters)
        {
            //Autorise le HttpClient avec le SSL et le Https
            using (HttpClient client = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }, }, false))
            {
                try
                {
                    var response = await client.PostAsync(query, new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                        //Instancie l'API exception avec comme paramètre le message d'error renvoyer par la réponse et le status code (404 error par exemple)
                        throw new ApiException(JsonConvert.DeserializeObject<ApiException>(await response.Content.ReadAsStringAsync()), response.StatusCode);
                    else
                        return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    throw new ApiException(ex, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                throw new Exception();
            }
        }
    }
}
