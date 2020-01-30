//-----------------------------------------------------------------------
// <copyright file="MessageRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;

    /// <summary>
    /// Class that connect the Object Game to the database
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Message passed in the parameters to the database
        /// </summary>
        /// <param name="message">Object Message to Add</param>
        public void Add(Message message)
        {
            this._smartCityContext.Messages.Add(message);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Message passed in the parameters
        /// </summary>
        /// <param name="message">Object Message to Delete</param>
        public void Delete(Message message)
        {
            this._smartCityContext.Messages.Remove(message);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Message with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Message</param>
        /// <returns>The Message with the Id researched</returns>
        public Message Find(int id)
        {
            return this._smartCityContext.Messages.Find(id);
        }

        /// <summary>
        /// Method to find all Message from the database
        /// </summary>
        /// <returns>A List with all Messages</returns>
        public IEnumerable<Message> FindAll()
        {
            return this._smartCityContext.Messages;
        }

        /// <summary>
        /// Method that will Update the Message passed in the parameters to the database
        /// </summary>
        /// <param name="message">Object Message to Update</param>
        public void Update(Message message)
        {
            this._smartCityContext.Messages.Update(message);
            this._smartCityContext.SaveChanges();
        }
    }
}
