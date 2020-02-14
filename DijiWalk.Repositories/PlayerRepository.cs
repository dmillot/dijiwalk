//-----------------------------------------------------------------------
// <copyright file="PlayerRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Business;
    using DijiWalk.Business.Contracts;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Encryption;
    using DijiWalk.Common.FileExtension;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Player to the database
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        private readonly SmartCityContext _context;

        private readonly ITeamBusiness _teamBusiness;

        private readonly ICryption _cryption;

        private readonly IPlayerBusiness _playerBusiness;

        private readonly IImageBusiness _imageBusiness;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public PlayerRepository(SmartCityContext context, ITeamBusiness teamBusiness, ICryption cryption, IPlayerBusiness playerBusiness, IImageBusiness imageBusiness)
        {
            _context = context;
            _teamBusiness = teamBusiness;
            _cryption = cryption;
            _playerBusiness = playerBusiness;
            _imageBusiness = imageBusiness;
        }


        /// <summary>
        /// Method to Add the Player passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Player to Add</param>
        public async Task<ApiResponse> Add(Player player)
        {
            try
            {
                if (!await _playerBusiness.Check(player))
                {
                    player.Picture = await _imageBusiness.UploadImage(player.ImageBase64, $"{player.FirstName}-{player.LastName}-{player.Login}-{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                    player.Password = _cryption.Encrypt(player.Password);
                    await _context.Players.AddAsync(player);
                    await _context.SaveChangesAsync();
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = player };
                } else
                {
                    return new ApiResponse { Status = ApiStatus.CantDelete, Message = "Le pseudo et l'e-mail doivent être uniques."};
                }
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to Delete from the database the Player passed in the parameters
        /// </summary>
        /// <param name="player">Object Player to Delete</param>
        public async Task<ApiResponse> Delete(int idPlayer)
        {
            try
            {
                if (!await _teamBusiness.OnlyThisPlayer(idPlayer))
                {
                    if (!await _teamBusiness.ContainsTeamsWithPlayer(idPlayer))
                    {
                       
                        await _teamBusiness.DeleteAllFromPlayer(idPlayer);
                        var player = await _context.Players.FindAsync(idPlayer);
                        _imageBusiness.DeleteImage(player.Picture);
                        _context.Players.Remove(player);
                        await _context.SaveChangesAsync();
                        return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
                    }
                    else
                    {
                        return new ApiResponse { Status = ApiStatus.CantDelete, Message = "Ce participant participe ou a déjà participé à un jeu !" };
                    }

                }
                else
                {
                    return new ApiResponse { Status = ApiStatus.CantDelete, Message = "Impossible de supprimer ce participant, il est dans une équipe seul !" };
                }
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to find an Player with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Player</param>
        /// <returns>The Player with the Id researched</returns>
        public async Task<Player> Find(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Player from the database
        /// </summary>
        /// <returns>A List with all Players</returns>
        public async Task<IEnumerable<Player>> FindAll()
        {
            return await _context.Players.ToListAsync();
        }


        public async Task<Game> GetActualGame(int idPlayer)
        {
            return await _playerBusiness.GetActualGame(idPlayer);
        }

        public async Task<IEnumerable<Game>> GetPreviousGames(int idPlayer)
        {
            return await _playerBusiness.GetPreviousGames(idPlayer);
        }

        /// <summary>
        /// Method that will Update the Player passed in the parameters to the database
        /// </summary>
        /// <param name="player">Object Player to Update</param>
        public async Task<ApiResponse> Update(Player player)
        {
            try
            {
                if (!await _playerBusiness.CheckUpdate(player))
                {
                    if (player.ImageChanged)
                    {
                        _imageBusiness.DeleteImage(player.Picture);
                        player.Picture = await _imageBusiness.UploadImage(player.ImageBase64, $"{player.FirstName}-{player.LastName}-{player.Login}-{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                    }
                    player.Password = _cryption.Encrypt(player.Password);
                    _context.Players.Update(player);
                    await _context.SaveChangesAsync();
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Update, Response = player };
                }
                else
                {
                    return new ApiResponse { Status = ApiStatus.CantDelete, Message = "Le pseudo et l'e-mail doivent être uniques." };
                }
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
