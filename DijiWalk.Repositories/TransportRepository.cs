﻿//-----------------------------------------------------------------------
// <copyright file="TransportRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Transport to the database
    /// </summary>
    public class TransportRepository : ITransportRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TransportRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="transport">Object Transport to Add</param>
        public async Task<ApiResponse> Add(Transport transport)
        {
            try
            {
                await _context.Transports.AddAsync(transport);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = transport };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to Delete from the database the Transport passed in the parameters
        /// </summary>
        /// <param name="transport">Object Transport to Delete</param>
        public async Task<ApiResponse> Delete(int idTransport)
        {
            try
            {
                _context.Transports.Remove(await _context.Transports.FindAsync(idTransport));
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to find an Transport with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Transport</param>
        /// <returns>The Transport with the Id researched</returns>
        public async Task<Transport> Find(int id)
        {
            return await _context.Transports.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Transport from the database
        /// </summary>
        /// <returns>A List with all Transport</returns>
        public async Task<IEnumerable<Transport>> FindAll()
        {
            return await _context.Transports.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="transport">Object Transport to Update</param>
        public async Task<ApiResponse> Update(Transport transport)
        {

            try
            {
                _context.Transports.Update(transport);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Update };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
