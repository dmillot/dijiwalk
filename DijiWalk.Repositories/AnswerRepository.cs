//-----------------------------------------------------------------------
// <copyright file="AnswerRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Answer to the database
    /// </summary>
    public class AnswerRepository : IAnswerRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public AnswerRepository(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Add the Answer passed in the parameters to the database
        /// </summary>
        /// <param name="answer">Object Answer to Add</param>
        public void Add(Answer answer)
        {
           _context.Answers.Add(answer);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Answer passed in the parameters
        /// </summary>
        /// <param name="answer">Object Answer to Delete</param>
        public void Delete(Answer answer)
        {
           _context.Answers.Remove(answer);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Answer with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Answer</param>
        /// <returns>The Answer with the Id researched</returns>
        public async Task<Answer> Find(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Answer from the database
        /// </summary>
        /// <returns>A List with all Answers</returns>
        public async Task<IEnumerable<Answer>> FindAll()
        {
            return await _context.Answers.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Answer passed in the parameters to the database
        /// </summary>
        /// <param name="answer">Object Answer to Update</param>
        public void Update(Answer answer)
        {
           _context.Answers.Update(answer);
           _context.SaveChanges();
        }
    }
}
