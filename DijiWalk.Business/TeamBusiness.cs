using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business
{
    public class TeamBusiness : ITeamBusiness
    {

        private readonly SmartCityContext _context;

        private readonly ICapitaineBusiness _capitaineBusiness;

        public TeamBusiness(SmartCityContext context, ICapitaineBusiness capitaineBusiness)
        {
            _context = context;
            _capitaineBusiness = capitaineBusiness;
        }

        /// <summary>
        /// Method to check if team as only the player want to delete
        /// </summary>
        /// <param name="idPlayer">Id of the player</param>
        public async Task<bool> OnlyThisPlayer(int idPlayer)
        {
            bool canDelete = true;
            await _context.Teams.ForEachAsync(t => {
                if (t.TeamPlayers.Count() != 0 && t.TeamPlayers.Count() == t.TeamPlayers.Where(team => team.IdPlayer == idPlayer).ToList().Count())
                {
                    canDelete = false;
                }
            });
            return canDelete;
        }

        /// <summary>
        /// Method to Delete all team player
        /// </summary>
        /// <param name="idTeam">id of the team</param>
        public async Task<ApiResponse> DeleteAllFromTeam(int idTeam)
        {
            try
            {
                _context.Teamanswers.RemoveRange(await _context.Teamanswers.Where(x => x.IdTeam == idTeam).ToListAsync());
                _context.Teamplayers.RemoveRange(await _context.Teamplayers.Where(x => x.IdTeam == idTeam).ToListAsync());
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to Delete all team player
        /// </summary>
        /// <param name="idPlayer">id of the player</param>
        public async Task<ApiResponse> DeleteAllFromPlayer(int idPlayer)
        {
            try
            {
                await _capitaineBusiness.ChangeCapitaine(idPlayer);
                _context.Teamplayers.RemoveRange(await _context.Teamplayers.Where(x => x.IdPlayer == idPlayer).ToListAsync());
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to Delete all team route with route id
        /// </summary>
        /// <param name="idRoute">id of the route</param>
        public async Task<ApiResponse> DeleteTeamRoutesFromRoute(int idRoute)
        {
            try
            {
                _context.Teamroutes.RemoveRange(await _context.Teamroutes.Where(x => x.IdRoute == idRoute).ToListAsync());
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }


        /// <summary>
        /// Method to check if team has participed to a game
        /// </summary>
        /// <param name="idTeam">id of a team</param>
        public async Task<bool> ContainsTeams(int idTeam)
        {
            return await _context.Plays.AnyAsync(t => t.IdTeam == idTeam);
        }


        /// <summary>
        /// Method to check if player has participed to a game
        /// </summary>
        /// <param name="idPlayer">id of a player</param>
        public async Task<bool> ContainsTeamsWithPlayer(int idPlayer)
        {
            return await _context.Teamplayers.Where(team => team.IdPlayer == idPlayer).AnyAsync(x => _context.Teamroutes.Any(t => t.IdTeam == x.IdTeam));
        }

        /// <summary>
        /// Method to check if a player is in two or more teams
        /// </summary>
        /// <param name="plays">List of plays</param>
        /// <returns>false if not duplicate, true if duplicate</returns>
        public async Task<bool> DuplicatePlayer(List<Play> plays)
        {
            var listTeams = plays.Select(p => p.IdTeam).ToList();
            var listPlayer = new List<int>();
            var teams = await _context.Teams.AsNoTracking().Where(t => listTeams.Contains(t.Id)).Include(t => t.TeamPlayers).ToListAsync();
            var listTeamPlayer = teams.Select(t => {
                if(t.TeamPlayers.Count != 0)
                {
                    return t.TeamPlayers;
                }
                return null;
                }).ToList();
            if (!listTeamPlayer.All(t => t == null))
            {
                teams.Select(t =>
                {
                    t.TeamPlayers = t.TeamPlayers.Select(tp =>
                    {
                        tp.Team = null;
                        return tp;
                    }).ToList();
                    return t;
                }).ToList().ForEach(t => listPlayer.AddRange(t.TeamPlayers.Select(tp => tp.IdPlayer)));
            } else
            {
                var idTeams = teams.Select(t => t.Id).ToList();
                var teamPlayers = await _context.Teamplayers.AsNoTracking().Where(tp => tp.IdTeam == idTeams[0]).ToListAsync();
                listPlayer.AddRange(teamPlayers.Select(tp => tp.IdPlayer).ToList());
            }
            var result = false;
            var listUniqueId = new List<int>();
            foreach(var player in listPlayer)
            {
                if(listUniqueId.Contains(player))
                {
                    result = true;
                    break;
                }
                listUniqueId.Add(player);
            };
            return result;
        }

        /// <summary>
        /// Method to separate team and mission 
        /// </summary>
        /// <param name="team">Object List team</param>
        public Team SeparateTeam(Team team)
        {
            return new Team(team);
        }

    }
}
