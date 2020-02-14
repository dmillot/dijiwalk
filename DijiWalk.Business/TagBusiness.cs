using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace DijiWalk.Business
{
    public class TagBusiness : ITagBusiness
    {

        private readonly SmartCityContext _context;



        public TagBusiness(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to separate tag and routetags 
        /// </summary>
        /// <param name="tag">Object tag</param>
        public Tag SeparateTag(Tag tag)
        {
            return new Tag(tag);
        }

    }
}
