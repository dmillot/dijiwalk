﻿//-----------------------------------------------------------------------
// <copyright file="ITagRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Tag repository
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Add</param>
        void Add(Tag tag);

        /// <summary>
        /// Definition of the function that will Delete from the database the Tag passed in the parameters
        /// </summary>
        /// <param name="tag">Object Tag to Delete</param>
        void Delete(Tag tag);

        /// <summary>
        /// Definition of the method to find an Tag with his Id
        /// </summary>
        /// <param name="id">The Id of the Tag</param>
        /// <returns>The Tag with the Id researched</returns>
        Tag Find(int id);

        /// <summary>
        /// Definition of the method to find all Tag
        /// </summary>
        /// <returns>A List of Tags</returns>
        IEnumerable<Tag> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Update</param>
        void Update(Tag tag);
    }
}