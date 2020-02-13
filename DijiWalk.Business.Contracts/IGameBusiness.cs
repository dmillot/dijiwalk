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
    }
}
