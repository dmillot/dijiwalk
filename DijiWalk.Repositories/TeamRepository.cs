//-----------------------------------------------------------------------
// <copyright file="TeamRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Business.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Team to the database
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        private readonly SmartCityContext _context;

        private readonly ITeamBusiness _teamBusiness;

        private readonly ITeamPlayerBusiness _teamPlayerBusiness;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TeamRepository(SmartCityContext context, ITeamBusiness teamBusiness, ITeamPlayerBusiness teamPlayerBusiness)
        {
            _context = context;
            _teamBusiness = teamBusiness;
            _teamPlayerBusiness = teamPlayerBusiness;
        }


     
        /// <summary>
        /// Method to Delete from the database the Team passed in the parameters
        /// </summary>
        /// <param name="idTeam">Object Team to Delete</param>
        public async Task<ApiResponse> Delete(int idTeam)
        {
            try
            {
                if (!await _teamBusiness.ContainsTeams(idTeam))
                {
                    await _teamBusiness.DeleteAllFromTeam(idTeam);
                }
                _context.Teams.Remove(await _context.Teams.FindAsync(idTeam));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        

        /// <summary>
        /// Method to find an Team with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Team</param>
        /// <returns>The Team with the Id researched</returns>
        public async Task<Team> Find(int id)
        {
            return await _context.Teams.Where(t => t.Id == id).Include(t => t.TeamPlayers).ThenInclude(tp => tp.Player).Include(t => t.Captain).Include(t => t.TeamRoutes).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to find all Team from the database
        /// </summary>
        /// <returns>A List with all Teams</returns>
        public async Task<IEnumerable<Team>> FindAll()
        {
            return await _context.Teams.Include(t => t.TeamPlayers).ThenInclude(tp => tp.Player).Include(t => t.TeamRoutes).Include(t => t.Captain).ToListAsync();
        }


        /// <summary>
        /// Method to Add the team passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Team to Add</param>
        public async Task<ApiResponse> Add(Team team)
        {
            try
            {
                var newTeam = _teamBusiness.SeparateTeam(team);
                await _context.Teams.AddAsync(newTeam);
                await _context.SaveChangesAsync();
                team.Id = newTeam.Id;
                var response = await _teamPlayerBusiness.SetUp(team.TeamPlayers.ToList(), team.Id);
                if (response.Status == ApiStatus.Ok)
                {
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = await this.Find(team.Id) };
                }
                else
                    return response;
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method that will Update the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Update</param>
        public async Task<ApiResponse> Update(Team team)
        {
            try
            {
                if (!await _teamBusiness.ContainsTeams(team.Id))
                {
                    _context.Teams.Update(_teamBusiness.SeparateTeam(team));
                    await _context.SaveChangesAsync();

                    // Récupérer tous les id actuels des participants de la team dans la DB
                    var currentIdParticipants = await _context.Teamplayers.Where(p => p.IdTeam == team.Id).Select(p => p.IdPlayer).ToListAsync();

                    // Récuperer tous les id des participants de la game envoyée en paramètre
                    var idParticipant = team.TeamPlayers.Select(p => p.IdPlayer).ToList();

                    // liste des id de la BDD qui ne sont pas dans les ID de l'update
                    var oldIdParticipant = currentIdParticipants.Where(p => !idParticipant.Contains(p)).ToList();

                    var responseDelete = await _teamPlayerBusiness.DeleteFromTeam(await _context.Teamplayers.Where(p => p.IdTeam == team.Id && oldIdParticipant.Contains(p.IdPlayer)).ToListAsync());
                    if (responseDelete.Status == ApiStatus.Ok)
                    {
                        var responseTeamPlayerAdd = await _teamPlayerBusiness.SetUp(team.TeamPlayers.Where(p => !currentIdParticipants.Contains(p.IdPlayer)).ToList(), team.Id);
                        if (responseTeamPlayerAdd.Status == ApiStatus.Ok)
                        {
                            return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Update, Response = await this.Find(team.Id) };
                        }
                        else
                            return responseTeamPlayerAdd;
                    }
                    else
                        return responseDelete;
                } else
                {
                    return new ApiResponse { Status = ApiStatus.CantUpdate, Message = "Impossible de mettre à jour une équipe qui a déjà jouée !"};
                }
               
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
