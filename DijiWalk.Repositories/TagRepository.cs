//-----------------------------------------------------------------------
// <copyright file="TagRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Tag to the database
    /// </summary>
    public class TagRepository : ITagRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Add</param>
        public void Add(Tag tag)
        {
            this._smartCityContext.Tags.Add(tag);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Tag passed in the parameters
        /// </summary>
        /// <param name="tag">Object Tag to Delete</param>
        public void Delete(Tag tag)
        {
            this._smartCityContext.Tags.Remove(tag);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Tag with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Tag</param>
        /// <returns>The Tag with the Id researched</returns>
        public Tag Find(int id)
        {
            return this._smartCityContext.Tags.Find(id);
        }

        /// <summary>
        /// Method to find all Tag from the database
        /// </summary>
        /// <returns>A List with all Tag</returns>
        public IEnumerable<Tag> FindAll()
        {
            return this._smartCityContext.Tags;
        }

        /// <summary>
        /// Method that will Update the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Update</param>
        public void Update(Tag tag)
        {
            this._smartCityContext.Tags.Update(tag);
            this._smartCityContext.SaveChanges();
        }
    }
}
