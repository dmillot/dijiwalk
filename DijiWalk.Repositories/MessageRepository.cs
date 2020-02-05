//-----------------------------------------------------------------------
// <copyright file="MessageRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Message to the database
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public MessageRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Message passed in the parameters to the database
        /// </summary>
        /// <param name="message">Object Message to Add</param>
        public void Add(Message message)
        {
           _context.Messages.Add(message);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Message passed in the parameters
        /// </summary>
        /// <param name="message">Object Message to Delete</param>
        public void Delete(Message message)
        {
           _context.Messages.Remove(message);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Message with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Message</param>
        /// <returns>The Message with the Id researched</returns>
        public async Task<Message> Find(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Message from the database
        /// </summary>
        /// <returns>A List with all Messages</returns>
        public async Task<IEnumerable<Message>> FindAll()
        {
            return await _context.Messages.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Message passed in the parameters to the database
        /// </summary>
        /// <param name="message">Object Message to Update</param>
        public void Update(Message message)
        {
           _context.Messages.Update(message);
           _context.SaveChanges();
        }
    }
}
