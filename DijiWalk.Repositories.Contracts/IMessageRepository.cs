//-----------------------------------------------------------------------
// <copyright file="IMessageRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Game repository
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Definition of the function that will Add the GaMessageme passed in the parameters to the database
        /// </summary>
        /// <param name="message">Object Message to Add</param>
        void Add(Message message);

        /// <summary>
        /// Definition of the function that will Delete from the database the Message passed in the parameters
        /// </summary>
        /// <param name="message">Object Message to Delete</param>
        void Delete(Message message);

        /// <summary>
        /// Definition of the method to find an Message with his Id
        /// </summary>
        /// <param name="id">The Id of the Message</param>
        /// <returns>The Message with the Id researched</returns>
        Message Find(int id);

        /// <summary>
        /// Definition of the method to find all Message
        /// </summary>
        /// <returns>A List of Messages</returns>
        IEnumerable<Message> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Message passed in the parameters to the database
        /// </summary>
        /// <param name="message">Object Message to Update</param>
        void Update(Message message);
    }
}
