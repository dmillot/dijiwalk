//-----------------------------------------------------------------------
// <copyright file="AnswerRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Answer to the database
    /// </summary>
    public class AnswerRepository : IAnswerRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Answer passed in the parameters to the database
        /// </summary>
        /// <param name="answer">Object Answer to Add</param>
        public void Add(Answer answer)
        {
            this._smartCityContext.Answers.Add(answer);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Answer passed in the parameters
        /// </summary>
        /// <param name="answer">Object Answer to Delete</param>
        public void Delete(Answer answer)
        {
            this._smartCityContext.Answers.Remove(answer);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Answer with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Answer</param>
        /// <returns>The Answer with the Id researched</returns>
        public Answer Find(int id)
        {
            return this._smartCityContext.Answers.Find(id);
        }

        /// <summary>
        /// Method to find all Answer from the database
        /// </summary>
        /// <returns>A List with all Answers</returns>
        public IEnumerable<Answer> FindAll()
        {
            return this._smartCityContext.Answers;
        }

        /// <summary>
        /// Method that will Update the Answer passed in the parameters to the database
        /// </summary>
        /// <param name="answer">Object Answer to Update</param>
        public void Update(Answer answer)
        {
            this._smartCityContext.Answers.Update(answer);
            this._smartCityContext.SaveChanges();
        }
    }
}
