using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IGameBusiness
    {
        bool IsGameInProgress(Game game);

        Task<ApiResponse> DeleteAllTeams(Game game);

        Game SeparatePlay(Game game);



        /// <summary>
        /// Generate team route differently and random
        /// </summary>
        /// <param name="game">Object game</param>
        /// <param name="idGame">Id of the game</param>
        Task<ApiResponse> GenerateTeamRoute(Game game, int idGame);

        Task<ApiResponse> DeleteAllTeamsRoute(int gameId);
    }
}
