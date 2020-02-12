﻿//-----------------------------------------------------------------------
// <copyright file="ITeamRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Team repository
    /// </summary>
    public interface ITeamRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Add</param>
        void Add(Team team);

        /// <summary>
        /// Definition of the function that will Delete from the database the Team passed in the parameters
        /// </summary>
        /// <param name="idTeam">Object Team to Delete</param>
        Task<ApiResponse> Delete(int idTeam);


        /// <summary>
        /// Definition of the method to find an Team with his Id
        /// </summary>
        /// <param name="id">The Id of the Team</param>
        /// <returns>The Team with the Id researched</returns>
        Task<Team> Find(int id);


        /// <summary>
        /// Definition of the method to find all Team
        /// </summary>
        /// <returns>A List of Teams</returns>
        Task<IEnumerable<Team>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Update</param>
        void Update(Team team);
    }
}
