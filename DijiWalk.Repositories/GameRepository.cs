﻿//-----------------------------------------------------------------------
// <copyright file="GameRepository.cs" company="DijiWalk">
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
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Game to the database
    /// </summary>
    public class GameRepository : IGameRepository
    {


        private readonly SmartCityContext _context;

        private readonly IGameBusiness _gameBusiness;

        private readonly IPlayBusiness _playBusiness;


        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public GameRepository(SmartCityContext context, IGameBusiness gameBusiness, IPlayBusiness playBusiness)
        {
            this._context = context;
            this._gameBusiness = gameBusiness;
            this._playBusiness = playBusiness;
        }
        /// <summary>
        /// Method to Add the Game passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Game to Add</param>
        public async Task<ApiResponse> Add(Game game)
        {
            try
            {
                await _context.Games.AddAsync(game);
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = await this.Find(game.Id) };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to Delete from the database the Game passed in the parameters
        /// </summary>
        /// <param name="game">Object Game to Delete</param>
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);
                if (_gameBusiness.IsGameInProgress(game) == false)
                {
                    await _gameBusiness.DeleteAllTeams(game);
                    _context.Games.Remove(await _context.Games.FindAsync(id));
                    _context.SaveChanges();
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
                } else
                {
                    return new ApiResponse { Status = ApiStatus.CantDelete, Message = "Ce jeu est en cours.." };
                }
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to check if route is in a game
        /// </summary>
        /// <param name="idRoute">id of a team</param>
        public async Task<bool> ContainsRoute(int idRoute)
        {
            return await _context.Games.AnyAsync(t => t.IdRoute == idRoute);
        }

        /// <summary>
        /// Method to find an Game with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Game</param>
        /// <returns>The Game with the Id researched</returns>
        public async Task<Game> Find(int id)
        {
            return await _context.Games.Where(g => g.Id == id).Include(r => r.Route).Include(t => t.Transport).Include(o => o.Organizer).Include(p => p.Plays).ThenInclude(t => t.Team).ThenInclude(m => m.TeamPlayers).ThenInclude(p => p.Player).Include(g => g.TeamRoutes).ThenInclude(tr => tr.RouteStep).ThenInclude(rt => rt.Step).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to find all Game from the database
        /// </summary>
        /// <returns>A List with all Games</returns>
        public async Task<List<Game>> FindAll()
        {
            return await _context.Games
                .Include(r => r.Route)
                .Include(t => t.Transport)
                .Include(o => o.Organizer)
                .Include(p => p.Plays).ThenInclude(t => t.Team)
                .ToListAsync();
        }

        /// <summary>
        /// Method to find all active Game from the database
        /// </summary>
        /// <returns>A List with all actives Games</returns>
        public async Task<IEnumerable<Game>> FindAllActifs()
        {
            return await _context.Games.Where(g => g.CreationDate <= DateTime.Now && g.FinalTime == null)
                .Include(g=>g.Organizer)
                .Include(g =>g.Route)
                .ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Game passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Game to Update</param>
        public async Task<ApiResponse> Update(Game game)
        {
            try
            {
                _context.Games.Update(_gameBusiness.SeparatePlay(game));
                await _context.SaveChangesAsync();

                // Récupérer tous les id actuels des teams de la game dans la DB
                var currentIdPlays = await _context.Plays.Where(p => p.IdGame == game.Id).Select(p => p.IdTeam).ToListAsync();

                // Récuperer tous les id teams de la game envoyée en paramètre
                var idPlay = game.Plays.Select(p => p.IdTeam).ToList();

                // liste des id de la BDD qui ne sont pas dans les ID de l'update
                var oldIdPlay = currentIdPlays.Where(p => !idPlay.Contains(p)).ToList();

                var responseDelete = await _playBusiness.DeleteFromGame(await _context.Plays.Where(p => p.IdGame == game.Id && oldIdPlay.Contains(p.IdTeam)).ToListAsync());
                if (responseDelete.Status == ApiStatus.Ok)
                {
                    var responsePlayAdd = await _playBusiness.SetUp(game.Plays.Where(p => !currentIdPlays.Contains(p.IdTeam)).ToList(), game.Id);
                    if (responsePlayAdd.Status == ApiStatus.Ok)
                    {
                        return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Update, Response = await this.Find(game.Id) };
                    }
                    else
                        return responsePlayAdd;
                }
                else
                    return responseDelete;
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}