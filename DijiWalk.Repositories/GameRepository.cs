//-----------------------------------------------------------------------
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

        private readonly ITeamBusiness _teamBusiness;



        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public GameRepository(SmartCityContext context, IGameBusiness gameBusiness, IPlayBusiness playBusiness, ITeamBusiness teamBusiness)
        {
            _context = context;
            _gameBusiness = gameBusiness;
            _playBusiness = playBusiness;
            _teamBusiness = teamBusiness;
        }
        /// <summary>
        /// Method to Add the Game passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Game to Add</param>
        public async Task<ApiResponse> Add(Game game)
        {
            try
            {
                if (!await _teamBusiness.DuplicatePlayer(game.Plays.ToList()))
                {
                    var newGame = _gameBusiness.SeparatePlay(game);
                    await _context.Games.AddAsync(newGame);
                    await _context.SaveChangesAsync();
                    var reponse = await _playBusiness.SetUp(game.Plays.ToList(), newGame.Id);
                    if (reponse.Status == ApiStatus.Ok)
                    {
                        var reponseGenerate = await _gameBusiness.GenerateTeamRoute(game, newGame.Id);
                        if (reponseGenerate.Status == ApiStatus.Ok)
                            return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Update, Response = await this.Find(newGame.Id) };
                        else
                            return reponseGenerate;
                    }
                    else
                        return reponse;
                }
                else
                {
                    return new ApiResponse { Status = ApiStatus.CantAdd, Message = "Impossible, un joueur est dans plusieurs équipes selectionnées !" };
                }
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
                }
                else
                {
                    return new ApiResponse { Status = ApiStatus.CantDelete, Message = "Ce jeu est en cours ou a déjà été joué." };
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
        /// Method to check if transport is in a game
        /// </summary>
        /// <param name="idTransport">id of a team</param>
        public async Task<bool> ContainsTransport(int idTransport)
        {
            return await _context.Games.AnyAsync(t => t.IdTransport == idTransport);
        }

        /// <summary>
        /// Method to find an Game with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Game</param>
        /// <returns>The Game with the Id researched</returns>
        public async Task<Game> Find(int id)
        {
            var game = await _context.Games.Where(g => g.Id == id).Include(p => p.Plays).ThenInclude(p => p.Team).ThenInclude(t => t.TeamPlayers).ThenInclude(tp => tp.Player).Include(r => r.Route).ThenInclude(r => r.RouteSteps).ThenInclude(rs => rs.Step).Include(t => t.Transport).Include(o => o.Organizer).Include(g => g.TeamRoutes).ThenInclude(tr => tr.RouteStep).ThenInclude(rt => rt.Step).FirstOrDefaultAsync();
            if (game.Organizer != null)
            {
                game.Organizer.Games = new HashSet<Game>();
                game.Organizer.Messages = new HashSet<Message>();
                game.Organizer.Players = new HashSet<Player>();
                game.Organizer.Routes = new HashSet<Route>();
                game.Organizer.Teams = new HashSet<Team>();
            }
            else
            {
                game.Organizer = await _context.Organizers.FirstOrDefaultAsync(o => o.Id == game.IdOrganizer);
            }
            if (game.Route != null)
            {
                game.Route.Games = new HashSet<Game>();
                game.Route.RouteSteps = game.Route.RouteSteps.Select(rs =>
               {
                   rs.Route = null;
                   rs.Step.Missions = new HashSet<Mission>();
                   rs.Step.RouteSteps = new HashSet<RouteStep>();
                   rs.Step.StepTags = new HashSet<StepTag>();
                   rs.Step.StepValidations = new HashSet<StepValidation>();
                   rs.TeamRoutes = new HashSet<TeamRoute>();
                   return rs;
               }).ToList();
                game.Route.RouteTags = new HashSet<RouteTag>();
            }
            else
            {
                game.Route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == game.IdRoute);
            }
            if (game.Transport != null)
            {
                game.Transport.Games = new HashSet<Game>();
            }
            else
            {
                game.Transport = await _context.Transports.FirstOrDefaultAsync(t => t.Id == game.IdTransport);
            }
            game.Plays = await _context.Plays.Where(p => p.IdGame == id).Include(p => p.Team).ThenInclude(t => t.TeamPlayers).ThenInclude(tp => tp.Player).ToListAsync();
            game.TeamRoutes = game.TeamRoutes.OrderBy(x => x.IdTeam).ThenBy(x => x.StepOrder).ToList();
            return game;
        }

        /// <summary>
        /// Method to find an Game with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Game</param>
        /// <returns>The Game with the Id researched</returns>
        public async Task<Game> FindNoTracking(int id)
        {
            var game = await _context.Games.AsNoTracking().Where(g => g.Id == id).Include(r => r.Route).ThenInclude(r => r.RouteSteps).Include(t => t.Transport).Include(o => o.Organizer).Include(p => p.Plays).ThenInclude(t => t.Team).ThenInclude(m => m.TeamPlayers).ThenInclude(p => p.Player).Include(g => g.TeamRoutes).ThenInclude(tr => tr.RouteStep).ThenInclude(rt => rt.Step).FirstOrDefaultAsync();
            if (game.Organizer != null)
            {
                game.Organizer.Games = new HashSet<Game>();
                game.Organizer.Messages = new HashSet<Message>();
                game.Organizer.Players = new HashSet<Player>();
                game.Organizer.Routes = new HashSet<Route>();
                game.Organizer.Teams = new HashSet<Team>();
            }
            else
            {
                game.Organizer = await _context.Organizers.FirstOrDefaultAsync(o => o.Id == game.IdOrganizer);
            }
            if (game.Route != null)
            {
                game.Route.Games = new HashSet<Game>();
                game.Route.RouteSteps = game.Route.RouteSteps.Select(rs =>
                {
                    rs.Route = null;
                    rs.Step = null;
                    rs.TeamRoutes = new HashSet<TeamRoute>();
                    return rs;
                }).ToList();
                game.Route.RouteTags = new HashSet<RouteTag>();
            }
            else
            {
                game.Route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == game.IdRoute);
            }
            if (game.Transport != null)
            {
                game.Transport.Games = new HashSet<Game>();
            }
            else
            {
                game.Transport = await _context.Transports.FirstOrDefaultAsync(t => t.Id == game.IdTransport);
            }

            if (game.Plays != null)
            {
                game.Plays = game.Plays.Select(p =>
                {
                    p.Game = null;
                    p.Team.Organizer = null;
                    p.Team.Plays = new HashSet<Play>();
                    p.Team.TeamAnswers = new HashSet<TeamAnswer>();
                    p.Team.TeamPlayers = new HashSet<TeamPlayer>();
                    p.Team.TeamRoutes = new HashSet<TeamRoute>();
                    return p;
                }).ToList();
            }
            return game;
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
            return await _context.Games.Where(g => g.CreationDate <= DateTime.Now && g.FinalTime == null).Include(r => r.Route).ThenInclude(r => r.RouteSteps).ThenInclude(rs => rs.Step).Include(r => r.Organizer).Include(r => r.TeamRoutes)
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
                if (!await _teamBusiness.DuplicatePlayer(game.Plays.ToList()))
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE [dbo].[GAME] SET  [Game_fk_Route_Id] = {game.IdRoute},  [Game_fk_Organizer_Id] = {game.IdOrganizer},  [Game_fk_Transport_Id] = {game.IdTransport} WHERE [Game_Id] = {game.Id}");
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
                            var responseDeleteTeamRoutes = await _gameBusiness.DeleteAllTeamsRoute(game.Id);
                            if (responseDeleteTeamRoutes.Status == ApiStatus.Ok)
                            {
                                var reponseGenerate = await _gameBusiness.GenerateTeamRoute(await this.FindNoTracking(game.Id), game.Id);
                                if (reponseGenerate.Status == ApiStatus.Ok)
                                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Update, Response = await this.Find(game.Id) };
                                else
                                    return reponseGenerate;
                            }
                            else
                                return responseDeleteTeamRoutes;
                        }
                        else
                            return responsePlayAdd;
                    }
                    else
                        return responseDelete;
                }
                else
                {
                    return new ApiResponse { Status = ApiStatus.CantAdd, Message = "Impossible, un joueur est dans plusieurs équipes selectionnées !" };
                }
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}